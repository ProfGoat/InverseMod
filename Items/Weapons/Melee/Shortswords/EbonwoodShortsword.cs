using InverseMod.Projectiles.Melee;
using System;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace InverseMod.Items.Weapons.Melee.Shortswords
{
    public class EbonwoodShortsword : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ebonwood Shortsword");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.damage = 9;
            Item.knockBack = 5;
            Item.useStyle = ItemUseStyleID.Rapier;
            Item.useAnimation = 14;
            Item.useTime = 14;
            Item.width = 32;
            Item.height = 32;
            Item.UseSound = SoundID.Item7;
            Item.DamageType = DamageClass.MeleeNoSpeed;
            Item.autoReuse = false;
            Item.noUseGraphic = true;
            Item.noMelee = true;

            Item.rare = ItemRarityID.White;
            Item.value = Item.sellPrice(0, 0, 1, 50);

            Item.shoot = ModContent.ProjectileType<EbonwoodShortswordProjectile>();
            Item.shootSpeed = 2.1f;
        }

        public override void AddRecipes()
        {
            Recipe r = CreateRecipe();
            r.AddIngredient(ItemID.Ebonwood, 5);
            r.AddTile(TileID.WorkBenches);
            r.Register();
        }
    }
}
