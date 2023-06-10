using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ModLoader;

namespace InverseMod.Projectiles.Magic
{
    public class ElementExplosion : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Element Explosion");
            Main.projFrames[Projectile.type] = 4; // Set the number of frames for the projectile's sprite
        }

        public override void SetDefaults()
        {
            Projectile.width = 180;
            Projectile.height = 180;
            Projectile.aiStyle = -1;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 30;
            Projectile.light = 0.8f;
            Projectile.scale = 1f;
            Projectile.extraUpdates = 1;
        }

        public override void AI()
        {
            // Fade out over time
            Projectile.alpha += 255 / Projectile.timeLeft;

            // Set the frame based on the time left (30 total time / 4 frames = 7.5 ticks per frame)
            Projectile.frame = 3 - Projectile.timeLeft / 8;

            // Apply a custom explosion effect
            if (Projectile.timeLeft == 30)
            {
                for (int i = 0; i < 100; i++)
                {
                    int dustType;
                    switch (Main.rand.Next(10))
                    {
                        case 0:
                            dustType = 185; // Fire dust
                            break;
                        case 1:
                            dustType = 259; // Ice dust
                            break;
                        case 2:
                            dustType = 73; // Lightning dust
                            break;
                        default:
                            dustType = 229; // Earth dust
                            break;
                    }
                    int dustIndex = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, dustType, 0f, 0f, 100, default, 2f);
                    Main.dust[dustIndex].velocity *= 2f;
                    Main.dust[dustIndex].noGravity = true;
                }

                SoundEngine.PlaySound(Terraria.ID.SoundID.Item14, Projectile.position);
            }
        }
    }
}