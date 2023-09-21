using InverseMod.Projectiles.Melee;
using System;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace InverseMod.Items.Weapons.Melee.Shortswords
{
    public class FlySnack : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Fly Snack");
            // Tooltip.SetDefault("TownNPC Executioner.\nCan also be used as an immunity shield because why not.");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.damage = 250;
            Item.knockBack = 1;
            Item.useStyle = ItemUseStyleID.Rapier;
            Item.useAnimation = 1;
            Item.useTime = 1;
            Item.width = 36;
            Item.height = 36;
            Item.UseSound = SoundID.Item7;
            Item.DamageType = DamageClass.MeleeNoSpeed;
            Item.autoReuse = true;
            Item.noUseGraphic = true;
            Item.noMelee = true;

            Item.rare = ItemRarityID.Green;
            Item.value = Item.sellPrice(0, 0, 0, 50);

            Item.shoot = ModContent.ProjectileType<FlySnackProjectile>();
            Item.shootSpeed = 2.1f;
        }

        public override void HoldItem(Player player)
        {
            player.immune = true;
            player.immuneNoBlink = true;
            player.immuneTime = 20;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemID.PlatinumShortsword, 1)
            .AddIngredient(ItemID.Buggy, 3)
            .AddTile(LiquidID.Honey)
            .Register();

            CreateRecipe()
            .AddIngredient(ItemID.GoldShortsword, 1)
            .AddIngredient(ItemID.Buggy, 3)
            .AddTile(LiquidID.Honey)
            .Register();

            CreateRecipe()
            .AddIngredient(ItemID.Flymeal, 1)
            .AddTile(TileID.TinkerersWorkbench)
            .Register();
        }
    }
}
