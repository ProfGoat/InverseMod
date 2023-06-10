using InverseMod.Projectiles.Melee;
using System;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace InverseMod.Items.Weapons.Melee.Shortswords
{
    public class ZombieHand : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Zombie Hand");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.damage = 13;
            Item.knockBack = 4;
            Item.useStyle = ItemUseStyleID.Rapier;
            Item.useAnimation = 20;
            Item.useTime = 20;
            Item.width = 32;
            Item.height = 32;
            Item.UseSound = SoundID.Item7;
            Item.DamageType = DamageClass.MeleeNoSpeed;
            Item.autoReuse = false;
            Item.noUseGraphic = true;
            Item.noMelee = true;

            Item.rare = ItemRarityID.White;
            Item.value = Item.sellPrice(0, 0, 2, 50);

            Item.shoot = ModContent.ProjectileType<ZombieHandProjectile>();
            Item.shootSpeed = 2.1f;
        }

        public override void AddRecipes()
        {
            Recipe r = CreateRecipe();
            r.AddIngredient(ItemID.ZombieArm, 1);
            r.AddTile(TileID.Sawmill);
            r.Register();
        }
    }
}
