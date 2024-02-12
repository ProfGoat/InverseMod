using InverseMod.Common.Systems;
using InverseMod.Dusts;
using InverseMod.Projectiles.Magic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace InverseMod.Projectiles.Melee
{
    public class GrapheneSaberstaffProjectile2 : ModProjectile
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

            Projectile.frameCounter = 0; // Counts ticks for frame changes
            Projectile.frame = 0; // Current frame
            Main.projFrames[Projectile.type] = 16; // Total number of frames in the texture
        }

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];

            Vector2 orbitCenter = player.Center;
            float rotation = Projectile.ai[1];

            // Increment the timer
            Projectile.ai[0] += 1f;

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

            if (Projectile.ai[0] >= OutwardTime)
            {
                // Calculate direction from projectile to player
                Vector2 directionToPlayer = player.Center - Projectile.Center;
                float distanceToPlayer = directionToPlayer.Length();

                // Normalize the direction vector, then adjust the velocity of the projectile to move towards the player
                if (distanceToPlayer > CatchDistance)
                {
                    directionToPlayer.Normalize();
                    directionToPlayer *= 22f; // Adjust this speed as needed

                    // Make the projectile's velocity interpolate towards the player's position, making it return
                    Projectile.velocity = (Projectile.velocity * 0.95f) + (directionToPlayer * 0.05f);

                    if (Main.mouseRight)
                    {
                        orbitCenter = Main.MouseWorld;

                        Projectile.Center = orbitCenter;
                    }
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
            int explosion = Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, Vector2.Zero, ModContent.ProjectileType<ElementExplosion>(), (int)(Projectile.damage * 1.5), Projectile.knockBack * 2, Projectile.owner);
            Main.projectile[explosion].DamageType = DamageClass.Melee;
            SoundEngine.PlaySound(rorAudio.Hit);
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
