using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace InverseMod.Projectiles.Magic
{
    public class SunFlare : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Sun Flare");
        }

        public override void SetDefaults()
        {
            Projectile.width = 36;
            Projectile.height = 36;
            Projectile.aiStyle = 1;
            AIType = ProjectileID.Bullet;  // Act like a bullet
            Projectile.friendly = true;
            Projectile.penetrate = 1;  // Can hit 1 enemy
            Projectile.timeLeft = 600;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.DamageType = DamageClass.Magic;
        }

        public override void AI()
        {
            // Add some light and dust
            Lighting.AddLight(Projectile.Center, Color.Orange.ToVector3() * 0.8f);
            if (Main.rand.NextBool(2))
            {
                int dustIndex = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.SolarFlare, 0f, 0f, 100, default, 2f);
                Main.dust[dustIndex].velocity *= 0.3f;
                Main.dust[dustIndex].noGravity = true;
            }
        }

        // Make the projectile disappear when it hits something
        public override void Kill(int timeLeft)
        {
            // Explode into Greek Fire upon being destroyed
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
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            base.OnHitNPC(target, damage, knockback, crit);
            {
                if (Main.rand.NextBool(4))
                    target.AddBuff(BuffID.Inferno, 180);
            }
        }
    }
}