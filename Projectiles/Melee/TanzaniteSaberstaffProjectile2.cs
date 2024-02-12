using InverseMod.Common.Systems;
using InverseMod.Dusts;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace InverseMod.Projectiles.Melee
{
    public class TanzaniteSaberstaffProjectile2 : ModProjectile
    {
        private const float OutwardTime = 30f; // Time in ticks before the boomerang starts returning
        private const float CatchDistance = 48f; // Distance from the player at which the projectile is considered caught
        private const float SpinRate = 0.2f; // Rotation in radians per tick

        public override void SetDefaults()
        {
            Projectile.width = 98; // Adjust as needed
            Projectile.height = 98; // Adjust as needed
            Projectile.aiStyle = 0; // No default AI style
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.ignoreWater = true;
            Projectile.extraUpdates = 1; // For smoother animation
        }

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];

            // Increment the timer
            Projectile.ai[0] += 1f;

            Lighting.AddLight(Projectile.Center, 0f, 0f, 1f);

            if (Main.rand.NextBool(2))
            {
                int numToSpawn = Main.rand.Next(3);
                for (int i = 0; i < numToSpawn; i++)
                {
                    int dustType = DustID.GemSapphire;
                    Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, dustType, Projectile.velocity.X * 0f, Projectile.velocity.Y * 0.1f,
                        0, default, .5f);
                }
            }

            if (Projectile.ai[0] >= OutwardTime)
            {
                // Calculate direction from projectile to player
                Vector2 directionToPlayer = player.Center - Projectile.Center;
                float distanceToPlayer = directionToPlayer.Length();

                // Normalize the direction vector, then adjust the velocity of the projectile to move towards the player
                if (distanceToPlayer > CatchDistance)
                {
                    directionToPlayer.Normalize();
                    directionToPlayer *= 14f; // Adjust this speed as needed

                    // Make the projectile's velocity interpolate towards the player's position, making it return
                    Projectile.velocity = (Projectile.velocity * 0.95f) + (directionToPlayer * 0.05f);
                }
                else
                {
                    // If the projectile is close enough to the player, kill it (considered caught)
                    Projectile.Kill();
                }
            }

            // Spin the projectile
            Projectile.rotation += SpinRate;

            // Optionally, if you want the projectile to spin in the direction of its movement, you can combine the spin with its current rotation like this:
            // Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2 + SpinRate * Projectile.ai[0];
            // This will make the projectile spin faster over time, which might not be what you want, so adjust as necessary.
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (Main.rand.NextBool(4))
                target.AddBuff(BuffID.Wet, 180);
            SoundEngine.PlaySound(rorAudio.Hit);
            base.OnHitNPC(target, hit, damageDone);
        }
    }
}
