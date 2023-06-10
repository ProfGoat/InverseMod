using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace InverseMod.Items.Tools
{
    internal class GrapheneHook : ModItem
    {
        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.AmethystHook);
            Item.shootSpeed = 40f;
            Item.shoot = ModContent.ProjectileType<GrapheneHookProjectile>();
            Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(4, 16));
            ItemID.Sets.AnimatesAsSoul[Item.type] = true;
        }

      public override void AddRecipes()
        {
            Recipe r = CreateRecipe();
            r.AddIngredient(null, "Graphene", 20);
            r.AddIngredient(ItemID.FragmentNebula, 5);
            r.AddIngredient(ItemID.FragmentVortex, 5);
            r.AddIngredient(ItemID.FragmentSolar, 5);
            r.AddIngredient(ItemID.FragmentStardust, 5);
            r.AddTile(TileID.LunarCraftingStation);
            r.Register();
        }
    }

    internal class GrapheneHookProjectile : ModProjectile
    {
        private static List<Asset<Texture2D>> chainTextures = new List<Asset<Texture2D>>();
        private int selectedTexture;
        private static Asset<Texture2D> hookTexture;
        public override void Load()
        {
            hookTexture = ModContent.Request<Texture2D>("InverseMod/Items/Tools/GrapheneHookProjectile"); // Adjust this to your actual hook texture path
            chainTextures.Add(ModContent.Request<Texture2D>("InverseMod/Items/Tools/GrapheneHookChain1"));
            chainTextures.Add(ModContent.Request<Texture2D>("InverseMod/Items/Tools/GrapheneHookChain2"));
            chainTextures.Add(ModContent.Request<Texture2D>("InverseMod/Items/Tools/GrapheneHookChain3"));
            chainTextures.Add(ModContent.Request<Texture2D>("InverseMod/Items/Tools/GrapheneHookChain4"));
        }

        public override void Unload()
        {
            chainTextures.Clear();
            hookTexture = null;
        }
        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.GemHookAmethyst);
            Main.projFrames[Projectile.type] = 16;

            // Randomly select a texture for this chain
            selectedTexture = Main.rand.Next(chainTextures.Count);
        }

        public override void AI()
        {
            frameCounter++;
            if (frameCounter >= 7) // Change the frame every 5 ticks
            {
                frameCounter = 0;
                Projectile.frame++;
                if (Projectile.frame >= 16) // If the frame is greater than or equal to the total number of frames, reset to the first frame
                {
                    Projectile.frame = 0;
                }
            }
            base.AI();
        }

        // Use this hook for hooks that can have multiple hooks mid-flight: Dual Hook, Web Slinger, Fish Hook, Static Hook, Lunar Hook.
        public override bool? CanUseGrapple(Player player)
        {
            int hooksOut = 0;
            for (int l = 0; l < 1000; l++)
            {
                if (Main.projectile[l].active && Main.projectile[l].owner == Main.myPlayer && Main.projectile[l].type == Projectile.type)
                {
                    hooksOut++;
                }
            }

            return hooksOut <= 1;
        }
        private int frameCounter = 0;
        public override float GrappleRange()
        {
            return 1000f;
        }

        public override void NumGrappleHooks(Player player, ref int numHooks)
        {
            numHooks = 1;
        }

        // default is 11, Lunar is 24
        public override void GrappleRetreatSpeed(Player player, ref float speed)
        {
            speed = 40f;
        }

        public override void GrapplePullSpeed(Player player, ref float speed)
        {
            speed = 30;
        }
        public override bool PreDrawExtras()
        {
            Vector2 playerCenter = Main.player[Projectile.owner].MountedCenter;
            Vector2 center = Projectile.Center;
            Vector2 directionToPlayer = playerCenter - Projectile.Center;
            float chainRotation = directionToPlayer.ToRotation() - MathHelper.PiOver2;
            float distanceToPlayer = directionToPlayer.Length();

            while (distanceToPlayer > 20f && !float.IsNaN(distanceToPlayer))
            {
                directionToPlayer /= distanceToPlayer;
                directionToPlayer *= chainTextures[selectedTexture].Height();

                center += directionToPlayer;
                directionToPlayer = playerCenter - center;
                distanceToPlayer = directionToPlayer.Length();

                Color drawColor = Lighting.GetColor((int)center.X / 16, (int)(center.Y / 16));

                Main.EntitySpriteDraw(chainTextures[selectedTexture].Value, center - Main.screenPosition,
                    chainTextures[selectedTexture].Value.Bounds, drawColor, chainRotation,
                    chainTextures[selectedTexture].Size() * 0.5f, 1f, SpriteEffects.None, 0);
            }
            return false;
        }
    }
}