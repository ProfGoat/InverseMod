using InverseMod.Core.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace InverseMod.SceneEffects
{
    public class InverseSoundtrack : ModSceneEffect
    {
        public override bool IsSceneEffectActive(Player player)
        {
            // The alternate soundtrack will be active when the player has enabled it in the configuration
            return ModContent.GetInstance<MenuConfig>().UseAlternateSoundtrack;
        }

        public override SceneEffectPriority Priority => SceneEffectPriority.BiomeHigh;
    }
    public class OverworldDaySoundtrack : InverseSoundtrack
    {
        public override bool IsSceneEffectActive(Player player)
        {
            // The alternate soundtrack will be active when the player has enabled it in the configuration
            // it's currently nighttime in the game, and the player is at overworld height.
            return ModContent.GetInstance<MenuConfig>().UseAlternateSoundtrack && Main.dayTime && player.ZoneOverworldHeight && player.ZoneForest;
        }

        public override int Music => MusicLoader.GetMusicSlot(Mod, "Assets/Music/Yad"); // Change this to your alternate soundtrack

        public override SceneEffectPriority Priority => SceneEffectPriority.BiomeHigh;
    }

    public class OverworldNightSoundtrack : InverseSoundtrack
    {
        public override bool IsSceneEffectActive(Player player)
        {
            // The alternate soundtrack will be active when the player has enabled it in the configuration
            // it's currently nighttime in the game, and the player is at overworld height.
            return ModContent.GetInstance<MenuConfig>().UseAlternateSoundtrack && ((!Main.dayTime && player.ZoneOverworldHeight && player.ZoneForest) || (!Main.dayTime && player.ZoneHallow));
        }

        public override int Music => MusicLoader.GetMusicSlot(Mod, "Assets/Music/Thgin"); // Change this to your alternate soundtrack

        public override SceneEffectPriority Priority => SceneEffectPriority.BiomeHigh;
    }
    public class BloodMoonSoundtrack : InverseSoundtrack
    {
        public override bool IsSceneEffectActive(Player player)
        {
            // The alternate soundtrack will be active when the player has enabled it in the configuration
            // it's currently nighttime in the game, and the player is at overworld height.
            return ModContent.GetInstance<MenuConfig>().UseAlternateSoundtrack && (player.ZoneMeteor || Main.bloodMoon);
        }

        public override int Music => MusicLoader.GetMusicSlot(Mod, "Assets/Music/Eiree"); // Change this to your alternate soundtrack

        public override SceneEffectPriority Priority => SceneEffectPriority.Event;
    }
    public class Boss1Soundtrack : InverseSoundtrack
    {
        public override bool IsSceneEffectActive(Player player)
        {
            return ModContent.GetInstance<MenuConfig>().UseAlternateSoundtrack && (NPC.AnyNPCs(NPCID.EyeofCthulhu) || NPC.AnyNPCs(NPCID.KingSlime) || NPC.AnyNPCs(NPCID.SkeletronHead) || NPC.AnyNPCs(NPCID.EaterofWorldsHead) || NPC.AnyNPCs(NPCID.SkeletronPrime));
        }

        public override int Music => MusicLoader.GetMusicSlot(Mod, "Assets/Music/1Ssob"); // Change this to your alternate soundtrack

        public override SceneEffectPriority Priority => SceneEffectPriority.BossHigh;
    }
    public class UndergroundSoundtrack : InverseSoundtrack
    {
        public override bool IsSceneEffectActive(Player player)
        {
            // The alternate soundtrack will be active when the player has enabled it in the configuration
            // it's currently nighttime in the game, and the player is at overworld height.
            return ModContent.GetInstance<MenuConfig>().UseAlternateSoundtrack && player.ZoneNormalUnderground;
        }

        public override int Music => MusicLoader.GetMusicSlot(Mod, "Assets/Music/DnuorgRednu"); // Change this to your alternate soundtrack

        public override SceneEffectPriority Priority => SceneEffectPriority.BiomeHigh;
    }
    public class JungleSoundtrack : InverseSoundtrack
    {
        public override bool IsSceneEffectActive(Player player)
        {
            // The alternate soundtrack will be active when the player has enabled it in the configuration
            // it's currently nighttime in the game, and the player is at overworld height.
            return ModContent.GetInstance<MenuConfig>().UseAlternateSoundtrack && player.ZoneJungle;
        }

        public override int Music => MusicLoader.GetMusicSlot(Mod, "Assets/Music/Elgnuj"); // Change this to your alternate soundtrack

        public override SceneEffectPriority Priority => SceneEffectPriority.BiomeHigh;
    }
    public class CorruptionSoundtrack : InverseSoundtrack
    {
        public override bool IsSceneEffectActive(Player player)
        {
            // The alternate soundtrack will be active when the player has enabled it in the configuration
            // it's currently nighttime in the game, and the player is at overworld height.
            return ModContent.GetInstance<MenuConfig>().UseAlternateSoundtrack && player.ZoneCorrupt && player.ZoneOverworldHeight;
        }

        public override int Music => MusicLoader.GetMusicSlot(Mod, "Assets/Music/Noitpurroc"); // Change this to your alternate soundtrack

        public override SceneEffectPriority Priority => SceneEffectPriority.BiomeHigh;
    }
    public class UndergroundCorruptionSoundtrack : InverseSoundtrack
    {
        public override bool IsSceneEffectActive(Player player)
        {
            // The alternate soundtrack will be active when the player has enabled it in the configuration
            // it's currently nighttime in the game, and the player is at overworld height.
            return ModContent.GetInstance<MenuConfig>().UseAlternateSoundtrack && player.ZoneCorrupt && (player.ZoneDirtLayerHeight || player.ZoneRockLayerHeight);
        }

        public override int Music => MusicLoader.GetMusicSlot(Mod, "Assets/Music/NoitpurrocDnuorgRednu"); // Change this to your alternate soundtrack

        public override SceneEffectPriority Priority => SceneEffectPriority.BiomeHigh;
    }
    public class HallowSoundtrack : InverseSoundtrack
    {
        public override bool IsSceneEffectActive(Player player)
        {
            // The alternate soundtrack will be active when the player has enabled it in the configuration
            // it's currently nighttime in the game, and the player is at overworld height.
            return ModContent.GetInstance<MenuConfig>().UseAlternateSoundtrack && player.ZoneHallow && Main.dayTime && player.ZoneOverworldHeight;
        }

        public override int Music => MusicLoader.GetMusicSlot(Mod, "Assets/Music/Wollah"); // Change this to your alternate soundtrack

        public override SceneEffectPriority Priority => SceneEffectPriority.BiomeHigh;
    }
    public class UndergroundHallowSoundtrack : InverseSoundtrack
    {
        public override bool IsSceneEffectActive(Player player)
        {
            // The alternate soundtrack will be active when the player has enabled it in the configuration
            // it's currently nighttime in the game, and the player is at overworld height.
            return ModContent.GetInstance<MenuConfig>().UseAlternateSoundtrack && player.ZoneHallow && (player.ZoneDirtLayerHeight || player.ZoneRockLayerHeight);
        }

        public override int Music => MusicLoader.GetMusicSlot(Mod, "Assets/Music/WollahDuorgrednu"); // Change this to your alternate soundtrack

        public override SceneEffectPriority Priority => SceneEffectPriority.BiomeHigh;
    }
    public class Boss2Soundtrack : InverseSoundtrack
    {
        public override bool IsSceneEffectActive(Player player)
        {
            return ModContent.GetInstance<MenuConfig>().UseAlternateSoundtrack && (NPC.AnyNPCs(NPCID.WallofFlesh) || NPC.AnyNPCs(NPCID.Spazmatism) || NPC.AnyNPCs(NPCID.Retinazer));
        }

        public override int Music => MusicLoader.GetMusicSlot(Mod, "Assets/Music/2Ssob"); // Change this to your alternate soundtrack

        public override SceneEffectPriority Priority => SceneEffectPriority.BossHigh;
    }
    public class Boss3Soundtrack : InverseSoundtrack
    {
        public override bool IsSceneEffectActive(Player player)
        {
            return ModContent.GetInstance<MenuConfig>().UseAlternateSoundtrack && (NPC.AnyNPCs(NPCID.BrainofCthulhu) || NPC.AnyNPCs(NPCID.TheDestroyer) || NPC.AnyNPCs(NPCID.TorchGod) || Main.invasionType == 2);
        }

        public override int Music => MusicLoader.GetMusicSlot(Mod, "Assets/Music/3Ssob"); // Change this to your alternate soundtrack

        public override SceneEffectPriority Priority => SceneEffectPriority.BossHigh;
    }
    public class SnowSoundtrack : InverseSoundtrack
    {
        public override bool IsSceneEffectActive(Player player)
        {
            // The alternate soundtrack will be active when the player has enabled it in the configuration
            // it's currently nighttime in the game, and the player is at overworld height.
            return ModContent.GetInstance<MenuConfig>().UseAlternateSoundtrack && player.ZoneOverworldHeight && player.ZoneSnow;
        }

        public override int Music => MusicLoader.GetMusicSlot(Mod, "Assets/Music/Wons"); // Change this to your alternate soundtrack

        public override SceneEffectPriority Priority => SceneEffectPriority.BiomeHigh;
    }
    public class SpaceNightSoundtrack : InverseSoundtrack
    {
        public override bool IsSceneEffectActive(Player player)
        {
            // The alternate soundtrack will be active when the player has enabled it in the configuration
            // it's currently nighttime in the game, and the player is at overworld height.
            return ModContent.GetInstance<MenuConfig>().UseAlternateSoundtrack && !Main.dayTime && player.ZoneNormalSpace;
        }

        public override int Music => MusicLoader.GetMusicSlot(Mod, "Assets/Music/ThginEcaps"); // Change this to your alternate soundtrack

        public override SceneEffectPriority Priority => SceneEffectPriority.BiomeHigh;
    }
    public class CrimsonSoundtrack : InverseSoundtrack
    {
        public override bool IsSceneEffectActive(Player player)
        {
            // The alternate soundtrack will be active when the player has enabled it in the configuration
            // it's currently nighttime in the game, and the player is at overworld height.
            return ModContent.GetInstance<MenuConfig>().UseAlternateSoundtrack && Main.dayTime && player.ZoneOverworldHeight && player.ZoneCrimson;
        }

        public override int Music => MusicLoader.GetMusicSlot(Mod, "Assets/Music/Nosmirc"); // Change this to your alternate soundtrack

        public override SceneEffectPriority Priority => SceneEffectPriority.BiomeHigh;
    }
    public class Boss4Soundtrack : InverseSoundtrack
    {
        public override bool IsSceneEffectActive(Player player)
        {
            return ModContent.GetInstance<MenuConfig>().UseAlternateSoundtrack && (NPC.AnyNPCs(NPCID.Golem) || NPC.AnyNPCs(NPCID.CultistBoss) || Main.invasionType == 3);
        }

        public override int Music => MusicLoader.GetMusicSlot(Mod, "Assets/Music/4Ssob"); // Change this to your alternate soundtrack

        public override SceneEffectPriority Priority => SceneEffectPriority.BossHigh;
    }
}