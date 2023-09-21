using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.Graphics.Effects;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace InverseMod.Projectiles.Magic
{
    public class CopalChromosphereProjectile : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Chromosphere");
        }

        public override void SetDefaults()
        {
            Projectile.width = 72;
            Projectile.height = 72;
            Projectile.aiStyle = -1;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 1200;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.DamageType = DamageClass.Magic;
        }

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];

            // Orbit around the player
            const double orbitRadius = 200;  // Adjust this for the desired orbit radius
            const double orbitSpeed = 0.02;  // Adjust this for the desired orbit speed
            Projectile.Center = player.Center + new Vector2((float)(orbitRadius * Math.Cos(orbitSpeed * Projectile.timeLeft)), (float)(orbitRadius * Math.Sin(orbitSpeed * Projectile.timeLeft)));

            // Periodically shoot a solar flare towards the cursor
            if (++Projectile.ai[0] >= 20)
            {
                Projectile.ai[0] = 0;  // Reset the counter
                Vector2 target = Main.MouseWorld;
                Vector2 direction = (target - Projectile.Center).SafeNormalize(Vector2.UnitX);
                direction = direction.RotatedByRandom(MathHelper.ToRadians(10));  // Add some randomness to the direction

                Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, direction * 8f, ModContent.ProjectileType<SunFlare>(), Projectile.damage, Projectile.knockBack, Projectile.owner);
            }

            // Add some light and dust
            Lighting.AddLight(Projectile.Center, 2.55f, .69f, 0f);
            if (Main.rand.NextBool(2))
            {
                int dustIndex = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.SolarFlare, 0f, 0f, 100, default, 2f);
                Main.dust[dustIndex].velocity *= 0.3f;
                Main.dust[dustIndex].noGravity = true;
            }
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (Main.rand.NextBool(1))
                target.AddBuff(BuffID.Inferno, 900);
            int reducedImmunityFrames = 3; // Set this value to the desired number of immunity frames
            target.immune[Projectile.owner] = reducedImmunityFrames;
        }
    }
}