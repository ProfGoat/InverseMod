using InverseMod.Common.Systems;
using InverseMod.Dusts;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Media;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace InverseMod.Projectiles.Melee
{
    public class TourmalineSaberstaffProjectile : ModProjectile
    {
        private const float SwingRadius = 0; // Radius of the swing arc
        private const float SwingDuration = 23; // Duration of the swing in frames

        public override void SetDefaults()
        {
            Projectile.width = 98; // Width of the projectile's collision box
            Projectile.height = 98; // Height of the projectile's collision box
            Projectile.aiStyle = 0; // No default AI style
            Projectile.friendly = true; // Can damage enemies
            Projectile.penetrate = -1; // No penetration limit
            Projectile.tileCollide = false; // Does not collide with tiles
            Projectile.DamageType = DamageClass.Melee; // Classify as melee for damage calculation
            Projectile.ignoreWater = true; // Ignores water physics

            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = -1;
        }

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            if (!player.active || player.dead)
            {
                Projectile.Kill();
                return;
            }
            // Ensure the projectile is aligned with the player's hand
            Projectile.Center = player.MountedCenter;

            player.heldProj = Projectile.whoAmI; // Ensures the projectile is drawn in front of the player

            Lighting.AddLight(Projectile.Center, 0.5f, 0f, 0.5f);
            Lighting.AddLight(Projectile.Center, 0f, 1f, 0f);

            if (Main.rand.NextBool(2))
            {
                int numToSpawn = Main.rand.Next(3);
                for (int i = 0; i < numToSpawn; i++)
                {
                    int dustType = DustID.GemAmethyst;
                    Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, dustType, Projectile.velocity.X * 0f, Projectile.velocity.Y * 0.1f,
                        0, default, .5f);
                }
                for (int i = 0; i < numToSpawn; i++)
                {
                    int dustType = DustID.GemEmerald;
                    Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, dustType, Projectile.velocity.X * 0f, Projectile.velocity.Y * 0.1f,
                        0, default, .5f);
                }
            }

            if (Projectile.ai[0] < SwingDuration)
            {
                Projectile.ai[0]++;
                float swingProgress = Projectile.ai[0] / SwingDuration;

                // Define the starting angle for the swing based on the player's direction
                float startingAngle = player.direction > 0 ? MathHelper.PiOver2 : -MathHelper.PiOver2;
                float swingAngle = MathHelper.ToRadians(180) * swingProgress;

                // Apply the rotation considering the starting angle
                float rotation = startingAngle + swingAngle * player.direction;

                Projectile.rotation = rotation;

                // Calculate the position offset for the swinging movement
                Vector2 offset = Vector2.UnitY.RotatedBy(rotation) * SwingRadius;
                Projectile.Center += offset * new Vector2(-1, 1); // Adjust to swing in an arc
            }
            else
            {
                Projectile.Kill();
            }
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            Player player = Main.player[Projectile.owner];

            // Check if the NPC is not a target dummy
            if (target.type != NPCID.TargetDummy && !target.boss)
            {
                // Calculate the direction from the player to the NPC
                Vector2 knockbackDirection = target.Center - player.Center;

                // Normalize the vector to get a unit vector (direction only, length of 1)
                knockbackDirection.Normalize();

                // Set the knockback strength (you can adjust this value as needed)
                float knockbackStrength = 2f; // Example strength, adjust as needed

                // Apply the knockback to the NPC
                target.velocity = knockbackDirection * knockbackStrength;

                // Optional: Add any additional effects upon hitting the NPC here
            }

            // Random pitch adjustment between -0.05 (slightly lower) and +0.05 (slightly higher)
            float pitchOffset = Main.rand.NextFloat(-0.2f, 0.2f);

            // Create a new SoundStyle with the pitch variance
            SoundStyle hitSoundWithPitch = new SoundStyle(rorAudio.Hit.SoundPath)
            {
                Volume = rorAudio.Hit.Volume,
                Pitch = pitchOffset, // Apply the random pitch here
                PitchVariance = 0f, // No additional variance needed since we're setting the pitch directly
                MaxInstances = rorAudio.Hit.MaxInstances,
                Type = rorAudio.Hit.Type,
            };
            if (Main.rand.NextBool(4))
                target.AddBuff(BuffID.ShadowFlame, 180);
            if (Main.rand.NextBool(4))
                target.AddBuff(BuffID.CursedInferno, 180);

            // Play the sound with the adjusted pitch
            SoundEngine.PlaySound(hitSoundWithPitch, Projectile.position);

            base.OnHitNPC(target, hit, damageDone);
        }

        public override bool PreDraw(ref Color lightColor)
        {
            return true;
        }
    }
}