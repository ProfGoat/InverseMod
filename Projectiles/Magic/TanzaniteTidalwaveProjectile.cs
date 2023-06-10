using InverseMod.Dusts;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace InverseMod.Projectiles.Magic
{
    internal class TanzaniteTidalwaveProjectile : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Tanzanite Tidalwave");
        }

        public override void SetDefaults()
        {
            Projectile.width = 12;
            Projectile.height = 12;

            Projectile.aiStyle = 0;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.ignoreWater = false;
            Projectile.light = 0.2f;
            Projectile.penetrate = 5;
            Projectile.tileCollide = false;
            Projectile.timeLeft = 800;
        }

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            if (player.dead && !player.active)
            {
                Projectile.Kill();
            }

            if (Main.rand.NextBool(2))
            {
                int numToSpawn = Main.rand.Next(4);
                for (int i = 0; i < numToSpawn; i++)
                {
                    int dustType = DustID.Water;
                    Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, dustType, Projectile.velocity.X * 0f, Projectile.velocity.Y * 0f,
                        0, default, 1.5f);
                }
            }

            // Factors for calculations
            double deg = Projectile.ai[1];
            double rad = deg * (Math.PI / 180);

            double a = 150; // scale factor
            double offsetX = a * Math.Cos(rad) * (1 + Math.Sin(rad));
            double offsetY = 0; // Initialize offsetY with a default value of 0

            double rotationAngleLeft = Math.PI / 180 * 90; // 90 degrees rotation left, convert to radians
            double rotationAngleRight = Math.PI / 180 * -90; // 90 degrees rotation right, convert to radians

            double cosAngleLeft = Math.Cos(rotationAngleLeft);
            double sinAngleLeft = Math.Sin(rotationAngleLeft);

            double cosAngleRight = Math.Cos(rotationAngleRight);
            double sinAngleRight = Math.Sin(rotationAngleRight);

            if (Projectile.ai[0] == 0)
            {
                offsetY = a * Math.Sin(rad) * (1 - Math.Sin(rad));

                offsetX += 0;
                offsetY += 70;
            }
            else if (Projectile.ai[0] == 1)
            {
                offsetY = a * Math.Sin(rad) * (1 - Math.Sin(rad)) * -1; // Invert the offsetY for the second triangle

                offsetX += 0;
                offsetY += -80;
            }
            else if (Projectile.ai[0] == 2) // Projectile.ai[0] == 2
            {
                double circleRadius = a / 2; // Adjust this value to change the distance of the circle from the player
                offsetX = circleRadius * Math.Cos(rad);
                offsetY = circleRadius * Math.Sin(rad);
            }
            else if (Projectile.ai[0] == 3)// Projectile.ai[0] == 2
            {
                double circleRadius = 225; // Adjust this value to change the distance of the circle from the player
                offsetX = circleRadius * Math.Cos(rad);
                offsetY = circleRadius * Math.Sin(rad);
            }
            else if (Projectile.ai[0] == 4)
            {
                offsetY = a * Math.Sin(rad) * (1 - Math.Sin(rad));

                // Apply left rotation
                double rotatedOffsetX = offsetX * cosAngleLeft - offsetY * sinAngleLeft;
                double rotatedOffsetY = offsetX * sinAngleLeft + offsetY * cosAngleLeft;

                offsetX = rotatedOffsetX;
                offsetY = rotatedOffsetY;

                offsetX += -80;
                offsetY += 0;
            }
            else
            {
                offsetY = a * Math.Sin(rad) * (1 - Math.Sin(rad));

                // Apply right rotation
                double rotatedOffsetX = offsetX * cosAngleRight - offsetY * sinAngleRight;
                double rotatedOffsetY = offsetX * sinAngleRight + offsetY * cosAngleRight;

                offsetX = rotatedOffsetX;
                offsetY = rotatedOffsetY;

                offsetX += 70;
                offsetY += 0;
            }

            // Position the projectile based on the calculated offsets
            Projectile.position.X = player.Center.X + (float)offsetX - Projectile.width / 2;
            Projectile.position.Y = player.Center.Y + (float)offsetY - Projectile.height / 2;

            // Increase the angle in degrees by 3 points
            Projectile.ai[1] += 3f;
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if (Main.rand.NextBool(4))
                target.AddBuff(BuffID.Wet, 180);
            int reducedImmunityFrames = 3; // Set this value to the desired number of immunity frames
            target.immune[Projectile.owner] = reducedImmunityFrames;

            // Call the base method to ensure other effects are applied as well
            base.OnHitNPC(target, damage, knockback, crit);
        }
    }
}