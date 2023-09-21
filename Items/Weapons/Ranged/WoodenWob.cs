using InverseMod.Projectiles;
using System;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace InverseMod.Items.Weapons.Ranged
{
    public class WoodenWob : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Wooden Wob");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.damage = 8;
            Item.knockBack = 3f;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.useAnimation = 30;
            Item.useTime = 30;
            Item.width = 14;
            Item.height = 32;
            Item.UseSound = SoundID.Item7;
            Item.DamageType = DamageClass.Ranged;
            Item.autoReuse = false;
            Item.noMelee = true;

            Item.rare = ItemRarityID.White;
            Item.value = Item.sellPrice(0, 0, 0, 30);

            Item.useAmmo = AmmoID.Arrow;
            Item.shoot = ProjectileID.PurificationPowder;
            Item.shootSpeed = -15;
        }

        public override void AddRecipes()
        {
            Recipe r = CreateRecipe();
            r.AddIngredient(ItemID.Wood, 10);
            r.AddTile(TileID.WorkBenches);
            r.Register();
        }
    }
}
