using InverseMod.Projectiles.Magic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace InverseMod.Items.LargeGems
{
    public class LargeTourmaline : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Large Tourmaline");
            Tooltip.SetDefault("For Capture the Gem. It drops when you die");
        }

        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.LargeAmethyst);
            Item.width = 32;
            Item.height = 32;
            Item.rare = ItemRarityID.Cyan;
        }
        public override bool PreDrawInWorld(
        SpriteBatch spriteBatch,
        Color lightColor,
        Color alphaColor,
        ref float rotation,
        ref float scale,
        int whoAmI)
        {
            Texture2D texture2D = ModContent.Request<Texture2D>("InverseMod/Items/LargeGems/LargeTourmaline").Value;
            Vector2 vector2_1 = Vector2.Divide(Utils.Size(texture2D), 2f);
            Vector2 vector2_2 = new Vector2(Item.width / 2 - texture2D.Width / 2, Item.height - texture2D.Height + 2f);
            Vector2 vector2_3 = Vector2.Add(Vector2.Add(Vector2.Subtract(Item.position, Main.screenPosition), vector2_1), vector2_2);
            spriteBatch.Draw(texture2D, vector2_3, new Rectangle?(), new Color(250, 250, 250, Main.mouseTextColor / 2), rotation, vector2_1, (float)(Main.mouseTextColor / 1000.0 + 0.800000011920929), 0, 0.0f);
            return false;
        }
        public override void AddRecipes()
        {
            Recipe r = CreateRecipe();
            r.AddIngredient(null, "Tourmaline", 15);
            r.AddTile(TileID.MythrilAnvil);
            r.Register();
        }
    }
}