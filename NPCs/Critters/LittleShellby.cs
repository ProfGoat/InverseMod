using InverseMod.Common.Systems;
using InverseMod.BossBars;
using InverseMod.Items;
using InverseMod.Items.Consumables;
using InverseMod.Projectiles;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;

namespace InverseMod.NPCs.Critters
{
    // https://github.com/tModLoader/tModLoader/wiki/Advanced-Vanilla-Code-Adaption#example-npc-npc-clone-with-modified-projectile-hoplite
    public class LittleShellby : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Little Shellby");

            Main.npcFrameCount[Type] = 10;

            NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
            {
                Velocity = 1f
            };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, value);
        }

        public override void SetDefaults()
        {
            NPC.width = 16;
            NPC.height = 12;
            NPC.damage = 1;
            NPC.defense = 0;
            NPC.lifeMax = 15;
            NPC.HitSound = SoundID.NPCHit38;
            NPC.DeathSound = SoundID.NPCDeath41;
            NPC.value = 2f;
            NPC.knockBackResist = 0f;
            NPC.aiStyle = 39;

            AIType = NPCID.GiantShelly;
            AnimationType = NPCID.GiantShelly;
            Banner = Item.NPCtoBanner(NPCID.GiantShelly);
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ItemID.MagicConch, 50));
            npcLoot.Add(ItemDropRule.Common(ItemID.DemonConch, 50));
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            return SpawnCondition.Cavern.Chance * 0.1f;
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Caverns,

				new FlavorTextBestiaryInfoElement("Just a little guy. He won't hurtcha. Not really anyways."),
            });
        }
    }
}