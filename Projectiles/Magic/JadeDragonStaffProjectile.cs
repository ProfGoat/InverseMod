using InverseMod.Items;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace InverseMod.Projectiles.Magic
{
    // This file shows an animated projectile
    // This file also shows advanced drawing to center the drawn projectile correctly
    public class JadeDragonStaffProjectile : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // Total count animation frames
            Main.projFrames[Projectile.type] = 19;
        }

        public override void SetDefaults()
        {
            Projectile.width = 500; // The width of projectile hitbox
            Projectile.height = 40; // The height of projectile hitbox

            Projectile.friendly = true; // Can the projectile deal damage to enemies?
            Projectile.DamageType = DamageClass.Melee; // Is the projectile shoot by a ranged weapon?
            Projectile.ignoreWater = true; // Does the projectile's speed be influenced by water?
            Projectile.tileCollide = false; // Can the projectile collide with tiles?
            Projectile.penetrate = -1; // Look at comments ExamplePiercingProjectile

            Projectile.timeLeft = 300;

            Projectile.alpha = 255; // How transparent to draw this projectile. 0 to 255. 255 is completely transparent.
        }

        // Allows you to determine the color and transparency in which a projectile is drawn
        // Return null to use the default color (normally light and buff color)
        // Returns null by default.
        public override Color? GetAlpha(Color lightColor)
        {
            // return Color.White;
            return new Color(255, 255, 255, 0) * Projectile.Opacity;
        }
        private NPC FindClosestEnemy()
        {
            NPC closestEnemy = null;
            float minDistance = 0; // You can adjust this value according to your desired range
            float maxDistance = 600f;

            foreach (NPC npc in Main.npc)
            {
                if (npc.CanBeChasedBy() && !npc.friendly)
                {
                    // Calculate the position of the front of the projectile
                    Vector2 frontOfProjectile = Projectile.Center + Vector2.Normalize(Projectile.velocity) * (Projectile.width + 250);

                    // Calculate the distance between the front of the projectile and the NPC's center
                    float currentDistance = Vector2.Distance(frontOfProjectile, npc.Center);

                    if (currentDistance < maxDistance || currentDistance > minDistance)
                    {
                        minDistance = currentDistance;
                        maxDistance = currentDistance;
                        closestEnemy = npc;
                    }
                }
            }

            return closestEnemy;
        }

        public override void AI()
        {
            int lightPoints = 20;  // Number of light points you want to create
            Vector2 step = new Vector2(500f / lightPoints, 40f / lightPoints);  // Steps between each light point

            for (int i = 0; i <= lightPoints; i++)
            {
                for (int j = 0; j <= lightPoints; j++)
                {
                    Vector2 lightPosition = Projectile.position + new Vector2(i * step.X, j * step.Y);
                    Lighting.AddLight(lightPosition, new Vector3(0, 0.4f, 0));
                }
            }

            // Fade in and out effect
            FadeInAndOut();

            Player player = Main.player[Projectile.owner];

            // Make the projectile face the correct direction
            Projectile.spriteDirection = Projectile.velocity.X > 0 ? 1 : -1;

            // Find the closest enemy within a certain range
            NPC target = FindClosestEnemy();

            if (target != null) // If an enemy is found
            {
                NPC targetNPC = Main.npc[target.whoAmI];

                // Calculate the direction towards the enemy and move the projectile towards it
                Vector2 directionToEnemy = targetNPC.Center - Projectile.Center;
                float distanceToEnemy = directionToEnemy.Length();
                float speed = 12f;

                // Normalize the direction vector and apply the speed
                directionToEnemy.Normalize();
                directionToEnemy *= speed;

                // Move the projectile towards the enemy
                MoveTowards(Projectile.Center, targetNPC.Center, speed);

                // Animate the projectile
                if (++Projectile.frameCounter >= 3)
                {
                    Projectile.frameCounter = 0;
                    if (++Projectile.frame >= Main.projFrames[Projectile.type])
                    {
                        Projectile.frame = 0;
                    }
                }
            }
            else // If no enemy is found, follow the player
            {
                MoveTowards(Projectile.Center, player.Center, 6f);
            }
        }

        public void MoveTowards(Vector2 currentPosition, Vector2 targetPosition, float speed)
        {
            Vector2 directionToTarget = targetPosition - currentPosition;
            float distanceToTarget = directionToTarget.Length();

            // Normalize the direction vector and apply the speed
            directionToTarget.Normalize();
            directionToTarget *= speed;

            // Update the projectile's velocity
            if (Projectile.velocity.X < directionToTarget.X)
            {
                Projectile.velocity.X += 0.1f;
                if (Projectile.velocity.X < 0 && directionToTarget.X > 0)
                {
                    Projectile.velocity.X += 0.1f;
                }
            }
            else if (Projectile.velocity.X > directionToTarget.X)
            {
                Projectile.velocity.X -= 0.1f;
                if (Projectile.velocity.X > 0 && directionToTarget.X < 0)
                {
                    Projectile.velocity.X -= 0.1f;
                }
            }
            if (Projectile.velocity.Y < directionToTarget.Y)
            {
                Projectile.velocity.Y += 0.1f;
                if (Projectile.velocity.Y < 0 && directionToTarget.Y > 0)
                {
                    Projectile.velocity.Y += 0.1f;
                }
            }
            else if (Projectile.velocity.Y > directionToTarget.Y)
            {
                Projectile.velocity.Y -= 0.1f;
                if (Projectile.velocity.Y > 0 && directionToTarget.Y < 0)
                {
                    Projectile.velocity.Y -= 0.1f;
                }
            }
        }

        // Many projectiles fade in so that when they spawn they don't overlap the gun muzzle they appear from
        public void FadeInAndOut()
        {
            // If last less than 50 ticks — fade in, than more — fade out
            if (Projectile.ai[0] <= 50f)
            {
                // Fade in
                Projectile.alpha -= 25;
                // Cap alpha before timer reaches 50 ticks
                if (Projectile.alpha < 100)
                    Projectile.alpha = 100;

                return;
            }

            // Fade out
            Projectile.alpha += 25;
            // Cal alpha to the maximum 255(complete transparent)
            if (Projectile.alpha > 255)
                Projectile.alpha = 255;
        }

        // Some advanced drawing because the texture image isn't centered or symetrical
        // If you dont want to manually drawing you can use vanilla projectile rendering offsets
        // Here you can check it https://github.com/tModLoader/tModLoader/wiki/Basic-Projectile#horizontal-sprite-example
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
            float offsetX = 250;
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
    }
}