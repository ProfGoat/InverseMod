using InverseMod.Projectiles;
using System;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace InverseMod.Items.Weapons.Ranged
{
    public class TungstenWob : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Tungsten Wob");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.damage = 15;
            Item.knockBack = 3f;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.useAnimation = 26;
            Item.useTime = 26;
            Item.width = 14;
            Item.height = 32;
            Item.UseSound = SoundID.Item7;
            Item.DamageType = DamageClass.Ranged;
            Item.autoReuse = true;
            Item.noMelee = true;

            Item.rare = ItemRarityID.White;
            Item.value = Item.sellPrice(0, 0, 11, 90);

            Item.useAmmo = AmmoID.Arrow;
            Item.shoot = ProjectileID.PurificationPowder;
            Item.shootSpeed = -16;
        }

        public override void AddRecipes()
        {
            Recipe r = CreateRecipe();
            r.AddIngredient(ItemID.TungstenBar, 9);
            r.AddTile(TileID.Anvils);
            r.Register();
        }
    }
}
