using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;
using InverseMod.Tiles.Furniture;

namespace InverseMod.Items.Furniture
{
    public class TourmalineGemLock : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.GemLockRuby);
            Item.createTile = ModContent.TileType<TourmalineGemLockTile>();
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<Ores.Tourmaline>(), 5);
            recipe.AddIngredient(ItemID.StoneBlock, 10);
            recipe.AddTile(TileID.HeavyWorkBench);
            recipe.Register();
        }
    }
}