using InverseMod.Common.Systems;
using InverseMod.Dusts;
using InverseMod.Projectiles.Magic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Media;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace InverseMod.Projectiles.Melee
{
    public class GrapheneSaberstaffProjectile : ModProjectile
    {
        private const float SwingRadius = 0; // Radius of the swing arc
        private const float SwingDuration = 23; // Duration of the swing in frames
        private readonly int[] elementDustIndices = { 185, 259, 229, 73 };

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

            Projectile.frameCounter = 0; // Counts ticks for frame changes
            Projectile.frame = 0; // Current frame
            Main.projFrames[Projectile.type] = 16; // Total number of frames in the texture

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

            Lighting.AddLight(Projectile.Center, 1f, 1f, 1f);

            Projectile.frameCounter++;
            if (Projectile.frameCounter >= 4) // Change this value to adjust animation speed
            {
                Projectile.frameCounter = 0;
                Projectile.frame++;
                if (Projectile.frame >= Main.projFrames[Projectile.type])
                {
                    Projectile.frame = 0; // Loop the animation
                }
            }

            if (Main.rand.NextBool(2))
            {
                int numToSpawn = Main.rand.Next(3);
                for (int i = 0; i < numToSpawn; i++)
                {
                    int dustID = 185;
                    Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, dustID, Projectile.velocity.X * 0f, Projectile.velocity.Y * 0.1f,
                        0, default, .5f);
                }
                for (int i = 0; i < numToSpawn; i++)
                {
                    int dustID = 259;
                    Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, dustID, Projectile.velocity.X * 0f, Projectile.velocity.Y * 0.1f,
                        0, default, .5f);
                }
                for (int i = 0; i < numToSpawn; i++)
                {
                    int dustID = 229;
                    Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, dustID, Projectile.velocity.X * 0f, Projectile.velocity.Y * 0.1f,
                        0, default, .5f);
                }
                for (int i = 0; i < numToSpawn; i++)
                {
                    int dustID = 73;
                    Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, dustID, Projectile.velocity.X * 0f, Projectile.velocity.Y * 0.1f,
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

            int explosion = Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, Vector2.Zero, ModContent.ProjectileType<ElementExplosion>(), (int)(Projectile.damage * 1.5), Projectile.knockBack * 2, Projectile.owner);
            Main.projectile[explosion].DamageType = DamageClass.Melee;

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

            // Play the sound with the adjusted pitch
            SoundEngine.PlaySound(hitSoundWithPitch, Projectile.position);

            base.OnHitNPC(target, hit, damageDone);
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D texture = Terraria.GameContent.TextureAssets.Projectile[Projectile.type].Value;
            int frameHeight = texture.Height / Main.projFrames[Projectile.type];
            Rectangle sourceRectangle = new Rectangle(0, Projectile.frame * frameHeight, texture.Width, frameHeight);
            Vector2 origin = sourceRectangle.Size() / 2f;

            // Adjust position as needed, you might want to offset it so the projectile rotates around a specific point
            Vector2 position = Projectile.Center - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY);

            Main.EntitySpriteDraw(texture, position, sourceRectangle, lightColor, Projectile.rotation, origin, Projectile.scale, SpriteEffects.None, 0);

            return false; // Return false because we manually drew the projectile
        }
    }
}