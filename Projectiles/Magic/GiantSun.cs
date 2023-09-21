using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.ID;
using Terraria.ModLoader;

namespace InverseMod.Projectiles.Magic
{
    public class GiantSun : ModProjectile
    {
        private const float maxIntensity = 3f;
        private float intensity = 0f;
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Giant Sun");
        }

        public override void SetDefaults()
        {
            Projectile.width = 179;  // Adjust these as necessary
            Projectile.height = 179;  // Adjust these as necessary
            Projectile.aiStyle = -1;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.timeLeft = 1200;  // The sun lasts for 10 seconds
            Projectile.light = 0.5f;  // Adjust this as necessary
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.extraUpdates = 1;
        }

        public override void AI()
        {
            // Set the scale of the projectile based on ai[0]
            Projectile.scale = Projectile.ai[0] / 3;  // You can adjust this formula as needed

            Player player = Main.player[Projectile.owner];

            // Set the projectile's position relative to the player
            Projectile.Center = player.Center + new Vector2(0, -200f);  // 200 pixels above the player

            if (!SkyManager.Instance["InverseMod/Assets/Textures/Backgrounds/RedSky"].IsActive())
            {
                SkyManager.Instance.Activate("InverseMod/Assets/Textures/Backgrounds/RedSky", Projectile.position);
            }
            if (Projectile.timeLeft > 60)
            {
                intensity += 1f;
                if (intensity > maxIntensity)
                {
                    intensity = maxIntensity;
                }
            }
            if (Projectile.timeLeft < 1180)
            {
                intensity -= 0.03f;
                if (intensity < 0f)
                {
                    intensity = 0f;
                }
            }


            // Tint the entire screen a dark orange-red
            Lighting.AddLight(Projectile.Center, 20.4f, 3f, 0f);
            if (Main.rand.NextBool(1))
            {
                int dustWidth = (int)(Projectile.width * Projectile.scale / 2.5);
                int dustHeight = (int)(Projectile.height * Projectile.scale / 2.5);
                Vector2 dustPosition = Projectile.Center - new Vector2(dustWidth / 2, dustHeight / 2); // Center the dust hitbox
                int dustIndex = Dust.NewDust(dustPosition, dustWidth, dustHeight, DustID.RedTorch, 0f, 0f, 100, default, 2f);
                Main.dust[dustIndex].velocity *= 2f;
                Main.dust[dustIndex].noGravity = true;
            }

            // Give all nearby enemies the Inferno Debuff and deal damage based on their distance to the sun
            float maxDistance = 600f;  // The maximum distance at which enemies are affected
            int immunityTime = 10;  // The number of frames between hits
            foreach (NPC npc in Main.npc)
            {
                if (npc.active && !npc.friendly && !npc.dontTakeDamage)
                {
                    float distance = Vector2.Distance(npc.Center, Projectile.Center);
                    if (distance <= maxDistance)
                    {
                        // Apply the Inferno Debuff
                        npc.AddBuff(BuffID.OnFire, 2 * 60);  // Lasts for 2 seconds

                        // Check whether enough time has passed since the last hit
                        if (Main.GameUpdateCount - npc.ai[3] >= immunityTime)
                        {
                            // Deal damage based on distance
                            float damageFactor = 1 - distance / maxDistance;  // The closer the NPC, the higher the damage factor
                            int damage = (int)(Projectile.damage * damageFactor);
                            npc.SimpleStrikeNPC((int)player.GetDamage(DamageClass.Melee).ApplyTo(damage), 1);

                            // Update the time of the last hit
                            npc.ai[3] = Main.GameUpdateCount;
                        }
                    }
                }
            }
        }
        public override void Kill(int timeLeft)
        {
            // Deactivate the custom sky when the projectile is no longer active
            if (SkyManager.Instance["InverseMod/Assets/Textures/Backgrounds/RedSky"].IsActive())
            {
                SkyManager.Instance.Deactivate("InverseMod/Assets/Textures/Backgrounds/RedSky");
            }
        }

        public override bool PreDraw(ref Color lightColor)
        {
            // Draw the sprite manually
            Texture2D texture = ModContent.Request<Texture2D>("InverseMod/Projectiles/Magic/GiantSun").Value;
            Color color = Color.White * intensity;  // Adjust the color based on the opacity
            Main.spriteBatch.Draw(texture, Projectile.Center - Main.screenPosition, null, color, Projectile.rotation, texture.Size() / 2, Projectile.scale, SpriteEffects.None, 0f);

            return false;  // Don't draw the default sprite
        }
    }
}