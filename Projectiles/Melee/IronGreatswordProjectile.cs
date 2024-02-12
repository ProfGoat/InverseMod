using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ID;
using Microsoft.CodeAnalysis;

namespace InverseMod.Projectiles.Melee
{
    public class IronGreatswordProjectile : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 62; // Adjust as needed
            Projectile.height = 62; // Adjust as needed
            Projectile.aiStyle = 1;             //The ai style of the projectile, please reference the source code of Terraria
            Projectile.friendly = true;         //Can the projectile deal damage to enemies?
            Projectile.hostile = false;         //Can the projectile deal damage to the player?
            Projectile.DamageType = DamageClass.Melee;
            Projectile.penetrate = -1;           //How many monsters the projectile can penetrate. (OnTileCollide below also decrements penetrate for bounces as well)
            Projectile.timeLeft = 18;          //The live time for the projectile (60 = 1 second, so 600 is 10 seconds)
            Projectile.alpha = 0;             //The transparency of the projectile, 255 for completely transparent. (aiStyle 1 quickly fades the projectile in) Make sure to delete this if you aren't using an aiStyle that fades in. You'll wonder why your projectile is invisible.
            Projectile.light = 0f;            //How much light emit around the projectile
            Projectile.ignoreWater = true;          //Does the projectile's speed be influenced by water?
            Projectile.tileCollide = false;          //Can the projectile collide with tiles?
            Projectile.extraUpdates = 0;            //Set to above 0 if you want the projectile to update multiple time in a frame

            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = -1;
        }
        float rotationStrength = 0.1f;
        bool firstSpawn = true;
        double deg;
        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            player.heldProj = Projectile.whoAmI;
            player.SetCompositeArmFront(true, Player.CompositeArmStretchAmount.Full, (player.Center - Projectile.Center).ToRotation() + MathHelper.PiOver2);

            if (firstSpawn)
            {
                Projectile.ai[1] = MathHelper.ToDegrees((float)Math.Atan2(Main.MouseWorld.Y - player.Center.Y, Main.MouseWorld.X - player.Center.X) - 30);
                firstSpawn = false;
            }
            if (player.dead && !player.active)
            {
                Projectile.Kill();
            }
            if (Projectile.timeLeft > 12)
            {
                rotationStrength += 0.9f;
            }
            else
            {
                rotationStrength -= 1.2f;
                if (rotationStrength < -12f)
                {
                    rotationStrength = -12f;
                }
                Projectile.alpha += 12;
            }

            deg = Projectile.ai[1] += 12f + rotationStrength;

            double rad = deg * (Math.PI / 180);
            double dist = 40;

            /*Position the player based on where the player is, the Sin/Cos of the angle times the /
            /distance for the desired distance away from the player minus the projectile's width   /
            /and height divided by two so the center of the projectile is at the right place.     */
            Projectile.position.X = player.Center.X - (int)(Math.Cos(rad) * dist) - Projectile.width / 2;
            Projectile.position.Y = player.Center.Y - (int)(Math.Sin(rad) * dist) - Projectile.height / 2;

            Projectile.rotation = Vector2.Normalize(Main.player[Projectile.owner].Center - Projectile.Center).ToRotation() + MathHelper.ToRadians(225f);

            Dust.NewDustPerfect(Projectile.Center, DustID.GemDiamond, Velocity: Vector2.Zero, Scale: 1f);
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            Player player = Main.player[Projectile.owner];

            // Check if the NPC is not a target dummy
            if (target.type != NPCID.TargetDummy && !target.boss)
            {
                // Calculate the direction from the player to the NPC
                Vector2 knockbackDirection = target.Center - player.Center;

                // Normalize the vector to get a unit vector (direction only, length of 1)
                knockbackDirection.Normalize();

                // Set the knockback strength (you can adjust this value as needed)
                float knockbackStrength = 8f; // Example strength, adjust as needed

                // Apply the knockback to the NPC
                target.velocity = knockbackDirection * knockbackStrength;

                // Optional: Add any additional effects upon hitIrong the NPC here
            }

            base.OnHitNPC(target, hit, damageDone);
        }
    }
}