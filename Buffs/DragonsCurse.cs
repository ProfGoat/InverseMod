using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace InverseMod.Buffs
{
    public class DragonsCurse : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.debuff[Type] = true;
            Main.pvpBuff[Type] = true;
            Main.buffNoSave[Type] = true;
        }
        public override void Update(NPC npc, ref int buffIndex)
        {
            // Check if it's time to apply damage (every 60 frames / 1 second)
            if (npc.buffTime[buffIndex] % 60 == 0)
            {
                // Calculate damage as 1% of max health, at least 1 damage
                int damage = Math.Max(1, (int)(npc.lifeMax * 0.03f));

                // Directly reduce NPC health, bypassing invincibility frames
                npc.life -= damage;

                // If the damage kills the NPC, make sure it's properly killed and drops loot
                if (npc.life <= 0)
                {
                    npc.StrikeInstantKill(); // Large damage to ensure death
                }

                // Play hit effect and create combat text (optional)
                npc.HitEffect();
                CombatText.NewText(npc.Hitbox, Microsoft.Xna.Framework.Color.OrangeRed, damage.ToString());
            }
            for (int i = 0; i < 3; i++) // Spawn 3 dust particles for effect
            {
                Vector2 position = npc.Center + new Vector2(Main.rand.Next(-15, 16), Main.rand.Next(-20, 21)); // Random position near the NPC
                Vector2 velocity = new Vector2(0, Main.rand.NextFloat(-1f, -4f)); // Upward velocity
                Dust dust = Dust.NewDustPerfect(position, DustID.GemEmerald, velocity, 100, Color.White, 1f);
                dust.noGravity = true; // Dust will float upwards
            }
        }
        public override void Update(Player player, ref int buffIndex)
        {
            // Check if it's time to apply damage (every 60 frames / 1 second)
            if (player.buffTime[buffIndex] % 60 == 0)
            {
                // Calculate damage as 1% of max health, at least 1 damage
                int damage = Math.Max(1, (int)(player.statLifeMax * 0.07f));

                // Directly reduce NPC health, bypassing invincibility frames
                player.statLife -= damage;

                // If the damage kills the NPC, make sure it's properly killed and drops loot
                if (player.statLife <= 0)
                {
                    player.KillMe(PlayerDeathReason.ByCustomReason($"{player.name} was swallowed by the Dragon's Curse."), 999, 0); // Large damage to ensure death
                }
            }

            for (int i = 0; i < 3; i++) // Spawn 3 dust particles for effect
            {
                Vector2 position = player.Center + new Vector2(Main.rand.Next(-15, 16), Main.rand.Next(-20, 21)); // Random position near the NPC
                Vector2 velocity = new Vector2(0, Main.rand.NextFloat(-1f, -4f)); // Upward velocity
                Dust dust = Dust.NewDustPerfect(position, DustID.GemEmerald, velocity, 100, Color.White, 1f);
                dust.noGravity = true; // Dust will float upwards
            }

            if (player.buffTime[buffIndex] % 60 == 0) // Ensure it aligns with the damage tick
            {
                int damage = Math.Max(1, (int)(player.statLifeMax2 * 0.07f)); // Calculate the damage as 1% of the player's max health
                CombatText.NewText(player.getRect(), Color.OrangeRed, damage.ToString(), true);
            }
        }
    }
}
