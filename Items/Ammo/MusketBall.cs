using System;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using InverseMod;
using Microsoft.Xna.Framework;
using Terraria.GameContent.Creative;

namespace InverseMod.Items.Ammo
{
    public class MusketBall : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Musket Ball..?"); // The item's description, can be set to whatever you want.
            Tooltip.SetDefault("Huh, that's pretty weird.");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 99;
        }

        public override void SetDefaults()
        {
            Item.damage = 7; // The damage for projectiles isn't actually 12, it actually is the damage combined with the projectile and the item together.
            Item.DamageType = DamageClass.Ranged;
            Item.width = 8;
            Item.height = 8;
            Item.maxStack = 9999;
            Item.consumable = true; // This marks the item as consumable, making it automatically be consumed when it's used as ammunition, or something else, if possible.
            Item.knockBack = 2;
            Item.value = 7;
            Item.rare = ItemRarityID.White;
            Item.shoot = ModContent.ProjectileType<Projectiles.Ranged.MusketBall>(); // The projectile that weapons fire when using this item as ammunition.
            Item.shootSpeed = 4; // The speed of the projectile.
            Item.ammo = AmmoID.Bullet; // The ammo class this ammo belongs to.
        }

        // Please see Content/ExampleRecipes.cs for a detailed explanation of recipe creation.
        public override void AddRecipes()
        {
            Recipe r = CreateRecipe(50);
            r.AddIngredient(ItemID.MusketBall, 50);
            r.AddTile(TileID.Anvils);
            r.Register();
        }
    }
}
