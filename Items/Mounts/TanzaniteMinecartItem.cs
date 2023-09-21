using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using InverseMod;
using InverseMod.Mounts;

namespace InverseMod.Items.Mounts
{
    public class TanzaniteMinecartItem : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Tanzanite Minecart");
            // Tooltip.SetDefault("Sparkles.");

            MountID.Sets.Cart[Item.mountType] = true;
        }

        public override void SetDefaults()
        {
            Item.width = 48;
            Item.height = 32;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.value = 1066560;
            Item.rare = ItemRarityID.LightRed;
            Item.UseSound = SoundID.Item69; //nice
            Item.noMelee = true;
            Item.mountType = ModContent.MountType<TanzaniteMinecart>();
        }

        public override bool CanUseItem(Player player) => false; //the player shouldn't be able to use this item but they can so that's cool I guess don't worry
        public override void AddRecipes()
        {
            Recipe r = CreateRecipe();
            r.AddIngredient(ItemID.Minecart, 1);
            r.AddIngredient(null, "LargeTanzanite", 1);
            r.AddTile(TileID.MythrilAnvil);
            r.Register();
        }
    }
}