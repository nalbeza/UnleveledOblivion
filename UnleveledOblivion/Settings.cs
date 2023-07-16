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
        public short GrandSoulOffset = 30;

        [MaintainOrder]
        [SettingName("Scaled Below Player Offset")]
        [Tooltip("Adds an additional offset to creatures that are scaled below the player in vanilla Oblivion.")]
        public short ScaledBelowPlayerOffset = -3;

        [MaintainOrder]
        [SettingName("Scaled Above Player Offset")]
        [Tooltip("Adds an additional offset to creatures that are scaled above the player in vanilla Oblivion.")]
        public short ScaledAbovePlayerOffset = 10;
    }

    public class NPCSettings 
    {
        [MaintainOrder]
        [SettingName("Base Level")]
        [Tooltip("The base level of every npc before adjustments. Four is recommended but this can be increased to make the game more difficult.")]
        public short BaseLevel = 4;

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
    }

    public class ClassSettings
    {
        [MaintainOrder]
        [SettingName("Guard Level")]
        [Tooltip("Set the default level of guards and soldier NPCs. This is their actual level rather than an offset.")]
        public short GuardLevel = 8;
    }
}
