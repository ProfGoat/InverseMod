﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace InverseMod.Projectiles.Melee
{
    public class YardStickProjectile : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Yard Stick");

            ProjectileID.Sets.DismountsPlayersOnHit[Type] = true;

            ProjectileID.Sets.NoMeleeSpeedVelocityScaling[Type] = true;
        }

        public override void SetDefaults()
        {
            Projectile.netImportant = true;

            Projectile.width = 25;
            Projectile.height = 25;

            Projectile.aiStyle = -1;

            Projectile.alpha = 255;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.scale = 1f;
            Projectile.hide = true;
            Projectile.ownerHitCheck = true;
            Projectile.DamageType = DamageClass.MeleeNoSpeed;
        }

        public override void AI()
        {
            Player owner = Main.player[Projectile.owner];
            Projectile.direction = owner.direction;
            owner.heldProj = Projectile.whoAmI;

            int itemAnimationMax = owner.itemAnimationMax;
            int holdOutFrame = (int)(itemAnimationMax * 0.34f);
            if (owner.channel && owner.itemAnimation < holdOutFrame)
            {
                owner.SetDummyItemTime(holdOutFrame);
            }

            if (owner.ItemAnimationEndingOrEnded)
            {
                Projectile.Kill();
                return;
            }

            int itemAnimation = owner.itemAnimation;
            float extension = 1 - Math.Max(itemAnimation - holdOutFrame, 0) / (float)(itemAnimationMax - holdOutFrame);
            float retraction = 1 - Math.Min(itemAnimation, holdOutFrame) / (float)holdOutFrame;

            float extendDist = 24;
            float retractDist = extendDist / 2;
            float tipDist = 98 + extension * extendDist - retraction * retractDist;

            Vector2 center = owner.RotatedRelativePoint(owner.MountedCenter);
            Projectile.Center = center;
            Projectile.position += Projectile.velocity * tipDist;

            Projectile.rotation = (float)Math.Atan2(Projectile.velocity.Y, Projectile.velocity.X) + (float)Math.PI * 3 / 4f;

            Projectile.alpha -= 40;
            if (Projectile.alpha < 0)
            {
                Projectile.alpha = 0;
            }

            float minimumDustVelocity = 6f;

            float movementInLanceDirection = Vector2.Dot(Projectile.velocity.SafeNormalize(Vector2.UnitX * owner.direction), owner.velocity.SafeNormalize(Vector2.UnitX * owner.direction));

            float playerVelocity = owner.velocity.Length();

            if (playerVelocity > minimumDustVelocity && movementInLanceDirection > 0.8f)
            {
                int dustChance = 8;
                if (playerVelocity > minimumDustVelocity + 1f)
                {
                    dustChance = 5;
                }
                if (playerVelocity > minimumDustVelocity + 2f)
                {
                    dustChance = 2;
                }

                int dustTypeCommon = ModContent.DustType<Dusts.YardStickDust>();
                int dustTypeRare = DustID.YellowTorch;

                int offset = 4;

                if (Main.rand.NextBool(dustChance))
                {
                    int newDust = Dust.NewDust(Projectile.Center - new Vector2(offset, offset), offset * 2, offset * 2, dustTypeCommon, Projectile.velocity.X * 0.2f + Projectile.direction * 3, Projectile.velocity.Y * 0.2f, 100, default, 1.2f);
                    Main.dust[newDust].noGravity = true;
                    Main.dust[newDust].velocity *= 0.25f;
                    newDust = Dust.NewDust(Projectile.Center - new Vector2(offset, offset), offset * 2, offset * 2, dustTypeCommon, 0f, 0f, 150, default, 1.4f);
                    Main.dust[newDust].velocity *= 0.25f;
                }

                if (Main.rand.NextBool(dustChance + 3))
                {
                    Dust.NewDust(Projectile.Center - new Vector2(offset, offset), offset * 2, offset * 2, dustTypeRare, 0f, 0f, 150, default, 1.4f);
                }
            }
        }

        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            if (damage > 0)
            {
                knockback *= Main.player[Projectile.owner].velocity.Length() / 7f;
            }
        }

        public override void ModifyDamageScaling(ref float damageScale)
        {
            damageScale *= 0.1f + Main.player[Projectile.owner].velocity.Length() / 7f * 0.9f;
        }

        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            float rotationFactor = Projectile.rotation + (float)Math.PI / 4f;
            float scaleFactor = 75f; // How far back the hit-line will be from the tip of the Jousting Lance. You will need to modify this if you have a longer or shorter Jousting Lance. Vanilla uses 95f
            float widthMultiplier = 18f; // How thick the hit-line is. Increase or decrease this value if your Jousting Lance is thicker or thinner. Vanilla uses 23f
            float collisionPoint = 0f;

            Rectangle lanceHitboxBounds = new Rectangle(0, 0, 300, 300);

            lanceHitboxBounds.X = (int)Projectile.position.X - lanceHitboxBounds.Width / 2;
            lanceHitboxBounds.Y = (int)Projectile.position.Y - lanceHitboxBounds.Height / 2;

            Vector2 hitLineEnd = Projectile.Center + rotationFactor.ToRotationVector2() * scaleFactor;

            // The following is for debugging the size of the hit line. This will allow you to easily see where it starts and ends.
            //Dust.NewDustPerfect(Projectile.Center, DustID.Pixie, Velocity: Vector2.Zero, Scale: 0.5f);
            //Dust.NewDustPerfect(hitLineEnd, DustID.Pixie, Velocity: Vector2.Zero, Scale: 0.5f);

            // First check that our large rectangle intersects with the target hitbox.
            // Then we check to see if a line from the tip of the Jousting Lance to the "end" of the lance intersects with the target hitbox.
            if (lanceHitboxBounds.Intersects(targetHitbox)
                && Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), Projectile.Center, hitLineEnd, widthMultiplier * Projectile.scale, ref collisionPoint))
            {
                return true;
            }
            return false;
        }

        public override bool PreDraw(ref Color lightColor)
        {

            SpriteEffects spriteEffects = SpriteEffects.None;

            Texture2D texture = TextureAssets.Projectile[Type].Value;

            Rectangle sourceRectangle = texture.Frame(1, Main.projFrames[Type], frameY: Projectile.frame);

            Vector2 origin = Vector2.Zero;

            float rotation = Projectile.rotation;

            if (Projectile.direction > 0)
            {
                rotation -= (float)Math.PI / 2f;
                origin.X += sourceRectangle.Width;
                spriteEffects = SpriteEffects.FlipHorizontally;
            }

            Vector2 position = new(Projectile.Center.X, Projectile.Center.Y - Main.player[Projectile.owner].gfxOffY);

            Color drawColor = Projectile.GetAlpha(lightColor);

            Main.EntitySpriteDraw(texture,
                position - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY),
                sourceRectangle, drawColor, rotation, origin, Projectile.scale, spriteEffects, 0);

            // The following is for debugging the size of the collision rectangle. Set this to the same size as the one you have in Colliding().
            //Rectangle lanceHitboxBounds = new Rectangle(0, 0, 300, 300);
            //Main.EntitySpriteDraw(TextureAssets.MagicPixel.Value,
            //    new Vector2((int)Projectile.Center.X - lanceHitboxBounds.Width / 2, (int)Projectile.Center.Y - lanceHitboxBounds.Height / 2) - Main.screenPosition,
            //    lanceHitboxBounds, Color.Orange * 0.5f, 0, Vector2.Zero, 1f, SpriteEffects.None, 0);

            return false;
        }
    }
}