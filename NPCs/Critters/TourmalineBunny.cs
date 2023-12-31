﻿using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;

namespace InverseMod.NPCs.Critters
{
    public class TourmalineBunny : ModNPC
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Tourmaline Bunny");
            Main.npcFrameCount[NPC.type] = Main.npcFrameCount[NPCID.Bunny];
        }

        public override void SetDefaults()
        {
            NPC.width = 20;
            NPC.height = 26;
            NPC.damage = 0;
            NPC.defense = 0;
            NPC.lifeMax = 5;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.knockBackResist = 0.5f;
            NPC.friendly = false;
            NPC.dontTakeDamageFromHostiles = false;
            NPC.aiStyle = 7;
            NPC.catchItem = (short)ModContent.ItemType<Items.Consumables.TourmalineBunny>();
            AIType = NPCID.Bunny;
            AnimationType = NPCID.Bunny;
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (Main.hardMode)
            {
                return SpawnCondition.Cavern.Chance * 0.05f;
            }
            else
            {
                return 0;
            }
        }
        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Caverns,

                new FlavorTextBestiaryInfoElement("These colorful rabbits made their way so deep, their reaction to magicked gems resulted in a coveted sparkling appearance."),
            });
        }
        public override void OnKill()
        {
            // Create 20 dust particles at the NPC's position
            for (int i = 0; i < 20; i++)
            {
                int dustType;
                switch (Main.rand.Next(10))
                {
                    case 0:
                        dustType = DustID.GemAmethyst;
                        break;
                    default:
                        dustType = DustID.GemEmerald;
                        break;
                }
                int dustIndex = Dust.NewDust(NPC.position, NPC.width, NPC.height, dustType, 0f, 0f, 100, default, 1f);
                Main.dust[dustIndex].velocity *= 1.4f;
            }
        }
    }
}