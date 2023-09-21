using InverseMod.Dusts;
using InverseMod.Items;
using InverseMod.Items.Weapons;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace InverseMod.Projectiles.Magic
{
    internal class GarnetGigadrainProjectile : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Gigadrain Shard");

            Main.projFrames[Projectile.type] = 5;
        }

        public override void SetDefaults()
        {
            Projectile.width = 22;
            Projectile.height = 22;

            Projectile.aiStyle = 0;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.ignoreWater = false;
            Projectile.penetrate = 8;
            Projectile.tileCollide = false;
            Projectile.timeLeft = 1000;

            // Add these lines
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 10000;
        }

        private int frameCounter = 0;
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
            Projectile.alpha += 25;
            // Cal alpha to the maximum 255(complete transparent)
            if (Projectile.alpha > 255)
                Projectile.alpha = 255;
        }

        private int attachedNPC = -1;
        private int damageTimer = 0;
        public override void AI()
        {
            Player player = Main.player[Projectile.owner];

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
                float homingSpeed = 0.2f;
                float distanceToClosestTarget = 500f;
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

                Projectile.rotation = Projectile.velocity.ToRotation() - (float)Math.PI / 2 + MathHelper.ToRadians(135);
            }

            Lighting.AddLight(Projectile.Center, new Vector3(0.5f, 0, 0));

            if (Main.rand.NextBool(3))
            {
                int numToSpawn = Main.rand.Next(2);
                for (int i = 0; i < numToSpawn; i++)
                {
                    int dustType = ModContent.DustType<GarnetDust>();
                    Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, dustType, Projectile.velocity.X * 0f, Projectile.velocity.Y * 0.2f,
                        0, default, 1.5f);
                }
            }

            // Animate the projectile
            frameCounter++;
            if (frameCounter >= 10) // Change the frame every 2 ticks
            {
                frameCounter = 0;
                Projectile.frame++;
                if (Projectile.frame >= 5) // If the frame is greater than or equal to the total number of frames, reset to the first frame
                {
                    Projectile.frame = 0;
                }
            }
        }

        public override bool PreDraw(ref Color lightColor)
        {
            // SpriteEffects helps to flip texture horizontally and vertically
            SpriteEffects spriteEffects = SpriteEffects.None;
            if (Projectile.spriteDirection == -1)
                spriteEffects = SpriteEffects.FlipHorizontally;

            // Getting texture of projectile
            Texture2D texture = (Texture2D)ModContent.Request<Texture2D>(Texture);

            // Calculating frameHeight and current Y pos dependence of frame
            // If texture without animation frameHeight is always texture.Height and startY is always 0
            int frameHeight = texture.Height / Main.projFrames[Projectile.type];
            int startY = frameHeight * Projectile.frame;

            // Get this frame on texture
            Rectangle sourceRectangle = new Rectangle(0, startY, texture.Width, frameHeight);

            // Alternatively, you can skip defining frameHeight and startY and use this:
            // Rectangle sourceRectangle = texture.Frame(1, Main.projFrames[Projectile.type], frameY: Projectile.frame);

            Vector2 origin = sourceRectangle.Size() / 2f;

            // If image isn't centered or symmetrical you can specify origin of the sprite
            // (0,0) for the upper-left corner
            float offsetX = 20; // You might need to tweak this value to better align the projectile
            origin.X = Projectile.spriteDirection == 1 ? sourceRectangle.Width - offsetX : offsetX;

            // If sprite is vertical
            // float offsetY = 20f;
            // origin.Y = (float)(Projectile.spriteDirection == 1 ? sourceRectangle.Height - offsetY : offsetY);


            // Applying lighting and draw current frame
            Color drawColor = Projectile.GetAlpha(lightColor);
            Main.EntitySpriteDraw(texture,
                Projectile.Center - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY),
                sourceRectangle, drawColor, Projectile.rotation, origin, Projectile.scale, spriteEffects, 0);

            // It's important to return false, otherwise we also draw the original texture.
            return false;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (Main.rand.NextBool(4))
            {
                target.AddBuff(BuffID.Bleeding, 180);
            }

            if (attachedNPC == -1)
            {
                attachedNPC = target.whoAmI;
                Projectile.penetrate = -1;
                Projectile.velocity = Vector2.Zero;
                Projectile.timeLeft = 300; // Adjust this value to control how long the projectile stays attached

                // Store the relative position between the projectile and the NPC
                Projectile.localAI[0] = (Projectile.Center - target.Center).ToRotation();
                // Store the relative rotation of the projectile
                Projectile.localAI[1] = Projectile.rotation - target.rotation;
            }
        }
    }
}
