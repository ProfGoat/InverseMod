using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using InverseMod.Items;
using Terraria.GameContent.ItemDropRules;
using InverseMod.Buffs;

namespace InverseMod.NPCs
{
    public class InverseGlobalNPC : GlobalNPC
    {
        public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
        {
            if (npc.type == NPCID.Zombie || npc.type == NPCID.ZombieEskimo || npc.type == NPCID.ZombieRaincoat || npc.type == NPCID.ZombieSuperman || npc.type == NPCID.ZombieSweater || npc.type == NPCID.TorchZombie || npc.type == NPCID.TwiggyZombie || npc.type == NPCID.ArmedZombie || npc.type == NPCID.ArmedZombieEskimo || npc.type == NPCID.BigZombie || npc.type == NPCID.SmallZombie || npc.type == NPCID.BaldZombie || npc.type == NPCID.BigBaldZombie || npc.type == NPCID.ArmedTorchZombie || npc.type == NPCID.FemaleZombie || npc.type == NPCID.SmallFemaleZombie || npc.type == NPCID.BigFemaleZombie || npc.type == NPCID.SmallTwiggyZombie || npc.type == NPCID.BigTwiggyZombie || npc.type == NPCID.SwampZombie || npc.type == NPCID.SmallSwampZombie || npc.type == NPCID.BigSwampZombie)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Items.Weapons.Melee.Shortswords.ZombieHand>(), 1400));
            }
            if (npc.type == NPCID.FlyingAntlion)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Items.Weapons.Melee.SwarmerWing>(), 50));
            }
        }
        public override void ModifyHitPlayer(NPC npc, Player target, ref Player.HurtModifiers modifiers)
        {
            if (npc.type == NPCID.ChaosElemental)
            {
                if (Main.rand.NextBool(2))
                    target.AddBuff(ModContent.BuffType<Levitation>(), 180);
            }
            if (npc.type == NPCID.WyvernBody)
            {
                if (Main.rand.NextBool(1))
                    target.AddBuff(ModContent.BuffType<DragonsCurse>(), 600);
            }
            if (npc.type == NPCID.WyvernHead)
            {
                if (Main.rand.NextBool(1))
                    target.AddBuff(ModContent.BuffType<DragonsCurse>(), 600);
            }
        }
    }
}