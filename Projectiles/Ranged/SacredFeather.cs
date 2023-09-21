using InverseMod.Buffs;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace InverseMod.Projectiles.Ranged
{
    public class SacredFeather : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Sacred Feather");
        }

        public override void SetDefaults()
        {
            Projectile.width = 14;
            Projectile.height = 24;
            Projectile.aiStyle = 1;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.timeLeft = 600;
            Projectile.penetrate = 3;
            AIType = ProjectileID.Bullet;
        }
        public override void AI()
        {
            base.AI();

            FadeInAndOut();

            Lighting.AddLight(Projectile.Center, 1f, 1f, 1.2f);
        }
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
            Projectile.alpha += 5;
            // Cal alpha to the maximum 255(complete transparent)
            if (Projectile.alpha > 255)
                Projectile.alpha = 255;

            return;
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (Main.rand.NextBool(2))
                target.AddBuff(ModContent.BuffType<Levitation>(), 60);
        }
    }
}