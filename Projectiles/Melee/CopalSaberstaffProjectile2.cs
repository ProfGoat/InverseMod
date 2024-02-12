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
using Terraria.Graphics.Effects;
using Terraria.ID;
using Terraria.ModLoader;

namespace InverseMod.Projectiles.Melee
{
    public class CopalSaberstaffProjectile2 : ModProjectile
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

            Lighting.AddLight(Projectile.Center, 20.4f, 3f, 0f);

            if (Main.rand.NextBool(2))
            {
                int numToSpawn = Main.rand.Next(5);
                for (int i = 0; i < numToSpawn; i++)
                {
                    int dustType = DustID.GemTopaz;
                    Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, dustType, Projectile.velocity.X * 0f, Projectile.velocity.Y * 0.1f,
                        0, default, 0.5f);
                }
            }

            float maxDistance = 600f;  // The maximum distance at which enemies are affected
            int immunityTime = 10;  // The number of frames between hits
            foreach (NPC npc in Main.npc)
            {
                if (npc.active && !npc.friendly && !npc.dontTakeDamage)
                {
                    float distance = Vector2.Distance(npc.Center, Projectile.Center);
                    if (distance <= maxDistance)
                    {
                        // Apply the Inferno Debuff
                        npc.AddBuff(BuffID.OnFire, 2 * 60);  // Lasts for 2 seconds

                        // Check whether enough time has passed since the last hit
                        if (Main.GameUpdateCount - npc.ai[3] >= immunityTime)
                        {
                            // Deal damage based on distance
                            float damageFactor = 1 - distance / maxDistance;  // The closer the NPC, the higher the damage factor
                            int damage = (int)(Projectile.damage * damageFactor);
                            npc.SimpleStrikeNPC((int)player.GetDamage(DamageClass.Melee).ApplyTo(damage), 1);

                            // Update the time of the last hit
                            npc.ai[3] = Main.GameUpdateCount;
                        }
                    }
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
                    directionToPlayer *= 20f; // Adjust this speed as needed

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
            const int numGreekFire = 4;
            for (int i = 0; i < numGreekFire; i++)
            {
                // Calculate the angle for this Greek Fire projectile
                double angle = Math.PI * 2 / numGreekFire * i;
                Vector2 velocity = new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle)) * 2f;  // 2f is the speed of the Greek Fire

                // Spawn a new Greek Fire projectile
                int greekFire = Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, velocity, ProjectileID.GreekFire1, Projectile.damage / 2, 0, Projectile.owner);
                Main.projectile[greekFire].DamageType = DamageClass.Magic;
                Main.projectile[greekFire].friendly = true;
                Main.projectile[greekFire].hostile = false;
            }

            SoundEngine.PlaySound(rorAudio.Hit);
            base.OnHitNPC(target, hit, damageDone);
        }
    }
}
