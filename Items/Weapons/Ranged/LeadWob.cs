using InverseMod.Projectiles;
using System;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace InverseMod.Items.Weapons.Ranged
{
    public class LeadWob : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Lead Wob");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.damage = 14;
            Item.knockBack = 3f;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.useAnimation = 27;
            Item.useTime = 27;
            Item.width = 14;
            Item.height = 32;
            Item.UseSound = SoundID.Item7;
            Item.DamageType = DamageClass.Ranged;
            Item.autoReuse = true;
            Item.noMelee = true;

            Item.rare = ItemRarityID.White;
            Item.value = Item.sellPrice(0, 0, 5, 5);

            Item.useAmmo = AmmoID.Arrow;
            Item.shoot = ProjectileID.PurificationPowder;
            Item.shootSpeed = -16;
        }

        public override void AddRecipes()
        {
            Recipe r = CreateRecipe();
            r.AddIngredient(ItemID.LeadBar, 9);
            r.AddTile(TileID.Anvils);
            r.Register();
        }
    }
}
