using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace InverseMod.Projectiles.Magic
{
    public class CursedFlameFireball : ModProjectile
    {

        private float waveFrequency = 0.1f; // Frequency of the wave
        private float waveAmplitude = 2f; // Amplitude of the wave
        private Vector2 initialVelocity; // To store the initial velocity
        public override void SetDefaults()
        {
            Projectile.width = 16; // Adjust the size as needed
            Projectile.height = 16; // Adjust the size as needed
            Projectile.aiStyle = 0; // No default AI style, we'll handle movement manually
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.tileCollide = true; // Or false, depending on whether you want it to pass through tiles
            Projectile.penetrate = 1;
            Projectile.light = 0.5f; // Adjust the light emitted by the projectile
            Projectile.ignoreWater = true;
        }

        public override void AI()
        {
            // Store the initial velocity
            if (Projectile.ai[0] == 0)
            {
                initialVelocity = Projectile.velocity;
                Projectile.ai[0] = 1;
            }

            // Helical movement logic
            float wavePhase = Projectile.ai[1] * waveFrequency; // Phase of the wave, which advances each tick
            Vector2 waveVector = initialVelocity.RotatedBy(Math.PI / 2); // Vector perpendicular to initial velocity
            waveVector.Normalize(); // Ensure it's a unit vector
            waveVector *= waveAmplitude * MathF.Cos(wavePhase); // Scale by the wave amplitude and cosine function

            // Adjust the position of the projectile based on the helical path
            Projectile.position += waveVector;

            // Advance the phase for the next tick
            Projectile.ai[1] += 1;

            // Visual effects, like dust
            if (Main.rand.NextBool(2)) // Adjust the frequency as needed
            {
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Terra, Projectile.velocity.X * 0f, Projectile.velocity.Y * 0f);
            }

            // Projectile rotation (if desired)
            Projectile.rotation += 0.1f; // Adjust rotation speed as needed
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (Main.rand.NextBool(4))
                target.AddBuff(BuffID.CursedInferno, 180);
        }
    }
}
