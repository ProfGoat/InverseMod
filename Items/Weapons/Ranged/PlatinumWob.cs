using InverseMod.Projectiles;
using System;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace InverseMod.Items.Weapons.Ranged
{
    public class PlatinumWob : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Platinum Wob");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.damage = 18;
            Item.knockBack = 3f;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.useAnimation = 25;
            Item.useTime = 25;
            Item.width = 16;
            Item.height = 32;
            Item.UseSound = SoundID.Item7;
            Item.DamageType = DamageClass.Ranged;
            Item.autoReuse = true;
            Item.noMelee = true;

            Item.rare = ItemRarityID.White;
            Item.value = Item.sellPrice(0, 0, 23, 10);

            Item.useAmmo = AmmoID.Arrow;
            Item.shoot = ProjectileID.PurificationPowder;
            Item.shootSpeed = -16;
        }

        public override void AddRecipes()
        {
            Recipe r = CreateRecipe();
            r.AddIngredient(ItemID.PlatinumBar, 9);
            r.AddTile(TileID.Anvils);
            r.Register();
        }
    }
}
