//using InverseMod.Common.Systems;
//using InverseMod.BossBars;
//using InverseMod.Items;
//using InverseMod.NPCs;
//using InverseMod.Projectiles;
//using Microsoft.Xna.Framework;
//using System;
//using System.Collections.Generic;
//using Terraria;
//using Terraria.Audio;
//using Terraria.DataStructures;
//using Terraria.GameContent.Bestiary;
//using Terraria.GameContent.ItemDropRules;
//using Terraria.ID;
//using Terraria.ModLoader;
//using Terraria.ModLoader.Utilities;

//namespace InverseMod.NPCs.Enemies
//{
//    // https://github.com/tModLoader/tModLoader/wiki/Advanced-Vanilla-Code-Adaption#example-npc-npc-clone-with-modified-projectile-hoplite
//    public class Obamna : ModNPC
//    {
//        public override void SetStaticDefaults()
//        {
//            DisplayName.SetDefault("Obamna");

//            Main.npcFrameCount[Type] = 2;

//            NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
//            {
//                Velocity = 1f
//            };
//            NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, value);
//        }

//        public override void SetDefaults()
//        {
//            NPC.width = 200;
//            NPC.height = 200;
//            NPC.damage = 69;
//            NPC.defense = 69;
//            NPC.lifeMax = 420;
//            NPC.value = 69f;
//            NPC.HitSound = SoundID.Item104;
//            NPC.DeathSound = SoundID.Item112;
//            NPC.knockBackResist = 69f;
//            NPC.aiStyle = 11;
//        }

//        public override void ModifyNPCLoot(NPCLoot npcLoot)
//        {
//        }

//        public override float SpawnChance(NPCSpawnInfo spawnInfo)
//        {
//            return SpawnCondition.Sky.Chance * 0.1f;
//        }

//        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
//        {
//            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
//                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Sky,

//                new FlavorTextBestiaryInfoElement("Obamna."),
//            });
//        }
//    }
//}