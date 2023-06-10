using InverseMod.Dusts;
using Microsoft.Xna.Framework;
using System;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Humanizer;
using System.Numerics;
using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace InverseMod.Projectiles.Magic
{
    public class TourmalineTwinfireProjectile : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Tourmaline Helix");

            Main.projFrames[Projectile.type] = 27;
        }

        public override void SetDefaults()
        {
            Projectile.width = 108;
            Projectile.height = 36;

            Projectile.aiStyle = 0;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.ignoreWater = false;
            Projectile.penetrate = 8;
            Projectile.tileCollide = true;
            Projectile.timeLeft = 1000;
        }

        private int frameCounter = 0;
        public override void AI()
        {
            Player player = Main.player[Projectile.owner];

            // Increase the distance from the player over time
            Projectile.ai[1] += 6.1f;

            // Update the projectile's position based on the player's position and mouse direction
            Vector2 directionToMouse = Main.MouseWorld - player.Center;
            directionToMouse.Normalize();

            // Set the projectile's position to a point along the line from the player to the mouse
            // The distance is controlled by Projectile.ai[1]
            Projectile.Center = player.Center + directionToMouse * Projectile.ai[1];

            // Calculate rotation that points from the player to the projectile
            Vector2 directionFromPlayer = Projectile.Center - player.Center;
            Projectile.rotation = directionFromPlayer.ToRotation() - (float)Math.PI / 2;

            Lighting.AddLight(Projectile.Center, new Microsoft.Xna.Framework.Vector3(0.4f, 0f, 0.4f));

            Lighting.AddLight(Projectile.Center, new Microsoft.Xna.Framework.Vector3(0, 0.8f, 0));
            // Animate the projectile
            frameCounter++;
            if (frameCounter >= 2) // Change the frame every 2 ticks
            {
                frameCounter = 0;
                Projectile.frame++;
                if (Projectile.frame >= 27) // If the frame is greater than or equal to the total number of frames, reset to the first frame
                {
                    Projectile.frame = 0;
                }
            }
        }

        public override bool PreDraw(ref Color lightColor)
        {
            // SpriteEffects helps to flip texture horizontally and vertically
            SpriteEffects spriteEffects = SpriteEffects.None;
            if (Projectile.spriteDirection == -1)
                spriteEffects = SpriteEffects.FlipHorizontally;

            // Getting texture of projectile
            Texture2D texture = (Texture2D)ModContent.Request<Texture2D>(Texture);

            // Calculating frameHeight and current Y pos dependence of frame
            // If texture without animation frameHeight is always texture.Height and startY is always 0
            int frameHeight = texture.Height / Main.projFrames[Projectile.type];
            int startY = frameHeight * Projectile.frame;

            // Get this frame on texture
            Rectangle sourceRectangle = new Rectangle(0, startY, texture.Width, frameHeight);

            // Alternatively, you can skip defining frameHeight and startY and use this:
            // Rectangle sourceRectangle = texture.Frame(1, Main.projFrames[Projectile.type], frameY: Projectile.frame);

            Vector2 origin = sourceRectangle.Size() / 2f;

            // If image isn't centered or symmetrical you can specify origin of the sprite
            // (0,0) for the upper-left corner
            float offsetX = 20;
            origin.X = Projectile.spriteDirection == 1 ? sourceRectangle.Width - offsetX : offsetX;

            // If sprite is vertical
            // float offsetY = 20f;
            // origin.Y = (float)(Projectile.spriteDirection == 1 ? sourceRectangle.Height - offsetY : offsetY);


            // Applying lighting and draw current frame
            Color drawColor = Projectile.GetAlpha(lightColor);
            Main.EntitySpriteDraw(texture,
                Projectile.Center - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY),
                sourceRectangle, drawColor, Projectile.rotation, origin, Projectile.scale, spriteEffects, 0);

            // It's important to return false, otherwise we also draw the original texture.
            return false;
        }


        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            // Destroy the projectile when it hits a tile
            Projectile.Kill();
            return false;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if (Main.rand.NextBool(4))
                target.AddBuff(BuffID.ShadowFlame, 180);
            target.AddBuff(BuffID.CursedInferno, 180);
        }
    }
}