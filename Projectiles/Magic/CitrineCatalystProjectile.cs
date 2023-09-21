using InverseMod.Dusts;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace InverseMod.Projectiles.Magic
{
    public class CitrineCatalystProjectile : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Citrine Flame");

            Main.projFrames[Projectile.type] = 6;
        }

        // You can check most of Fields and Properties here https://github.com/tModLoader/tModLoader/wiki/Projectile-Class-Documentation
        public override void SetDefaults()
        {
            Projectile.width = 46;
            Projectile.height = 46;

            Projectile.aiStyle = 0; // The ai style of the projectile (0 means custom AI). For more please reference the source code of Terraria
            Projectile.DamageType = DamageClass.Magic;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.ignoreWater = false;
            Projectile.light = 0.8f;
            Projectile.tileCollide = false;
            Projectile.penetrate = 3;
            Projectile.timeLeft = 1000; // (60 = 1 second, so 600 is 10 seconds)
        }

        private int frameCounter = 0;
        public override void AI()
        {
            Lighting.AddLight(Projectile.Center, 1f, .6f, .35f);

            Player player = Main.player[Projectile.owner];
            if (player.dead && !player.active)
            {
                Projectile.Kill();
            }
            Projectile.spriteDirection = Projectile.velocity.X > 0 ? 1 : -1;
            Projectile.rotation = Projectile.velocity.ToRotation();
            if (Projectile.velocity.Y > 16f)
            {
                Projectile.velocity.Y = 16f;
            }

            if (Main.rand.NextBool(2))
            {
                int numToSpawn = Main.rand.Next(3);
                for (int i = 0; i < numToSpawn; i++)
                {
                    int dustType = ModContent.DustType<CitrineCatalystDust>();
                    Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, dustType, Projectile.velocity.X * 0f, Projectile.velocity.Y * 0.1f,
                        0, default, 1.5f);
                }
            }

            // Factors for calculations
            double deg = Projectile.ai[0] * 72 + Projectile.ai[1];
            double rad = deg * (Math.PI / 180);
            double dist = 128 + Projectile.ai[1] / 8; // Increasing distance from the player over time

            // Position the projectile based on the Sin/Cos of the angle and the increasing distance from the player
            Projectile.position.X = player.Center.X - (int)(Math.Cos(rad) * dist) - Projectile.width / 2;
            Projectile.position.Y = player.Center.Y - (int)(Math.Sin(rad) * dist) - Projectile.height / 2;

            // Increase the counter/angle in degrees by 3 points
            Projectile.ai[1] += 3f;

            frameCounter++;
            if (frameCounter >= 5) // Change the frame every 5 ticks
            {
                frameCounter = 0;
                Projectile.frame++;
                if (Projectile.frame >= 6) // If the frame is greater than or equal to the total number of frames, reset to the first frame
                {
                    Projectile.frame = 0;
                }
            }


            base.AI();
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (Main.rand.NextBool(4))
                target.AddBuff(BuffID.OnFire, 180);
        }
    }
}