using DynamicData;
using Mutagen.Bethesda;
using Mutagen.Bethesda.Oblivion;
using Mutagen.Bethesda.Plugins;
using Mutagen.Bethesda.Plugins.Cache;
using Mutagen.Bethesda.Synthesis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnleveledOblivion
{
    public static class CreatureLeveledListPatcher
    {
        public static void UpdateCreatureLeveledLists(IPatcherState<IOblivionMod, IOblivionModGetter> state, List<Creature> creatures)
        {
            foreach (var clGetter in state.LoadOrder.PriorityOrder.LeveledCreature().WinningOverrides())
            {
                // Add it to the patch
                var creatureReferenceList = state.PatchMod.LeveledCreatures.GetOrAddAsOverride(clGetter);

                // Adjust
                bool containsSpawnChanceFix = (state.LoadOrder.Keys.FirstOrDefault(x => x.FileName == "Creature Spawn Chance Fix.esp")) == null ? false : true;
                if (creatureReferenceList != null && creatureReferenceList.Entries.Any())
                {
                    OverrideCertainLists(ref creatureReferenceList, creatureReferenceList.EditorID, creatures, containsSpawnChanceFix);

                    short? max = creatureReferenceList?.Entries.Max(x => x.Level);
                    foreach (var entry in creatureReferenceList.Entries)
                    {
                        entry.Level = 1;
                    }
                }
            }
        }

        public static void OverrideCertainLists(ref LeveledCreature? leveledCreature, string editorID, List<Creature> creatures, bool containsSpawnChanceFix = false)
        {
            foreach (var key in Overrides.Keys)
            {
                if (editorID.Contains(key) && !editorID.ToLower().Contains("boss"))
                {
                    if (containsSpawnChanceFix && (editorID.Contains("75") || editorID.Contains("50") || editorID.Contains("25")))
                    {
                        continue;
                    }
                    var creature = creatures.FirstOrDefault(c => c.EditorID == Overrides[key].Key);
                    if (creature != null)
                    {
                        for (int i = 0; i < Overrides[key].Value; i++)
                        {
                            var itemLink = new FormLink<INpcSpawnGetter>(creature.FormKey);
                            leveledCreature.Entries.Add(
                                new LeveledCreatureEntry()
                                {
                                    Level = 1,
                                    Count = 1,
                                    Reference = itemLink
                                }
                            );
                        }
                    }
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

        public static Dictionary<string, KeyValuePair<string, int>> Overrides { get; set; }
            = new Dictionary<string, KeyValuePair<string, int>>
            {
                { "MythicNatureWet", new KeyValuePair<string, int>("CreatureMudCrab", 6) },
                { "Kvatch", new KeyValuePair<string, int>("CreatureScampStunted", 7) },
                { "LL1Beast", new KeyValuePair<string, int>("CreatureRat", 3) },
                { "LL1ConjurerDaeda", new KeyValuePair<string, int>("CreatureScampStunted", 5) },
                { "LL1Daedra", new KeyValuePair<string, int>("CreatureScamp", 7) },
                { "LL1DaedricBeast", new KeyValuePair<string, int>("CreatureScampStunted", 5) },
                { "LL1Dremora", new KeyValuePair<string, int>("Dremora0ChurlMelee1", 4) },
                { "LL1Goblin", new KeyValuePair<string, int>("CreatureGoblin1", 2) },
                { "LL1MythicEnemy", new KeyValuePair<string, int>("CreatureWolf", 3) },
                { "LL1MythicNature", new KeyValuePair<string, int>("CreatureImp", 3) },
                { "LL1RoadFarmlands", new KeyValuePair<string, int>("CreatureDeerDoe", 2) },
                { "LL1RoadForest", new KeyValuePair<string, int>("CreatureDeerDoe", 2) },
                { "LL1RoadHighlands", new KeyValuePair<string, int>("CreatureDeerDoe", 2) },
                { "LL1RoadMountains", new KeyValuePair<string, int>("CreatureWolf", 1) },
                { "LL1RoadPlains", new KeyValuePair<string, int>("CreatureDeerDoe", 3) },
                { "LL1RoadRainforest", new KeyValuePair<string, int>("CreatureDeerDoe", 2) },
                { "LL1RoadSwamp", new KeyValuePair<string, int>("CreatureMudCrab ", 1) },
                { "LL1RoadValley", new KeyValuePair<string, int>("CreatureDeerDoe", 2) },
                { "LL1SkeletonMelee", new KeyValuePair<string, int>("CreatureSkeleton1", 2) },
                { "LL1SkeletonMissile", new KeyValuePair<string, int>("CreatureSkeleton1Archer", 2) },
                { "LL1UndeadBones", new KeyValuePair<string, int>("CreatureSkeleton1", 4) },
                { "LL1UndeadEthereal", new KeyValuePair<string, int>("CreatureGhost", 4) },
                { "LL1UndeadZombie", new KeyValuePair<string, int>("CreatureZombie1", 4) },
                { "MS48", new KeyValuePair<string, int>("CreatureScampStunted", 10) }, // Breaking the siege of Kvatch
            };

    }
}
