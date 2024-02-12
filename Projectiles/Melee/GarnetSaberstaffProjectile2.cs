using InverseMod.Common.Systems;
using InverseMod.Dusts;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace InverseMod.Projectiles.Melee
{
    public class GarnetSaberstaffProjectile2 : ModProjectile
    {
        private const float OutwardTime = 30f;
        private const float CatchDistance = 48f;
        private const float SpinRate = 0.2f;
        public bool isAttached = false;

        private int attachedNPC = -1;
        private int damageTimer = 0;
        private int attachmentTimer = 300; // 10 seconds at 60fps

        public override void SetDefaults()
        {
            Projectile.width = 98; // Adjust as needed
            Projectile.height = 98; // Adjust as needed
            Projectile.aiStyle = 0; // No default AI style
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.ignoreWater = true;
            Projectile.extraUpdates = 1; // For smoother animation
        }
        public override void AI()
        {
            Player player = Main.player[Projectile.owner];

            // Increment the timer
            Projectile.ai[0] += 1f;

            Lighting.AddLight(Projectile.Center, 1f, 0f, 0f);

            if (Main.rand.NextBool(2))
            {
                int numToSpawn = Main.rand.Next(3);
                for (int i = 0; i < numToSpawn; i++)
                {
                    int dustType = DustID.GemRuby;
                    Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, dustType, Projectile.velocity.X * 0f, Projectile.velocity.Y * 0.1f,
                        0, default, .5f);
                }
            }
            if (!isAttached)
            {
                if (Projectile.ai[0] >= OutwardTime)
                {
                    // Calculate direction from projectile to player
                    Vector2 directionToPlayer = player.Center - Projectile.Center;
                    float distanceToPlayer = directionToPlayer.Length();

                    // Normalize the direction vector, then adjust the velocity of the projectile to move towards the player
                    if (distanceToPlayer > CatchDistance)
                    {
                        directionToPlayer.Normalize();
                        directionToPlayer *= 18f; // Adjust this speed as needed

                        // Make the projectile's velocity interpolate towards the player's position, making it return
                        Projectile.velocity = (Projectile.velocity * 0.95f) + (directionToPlayer * 0.05f);
                    }
                    else
                    {
                        // If the projectile is close enough to the player, kill it (considered caught)
                        Projectile.Kill();
                    }
                }
            }

            if (isAttached && attachedNPC >= 0)
            {
                attachmentTimer--; // Decrement the attachment timer

                if (attachmentTimer <= 0)
                {
                    // Timer expired, detach from NPC and start returning
                    isAttached = false;
                    attachedNPC = -1;
                    StartReturning();
                }
            }

            if (attachedNPC >= 0 && Main.npc[attachedNPC].active && !Main.npc[attachedNPC].dontTakeDamage)
            {
                NPC target = Main.npc[attachedNPC];

                // Update the projectile's position based on the stored relative position and NPC's rotation
                Projectile.Center = target.Center + Projectile.localAI[0].ToRotationVector2() * target.scale;

                // Increment the damage timer
                damageTimer++;

                if (damageTimer >= 30)
                {
                    if (Main.rand.NextBool(2))
                    {
                        // Reset the damage timer and deal damage
                        damageTimer = 0;
                        //   int damageDealt = (int)target.StrikeNPC(Projectile.damage, 0f, 0, crit: false, noEffect: true);
                        int damageDealt = (int)target.SimpleStrikeNPC((int)player.GetDamage(DamageClass.Melee).ApplyTo(Projectile.damage), 1);
                        // Life-steal mechanic
                        player.HealEffect(Main.rand.Next(1, 5), true);
                        player.statLife += Main.rand.Next(1, 5);
                        if (player.statLife > player.statLifeMax2)
                        {
                            player.statLife = player.statLifeMax2;
                        }
                    }
                }
            }
            else
            {
                if (attachedNPC != -1)
                {
                    // Reset attached NPC and velocity when the target is killed or inactive
                    attachedNPC = -1;
                    Vector2 directionToMouse = Main.MouseWorld - player.Center;
                    directionToMouse.Normalize();
                    Projectile.velocity = directionToMouse * 7f;
                    Projectile.ai[0] = 1;
                }

                // Homing behavior
                float homingSpeed = 0.5f;
                float distanceToClosestTarget = 700f;
                int targetIndex = -1;

                for (int i = 0; i < Main.maxNPCs; i++)
                {
                    NPC npc = Main.npc[i];
                    if (npc.active && !npc.friendly && !npc.dontTakeDamage && npc.lifeMax > 5)
                    {
                        float currentDistance = Vector2.Distance(Projectile.Center, npc.Center);
                        if (currentDistance < distanceToClosestTarget)
                        {
                            distanceToClosestTarget = currentDistance;
                            targetIndex = i;
                        }
                    }
                }

                if (targetIndex != -1)
                {
                    NPC target = Main.npc[targetIndex];
                    Vector2 directionToTarget = target.Center - Projectile.Center;
                    directionToTarget.Normalize();
                    Projectile.velocity += directionToTarget * homingSpeed;
                    Projectile.velocity.Normalize();
                    Projectile.velocity *= 7f;
                }
            }

            // Spin the projectile
            Projectile.rotation += SpinRate;

            // Optionally, if you want the projectile to spin in the direction of its movement, you can combine the spin with its current rotation like this:
            // Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2 + SpinRate * Projectile.ai[0];
            // This will make the projectile spin faster over time, which might not be what you want, so adjust as necessary.
        }
        public void StartReturning()
        {

            Player player = Main.player[Projectile.owner];
            // Calculate direction from projectile to player
            Vector2 directionToPlayer = player.Center - Projectile.Center;
            float distanceToPlayer = directionToPlayer.Length();

            // Normalize the direction vector, then adjust the velocity of the projectile to move towards the player
            if (distanceToPlayer > CatchDistance)
            {
                directionToPlayer.Normalize();
                directionToPlayer *= 10f; // Adjust this speed as needed

                // Make the projectile's velocity interpolate towards the player's position, making it return
                Projectile.velocity = (Projectile.velocity * 0.95f) + (directionToPlayer * 0.05f);
            }
            else
            {
                // If the projectile is close enough to the player, kill it (considered caught)
                Projectile.Kill();
            }
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            Player player = Main.player[Projectile.owner];
            if (!isAttached)
            {
                isAttached = true;
                attachedNPC = target.whoAmI;
                Projectile.penetrate = -1;
                Projectile.velocity = Vector2.Zero;

                // Store relative positions and rotations
                Projectile.localAI[0] = (Projectile.Center - target.Center).ToRotation();
                Projectile.localAI[1] = Projectile.rotation - target.rotation;

                // Life-steal mechanic
                player.HealEffect(Main.rand.Next(5, 10), true);
                player.statLife += Main.rand.Next(5, 10);
                if (player.statLife > player.statLifeMax2)
                {
                    player.statLife = player.statLifeMax2;
                }

                if (Main.rand.NextBool(4))
                {
                    target.AddBuff(BuffID.Bleeding, 180);
                }

                if (attachedNPC == -1)
                {
                    attachedNPC = target.whoAmI;
                    Projectile.penetrate = -1;
                    Projectile.velocity = Vector2.Zero;

                    // Store the relative position between the projectile and the NPC
                    Projectile.localAI[0] = (Projectile.Center - target.Center).ToRotation();
                    // Store the relative rotation of the projectile
                    Projectile.localAI[1] = Projectile.rotation - target.rotation;
                }
            }
            SoundEngine.PlaySound(rorAudio.Hit);
            base.OnHitNPC(target, hit, damageDone);
        }
    }
}