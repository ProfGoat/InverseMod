using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace InverseMod.Projectiles.Magic
{
    public class CelestialElement : ModProjectile
    {
        private readonly string[] elementTextureNames = { "StardustElement", "SolarElement", "VortexElement", "NebulaElement" };
        private readonly int[] elementDustIndices = { 185, 259, 229, 73 };
        private Texture2D[] elementTextures;

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Celestial Element");
        }

        public override void SetDefaults()
        {
            Projectile.width = 26;
            Projectile.height = 26;
            Projectile.aiStyle = -1;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 180;
            Projectile.light = 0.5f;
            Projectile.scale = 1f;
            Projectile.extraUpdates = 1;

            elementTextures = new Texture2D[elementTextureNames.Length];
            for (int i = 0; i < elementTextureNames.Length; i++)
            {
                elementTextures[i] = Request<Texture2D>("InverseMod/Projectiles/Magic/" + elementTextureNames[i]).Value;
            }
        }

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            int elementIndex = (int)Projectile.ai[0];
            float rotationSpeed = 0.03f + 0.001f * elementIndex;
            float distance = 60f;
            // Orbit around the player
            Vector2 orbitCenter = player.Center;

            // Separate the integer and decimal part of ai[1]
            int state = (int)Projectile.ai[1];
            float rotation = Projectile.ai[1] - state;

            // Increase rotation
            rotation += rotationSpeed;
            if (rotation >= 1f)
            {
                rotation -= 1f;
            }

            double angle = rotation * MathHelper.TwoPi;
            Vector2 offset = new Vector2(distance, 0).RotatedBy(angle);

            // Get the target position
            Vector2 targetPosition = Main.MouseWorld;
            float pulseSpeed = 0.05f;  // Adjust this value to change the speed of pulsing
            float minScale = 0.8f;  // Adjust this value to change the minimum size
            float maxScale = 1.2f;  // Adjust this value to change the maximum size
            Projectile.scale = minScale + (maxScale - minScale) * (float)Math.Abs(Math.Sin(Main.GameUpdateCount * pulseSpeed));


            // If the right mouse button is held down, change the orbit center to the mouse's world position
            if (Main.mouseRight)
            {
                orbitCenter = Main.MouseWorld;
                Projectile.timeLeft = 180;
            }

            string textureName = "InverseMod/Projectiles/Magic/" + elementTextureNames[(int)Projectile.ai[0]];
            Projectile.spriteDirection = 1;

            // When the left mouse button is clicked, shoot towards the target position
            if (Main.mouseLeft && player.itemAnimation > 0 && player.itemAnimation <= player.itemAnimationMax / 2)
            {
                Vector2 shootVelocity = targetPosition - Projectile.Center;
                shootVelocity.Normalize();

                // Add a bit of randomness to the shooting direction
                const float maxAngleDeviation = MathHelper.PiOver4 / 2;  // Max deviation of 45 degrees / 2
                float deviation = Main.rand.NextFloat(-maxAngleDeviation, maxAngleDeviation);  // Random deviation
                shootVelocity = shootVelocity.RotatedBy(deviation);  // Rotate the shoot velocity by the deviation

                shootVelocity *= 12;
                Projectile.velocity = shootVelocity;
                Projectile.timeLeft = 90;
                Projectile.friendly = true;
                Projectile.penetrate = 1;
                Projectile.damage = 500;
                // Set state to 1, meaning the projectile is now moving
                state = 1;
            }

            // Only orbit the center when state is not 1 (projectile is not moving)
            if (state != 1)
            {
                Projectile.Center = orbitCenter + offset;
            }

            // Update rotation
            Projectile.rotation = (float)angle;

            // Store the state and rotation back to ai[1]
            Projectile.ai[1] = state + rotation;

            int dustIndex = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, elementDustIndices[(int)Projectile.ai[0]], 0f, 0f, 150, default, 1.5f);
            Main.dust[dustIndex].velocity *= 0.3f;
            Main.dust[dustIndex].noGravity = true;
        }

        public override bool PreDraw(ref Color lightColor)
        {
            string textureName = "InverseMod/Projectiles/Magic/" + elementTextureNames[(int)Projectile.ai[0]];
            Texture2D texture = Request<Texture2D>(textureName).Value;

            // Draw the sprite manually
            Main.spriteBatch.Draw(texture, Projectile.Center - Main.screenPosition, null, lightColor, Projectile.rotation, texture.Size() / 2, Projectile.scale, SpriteEffects.None, 0f);

            return false;  // Don't draw the default sprite
        }

        public override void Kill(int timeLeft)
        {
            if (!Projectile.friendly) return;

            // Create an explosion projectile at the position where this projectile died
            int explosion = Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, Vector2.Zero, ProjectileType<ElementExplosion>(), (int)(Projectile.damage * 1.5), Projectile.knockBack * 2, Projectile.owner);
            Main.projectile[explosion].DamageType = DamageClass.Magic;
        }
    }
}