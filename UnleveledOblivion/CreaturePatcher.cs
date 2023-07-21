using Mutagen.Bethesda;
using Mutagen.Bethesda.Oblivion;
using Mutagen.Bethesda.Plugins.Cache;
using Mutagen.Bethesda.Synthesis;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace UnleveledOblivion
{
    public static class CreaturePatcher
    {
        public static void UpdateCreatureLeveledLists(IPatcherState<IOblivionMod, IOblivionModGetter> state)
        {
            foreach (var clGetter in state.LoadOrder.PriorityOrder.LeveledCreature().WinningOverrides())
            {
                // Add it to the patch
                var creatureList = state.PatchMod.LeveledCreatures.GetOrAddAsOverride(clGetter);

                // Adjust
                if (creatureList != null && creatureList.Entries.Any())
                {
                    short? max = creatureList?.Entries.Max(x => x.Level);
                    var newEntries = new List<LeveledCreatureEntry>();
                    foreach (var entry in creatureList.Entries)
                    {
                        var initialLevel = entry.Level;
                        entry.Level = 1;
                        if (max > 1)
                        {
                            switch (initialLevel)
                            {
                                case short level when initialLevel == 1:
                                    AddDeepCopiesOfLeveledListEntry(9, entry, ref newEntries);
                                    break;
                                case short level when level <= 7:
                                    AddDeepCopiesOfLeveledListEntry(1, entry, ref newEntries);
                                    break;
                                default:
                                    AddDeepCopiesOfLeveledListEntry(0, entry, ref newEntries);
                                    break;
                            }
                        }
                    }
                    creatureList.Entries.AddRange(newEntries);
                }                
            }
        }

        public static void AddDeepCopiesOfLeveledListEntry(int copies, LeveledCreatureEntry entry, ref List<LeveledCreatureEntry> list)
        {
            for (int i = 0; i < copies; i++)
            {
                list.Add(entry.DeepCopy());  
            }
        }

        public static void UpdateCreatures(IPatcherState<IOblivionMod, IOblivionModGetter> state)
        {
            var highestLevels = new Dictionary<string, (ushort, List<Creature>)>();
            var regex = new Regex(@"\d+$");  // regex to match trailing numbers

            // Read the creature file and create a dictionary
            var path = Path.Combine(state.ExtraSettingsDataPath, "Creatures.json");
            string creatureLevelsJson = File.ReadAllText(path);
            var creatureLevelsFromFile = JsonConvert.DeserializeObject<List<NPCLevel>>(creatureLevelsJson)
                        .Where(creatureLevel => creatureLevel != null && !string.IsNullOrWhiteSpace(creatureLevel?.EditorID))
                        .ToDictionary(creatureLevel => creatureLevel.EditorID, level => level.Level);

            foreach (var creatureGetter in state.LoadOrder.PriorityOrder.Creature().WinningOverrides())
            {
                Creature creature = state.PatchMod.Creatures.GetOrAddAsOverride(creatureGetter) ?? throw new Exception("Could not add creature as override.");
                if (creature.Configuration is null) { continue; }

                // Adjust
                if (creature.Configuration.Flags.HasFlag(Creature.CreatureFlag.PCLevelOffset))
                {
                    CalculateCreatureLevel(creature, state.LinkCache, isStatic: false, creatureLevelsFromFile);
                }
                else
                {
                    CalculateCreatureLevel(creature, state.LinkCache, isStatic: true, creatureLevelsFromFile);
                }
                CheckForHigherLevelVariant(creature, highestLevels, regex);
            }
        }

        public static void CalculateCreatureLevel(Creature creature, ILinkCache linkCache, bool isStatic, Dictionary<string, short> creatureLevelsFromFile)
        {
            if (creature.Configuration is null || creature.EditorID is null || creature.Name is null) { return; }

            // If the creature is in the file, use the level from the file
            if (creatureLevelsFromFile.TryGetValue(creature.EditorID, out short levelFromFile))
            {
                creature.Configuration.CalcMin = (ushort)levelFromFile;
                creature.Configuration.CalcMax = (ushort)levelFromFile;
                var level = creature.Configuration.LevelOffset;
                creature.Data.AttackDamage = (ushort)(creature.Data.AttackDamage <= 5 ? creature.Data.AttackDamage : levelFromFile + 10);
                creature.Configuration.LevelOffset = 0;
                if (!creature.Configuration.Flags.HasFlag(Creature.CreatureFlag.PCLevelOffset))
                {
                    creature.Data.Health = (uint)(creature.Data.Health / level);
                    creature.Configuration.Fatigue = ((ushort)(creature.Configuration.Fatigue / level));
                }
                return;
            }

            short startingLevel = creature.Configuration.LevelOffset;
            creature.Configuration.LevelOffset = 0;
            short finalLevel = Program.Settings.CreatureSettings.BaseLevel;
            finalLevel += AdjustLevelOffsetBySoulType(creature);
            AdjustCreatureLevelByFaction(creature, ref finalLevel, linkCache);
            if (!isStatic)
            {
                _ = startingLevel switch
                {
                    _ when startingLevel < 0 => finalLevel -= Program.Settings.CreatureSettings.ScaledBelowPlayerOffset,
                    _ when startingLevel > 0 || creature.Configuration.CalcMin >= 15 => finalLevel += Program.Settings.CreatureSettings.ScaledAbovePlayerOffset,
                    _ => finalLevel += 0
                };
            }
            if (creature.Name.ToLower().Contains("goblin") || creature.Name.ToLower().Contains("grummite"))
            {
                finalLevel = Math.Max(finalLevel, Program.Settings.CreatureSettings.CreatureMinSettings.GoblinMin);
            }
            if (creature.Name.ToLower().Contains("dog") || creature.EditorID.ToLower().Contains("dog"))
            {
                finalLevel = Math.Max(finalLevel, Program.Settings.CreatureSettings.CreatureMinSettings.DogMin);
            }
            if (creature.Name.ToLower().Contains("wolf") || creature.EditorID.ToLower().Contains("wolf"))
            {
                finalLevel = Math.Max(finalLevel, Program.Settings.CreatureSettings.CreatureMinSettings.WolfMin);
            }
            if (creature.Name.ToLower().Contains("horse") || creature.EditorID.ToLower().Contains("horse"))
            {
                finalLevel = Math.Max(finalLevel, Program.Settings.CreatureSettings.CreatureMinSettings.HorseMin);
            }
            if (creature.Name.ToLower().Contains("troll") || creature.EditorID.ToLower().Contains("troll"))
            {
                finalLevel = Math.Max(finalLevel, Program.Settings.CreatureSettings.CreatureMinSettings.TrollMin);
            }
            if (creature.Name.ToLower().Contains("zombie") || creature.EditorID.ToLower().Contains("zombie"))
            {
                finalLevel = Math.Max(finalLevel, Program.Settings.CreatureSettings.CreatureMinSettings.ZombieMin);
            }
            if (creature.Name.ToLower().Contains("minotaur") || creature.EditorID.ToLower().Contains("minotaur"))
            {
                finalLevel = Math.Max(finalLevel, Program.Settings.CreatureSettings.CreatureMinSettings.MinotaurMin);
            }
            if (creature.Name.ToLower().Contains("ogre") || creature.EditorID.ToLower().Contains("ogre"))
            {
                finalLevel = Math.Max(finalLevel, Program.Settings.CreatureSettings.CreatureMinSettings.OgreMin);
            }
            if (creature.Name.ToLower().Contains("bear") || creature.EditorID.ToLower().Contains("bear"))
            {
                finalLevel = Math.Max(finalLevel, Program.Settings.CreatureSettings.CreatureMinSettings.BearMin);
            }
            finalLevel = Math.Max((short)1, Math.Min((short)50, finalLevel));
            if (isStatic)
            {
                creature.Configuration.Flags |= Creature.CreatureFlag.PCLevelOffset;
                creature.Data.Health = (uint)(creature.Data.Health / startingLevel);
                creature.Configuration.Fatigue = ((ushort)(creature.Configuration.Fatigue / startingLevel));
            }
            creature.Data.AttackDamage = (ushort)(creature.Data.AttackDamage <= 5 ? creature.Data.AttackDamage : finalLevel + 10);
            creature.Configuration.CalcMin = (ushort)finalLevel;
            creature.Configuration.CalcMax = (ushort)finalLevel;
        }

        public static void CheckForHigherLevelVariant(Creature creature, Dictionary<string, (ushort, List<Creature>)> highestLevels, Regex regex)
        {
            if (creature.Configuration is null || creature.EditorID is null || creature.Data is null) { return; }
            var idWithoutNumber = regex.Replace(creature.EditorID, "");
            var compositeKey = idWithoutNumber + "_" + creature.Name;

            if (highestLevels.TryGetValue(compositeKey, out (ushort, List<Creature>) previousHighestList))
            {
                if (creature.Configuration.CalcMin > previousHighestList.Item1)
                {
                    foreach (var previousHighest in previousHighestList.Item2)
                    {
                        if (previousHighest.Configuration is not null && previousHighest.Data is not null)
                        {
                            previousHighest.Configuration.CalcMin = creature.Configuration.CalcMin;
                            previousHighest.Configuration.Fatigue = creature.Configuration.Fatigue;
                            previousHighest.Data.Health = creature.Data.Health;
                            previousHighest.Data.AttackDamage = creature.Data.AttackDamage;
                            previousHighest.Data.SoulLevel = (SoulLevel)Math.Max((byte)creature.Data.SoulLevel, (byte)previousHighest.Data.SoulLevel);
                        }
                    }
                    previousHighestList.Item2.Add(creature); // Add the new creature to the list
                    highestLevels[compositeKey] = (creature.Configuration.CalcMin, previousHighestList.Item2); // Update the dictionary entry
                }
                else
                {
                    creature.Configuration.CalcMin = previousHighestList.Item1;
                    creature.Configuration.Fatigue = previousHighestList.Item2.Max(x => x.Configuration.Fatigue);
                    creature.Data.Health = previousHighestList.Item2.Max(x => x.Data.Health);
                    creature.Data.AttackDamage = previousHighestList.Item2.Max(x => x.Data.AttackDamage);
                    creature.Data.SoulLevel = (SoulLevel)Math.Max((byte)creature.Data.SoulLevel, (byte)previousHighestList.Item2.Max(c => (byte)c.Data.SoulLevel));
                    previousHighestList.Item2.Add(creature); // Add the new creature to the list
                }
            }
            else
            {
                highestLevels[compositeKey] = (creature.Configuration.CalcMin, new List<Creature>() { creature }); // Create a new list with the creature
            }
        }

        public static short AdjustLevelOffsetBySoulType(Creature creature)
        {
            var level = creature?.Data?.SoulLevel;
            switch (level)
            {
                case SoulLevel.None:
                    return 0;
                case SoulLevel.Petty:
                    return Program.Settings.CreatureSettings.SoulSettings.PettySoulOffset;
                case SoulLevel.Lesser:
                    return Program.Settings.CreatureSettings.SoulSettings.LesserSoulOffset;
                case SoulLevel.Common:
                    return Program.Settings.CreatureSettings.SoulSettings.CommonSoulOffset;
                case SoulLevel.Greater:
                    return Program.Settings.CreatureSettings.SoulSettings.GreaterSoulOffset;
                case SoulLevel.Grand:
                    return Program.Settings.CreatureSettings.SoulSettings.GrandSoulOffset;
                default:
                    return 0;
            }
        }

        public static void AdjustCreatureLevelByFaction(Creature creature, ref short finalLevel, ILinkCache linkCache)
        {
            foreach (var item in creature.Factions)
            {
                var faction = item.Faction.TryResolve(linkCache);
                if (faction is null || faction?.EditorID is null) { return; }
                if (faction.EditorID == "LichFaction")
                {
                    finalLevel = Math.Max(finalLevel, Program.Settings.CreatureSettings.FactionSettings.LichMin);
                }
            }
        }

        public static void OutputCreatureStats(IPatcherState<IOblivionMod, IOblivionModGetter> state, string extension = "")
        {
            var creatures = new List<string>();
            foreach (var creatureGetter in state.LoadOrder.PriorityOrder.Creature().WinningOverrides())
            {
                var creature = creatureGetter.DeepCopy();
                creatures.Add($"{creature.Name} - {creature.EditorID} - Level:{creature.Configuration.CalcMin} - Health:{creature.Data.Health} - Atk:{creature.Data.AttackDamage} - Ftg:{creature.Configuration.Fatigue}");
            }

            string downloadsFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads");

            // Write lists to files in the Downloads directory
            File.WriteAllLines(Path.Combine(downloadsFolder, $"creatures{extension}.txt"), creatures);
        }
    }
}
