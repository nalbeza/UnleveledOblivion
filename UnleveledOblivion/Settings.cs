using Mutagen.Bethesda.WPF.Reflection.Attributes;
using Synthesis.Bethesda.Commands;

namespace UnleveledOblivion
{
    public class Settings
    {
        [SettingName("Creature Settings")]
        public CreatureSettings CreatureSettings = new();
        [SettingName("NPC Settings")]
        public NPCSettings NPCSettings = new();
    }

    public class CreatureSettings
    {
        [MaintainOrder]
        [SettingName("Base Level")]
        [Tooltip("The base level of every creature. Zero is recommended but this can be increased to make the game more difficult.")]
        public short BaseLevel = 0;

        [SettingName("Soul Settings")]
        public SoulSettings SoulSettings = new();

        [MaintainOrder]
        [SettingName("Scaled Below Player Offset")]
        [Tooltip("Adds an additional offset to creatures that are scaled below the player in vanilla Oblivion.")]
        public short ScaledBelowPlayerOffset = -3;

        [MaintainOrder]
        [SettingName("Scaled Above Player Offset")]
        [Tooltip("Adds an additional offset to creatures that are scaled above the player in vanilla Oblivion.")]
        public short ScaledAbovePlayerOffset = 10;

        [SettingName("Creature Min Level Settings")]
        public CreatureMinSettings CreatureMinSettings = new();

        [SettingName("Faction Settings")]
        public CreatureFactionSettings FactionSettings = new();
    }

    public class SoulSettings
    {
        [MaintainOrder]
        [SettingName("Petty Soul Offset")]
        [Tooltip("Determines how many levels to add or subtract to the base level for creatures with a petty soul.")]
        public short PettySoulOffset = 1;

        [MaintainOrder]
        [SettingName("Lesser Soul Offset")]
        [Tooltip("Determines how many levels to add or subtract to the base level for creatures with a lesser soul.")]
        public short LesserSoulOffset = 5;

        [MaintainOrder]
        [SettingName("Common Soul Offset")]
        [Tooltip("Determines how many levels to add or subtract to the base level for creatures with a common soul.")]
        public short CommonSoulOffset = 10;

        [MaintainOrder]
        [SettingName("Greater Soul Offset")]
        [Tooltip("Determines how many levels to add or subtract to the base level for creatures with a greater soul.")]
        public short GreaterSoulOffset = 20;

        [MaintainOrder]
        [SettingName("Grand Soul Offset")]
        [Tooltip("Determines how many levels to add or subtract to the base level for creatures with a grand soul.")]
        public short GrandSoulOffset = 25;
    }

    public class CreatureMinSettings
    {
        [MaintainOrder]
        [SettingName("Goblin Min Level")]
        [Tooltip("Determines the minimum level a goblin can be.")]
        public short GoblinMin = 3;

        [MaintainOrder]
        [SettingName("Bear Min Level")]
        [Tooltip("Determines the minimum level a bear can be.")]
        public short BearMin = 10;

        [MaintainOrder]
        [SettingName("Troll Min Level")]
        [Tooltip("Determines the minimum level a troll can be.")]
        public short TrollMin = 10;

        [MaintainOrder]
        [SettingName("Dog Min Level")]
        [Tooltip("Determines the minimum level a dog can be.")]
        public short DogMin = 3;

        [MaintainOrder]
        [SettingName("Wolf Min Level")]
        [Tooltip("Determines the minimum level a wolf can be.")]
        public short WolfMin = 3;

        [MaintainOrder]
        [SettingName("Horse Min Level")]
        [Tooltip("Determines the minimum level a horse can be.")]
        public short HorseMin = 5;

        [MaintainOrder]
        [SettingName("Minotaur Min Level")]
        [Tooltip("Determines the minimum level a minotaur can be.")]
        public short MinotaurMin = 15;

        [MaintainOrder]
        [SettingName("Zombie Min Level")]
        [Tooltip("Determines the minimum level a zombie can be.")]
        public short ZombieMin = 3;

        [MaintainOrder]
        [SettingName("Ogre Min Level")]
        [Tooltip("Determines the minimum level a ogre can be.")]
        public short OgreMin = 10;
    }

    public class NPCSettings 
    {
        [MaintainOrder]
        [SettingName("Base Level")]
        [Tooltip("The base level of every npc before adjustments. Four is recommended but this can be increased to make the game more difficult.")]
        public short BaseLevel = 4;

        [MaintainOrder]
        [SettingName("Gender Adjustment")]
        [Tooltip("Applies an offset to the base level when NPC is flagged as female.")]
        public short GenderOffset = 0;

        [MaintainOrder]
        [SettingName("Vampire Base Level")]
        [Tooltip("Applies a separate base level to NPCs with vampire in their name.")]
        public short VampireBaseLevel = 20;

        [MaintainOrder]
        [SettingName("Tier 1 Offset")]
        [Tooltip("Adds an offset for NPCs that previously scaled below the player character.")]
        public short Tier1Offset = -2;

        [MaintainOrder]
        [SettingName("Tier 2 Offset")]
        [Tooltip("Adds an offset for NPCs that previously scaled equally with the player character.")]
        public short Tier2Offset = 5;

        [MaintainOrder]
        [SettingName("Tier 3 Offset")]
        [Tooltip("Adds an offset for NPCs that previously scaled slightly above the player character.")]
        public short Tier3Offset = 8;

        [MaintainOrder]
        [SettingName("Tier 4 Offset")]
        [Tooltip("Adds an offset for NPCs that previously scaled well above the player character.")]
        public short Tier4Offset = 13;

        [MaintainOrder]
        [SettingName("Tier 5 Offset")]
        [Tooltip("Adds an offset for NPCs that previously scaled well above the player character.")]
        public short Tier5Offset = 15;

        [MaintainOrder]
        [SettingName("Tier 6 Offset")]
        [Tooltip("Adds an offset for NPCs that previously scaled well above the player character.")]
        public short Tier6Offset = 16;

        [MaintainOrder]
        [SettingName("Tier 7 Offset")]
        [Tooltip("Adds an offset for NPCs that previously scaled well above the player character.")]
        public short Tier7Offset = 17;

        [MaintainOrder]
        [SettingName("Tier 8 Offset")]
        [Tooltip("Adds an offset for NPCs that previously scaled well above the player character.")]
        public short Tier8Offset = 18;

        [MaintainOrder]
        [SettingName("Tier 9 Offset")]
        [Tooltip("Adds an offset for NPCs that previously scaled very far above the player character.")]
        public short Tier9Offset = 26;

        [SettingName("Class Settings")]
        public ClassSettings ClassSettings = new();

        [SettingName("Race Settings")]
        public RaceSettings RaceSettings = new();

        [SettingName("Faction Settings")]
        public FactionSettings FactionSettings = new();
    }

    public class ClassSettings
    {
        [MaintainOrder]
        [SettingName("Guard Level")]
        [Tooltip("Sets the default level of guards and soldier NPCs. This is their actual level rather than an offset.")]
        public short GuardLevel = 8;

        [MaintainOrder]
        [SettingName("Commoner Offset")]
        [Tooltip("Sets the offset applied to commoner NPCs (farmers, herders, etc).")]
        public short CommonerOffset = -3;

        [MaintainOrder]
        [SettingName("Noble Offset")]
        [Tooltip("Sets the offset applied to noble NPCs.")]
        public short NobleOffset = -3;

        [MaintainOrder]
        [SettingName("Bandit Min Level")]
        [Tooltip("Sets the minimum level a bandit NPC can be.")]
        public short BanditMin = 5;

        [MaintainOrder]
        [SettingName("Bandit Max Level")]
        [Tooltip("Set the maximum level a bandit NPC can be.")]
        public short BanditMax = 15;

        [MaintainOrder]
        [SettingName("Warrior Min Level")]
        [Tooltip("Sets the minimum level for NPCs with combat heavy classes (Knight,Warrior,Champion, Battlemage, etc).")]
        public short WarriorMin = 6;
    }

    public class RaceSettings
    {
        [MaintainOrder]
        [SettingName("Imperial Bonus")]
        [Tooltip("Applies the following bonus adjustment at the end of level calculation.")]
        public short ImperialLevel = 0;

        [MaintainOrder]
        [SettingName("Redguard Bonus")]
        [Tooltip("Applies the following bonus adjustment at the end of level calculation.")]
        public short RedguardLevel = 0;

        [MaintainOrder]
        [SettingName("Orc Bonus")]
        [Tooltip("Applies the following bonus adjustment at the end of level calculation.")]
        public short OrcLevel = 3;

        [MaintainOrder]
        [SettingName("Dark Elf Bonus")]
        [Tooltip("Applies the following bonus adjustment at the end of level calculation.")]
        public short DarkElfLevel = 0;

        [MaintainOrder]
        [SettingName("High Elf Bonus")]
        [Tooltip("Applies the following bonus adjustment at the end of level calculation.")]
        public short HighElfLevel = 0;

        [MaintainOrder]
        [SettingName("Khajiit Bonus")]
        [Tooltip("Applies the following bonus adjustment at the end of level calculation.")]
        public short KhajiitLevel = 0;

        [MaintainOrder]
        [SettingName("Wood Elf Bonus")]
        [Tooltip("Applies the following bonus adjustment at the end of level calculation.")]
        public short WoodElfLevel = 0;

        [MaintainOrder]
        [SettingName("Breton Bonus")]
        [Tooltip("Applies the following bonus adjustment at the end of level calculation.")]
        public short BretonLevel = 0;

        [MaintainOrder]
        [SettingName("Nord Bonus")]
        [Tooltip("Applies the following bonus adjustment at the end of level calculation.")]
        public short NordLevel = 0;

        [MaintainOrder]
        [SettingName("Argonian Bonus")]
        [Tooltip("Applies the following bonus adjustment at the end of level calculation.")]
        public short ArgonianLevel = 0;

        [MaintainOrder]
        [SettingName("Dremora Bonus")]
        [Tooltip("Applies the following bonus adjustment at the end of level calculation.")]
        public short DremoraLevel = 3;

        [MaintainOrder]
        [SettingName("Golden Saint Bonus")]
        [Tooltip("Applies the following bonus adjustment at the end of level calculation.")]
        public short GoldenSaintLevel = 6;

        [MaintainOrder]
        [SettingName("Dark Seducer Bonus")]
        [Tooltip("Applies the following bonus adjustment at the end of level calculation.")]
        public short DarkSeducerLevel = 6;
    }

    public class FactionSettings
    {
        [MaintainOrder]
        [SettingName("Fighters Guild Min Level")]
        [Tooltip("Sets the minimum level for NPCs in the Fighters Guild faction.")]
        public short FightersGuildMin = 5;

        [MaintainOrder]
        [SettingName("Mages Guild Min Level")]
        [Tooltip("Sets the minimum level for NPCs in the Mages Guild faction.")]
        public short MagesGuildMin = 5;

        [MaintainOrder]
        [SettingName("Dark Brotherhood Min Level")]
        [Tooltip("Sets the minimum level for NPCs in the Dark Brotherhood faction.")]
        public short DarkBrotherhoodMin = 10;

        [MaintainOrder]
        [SettingName("Vampire Min Level")]
        [Tooltip("Sets the minimum level for NPCs in the Vampire faction.")]
        public short VampireMin = 12;

        [MaintainOrder]
        [SettingName("Thieves Guild Min Level")]
        [Tooltip("Sets the minimum level for NPCs in the Vampire faction.")]
        public short ThievesGuildMin = 5;

        [MaintainOrder]
        [SettingName("Mythic Dawn Min Level")]
        [Tooltip("Sets the minimum level for NPCs in the Mythic Dawn faction.")]
        public short MythicDawnMin = 5;

        [MaintainOrder]
        [SettingName("Forces of Order Min Level")]
        [Tooltip("Sets the minimum level for NPCs in the Forces of Order faction.")]
        public short ForcesOrderMin = 12;

        [MaintainOrder]
        [SettingName("Knights of the Nine Min Level")]
        [Tooltip("Sets the minimum level for NPCs in the Knights of the Nine faction.")]
        public short KnightsNineMin = 10;
    }

    public class CreatureFactionSettings
    {
        [MaintainOrder]
        [SettingName("Lich Min Level")]
        [Tooltip("Sets the minimum level for creatures in the Lich faction.")]
        public short LichMin = 35;
    }
}
