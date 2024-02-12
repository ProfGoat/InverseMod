using InverseMod.Projectiles.Magic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.ID;
using Terraria.ModLoader;

namespace InverseMod.Items.Weapons.Magic
{
    internal class TanzaniteTidalwave : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 30;
            Item.DamageType = DamageClass.Magic;
            Item.width = 50;
            Item.height = 50;
            Item.useTime = 4;
            Item.useAnimation = 4;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.staff[Item.type] = true;
            Item.noMelee = true;
            Item.knockBack = 7;
            Item.value = 300000;
            Item.rare = ItemRarityID.LightRed;
            Item.UseSound = SoundID.Item32;
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<TanzaniteTidalwaveProjectile>();
            Item.shootSpeed = 20;
            Item.crit = 8;
            Item.mana = 3;
        }
        public override void AddRecipes()
        {
            Recipe r = CreateRecipe();
            r.AddIngredient(null, "Tanzanite", 20);
            r.AddIngredient(ItemID.MythrilBar, 15);
            r.AddTile(TileID.Anvils);
            r.Register();
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            int proj1 = Projectile.NewProjectile(source, position, Vector2.Zero, ModContent.ProjectileType<TanzaniteTidalwaveProjectile>(), damage, knockback, player.whoAmI);
            Main.projectile[proj1].ai[0] = 0;

            int proj2 = Projectile.NewProjectile(source, position, Vector2.Zero, ModContent.ProjectileType<TanzaniteTidalwaveProjectile>(), damage, knockback, player.whoAmI);
            Main.projectile[proj2].ai[0] = 1;

            int proj3 = Projectile.NewProjectile(source, position, Vector2.Zero, ModContent.ProjectileType<TanzaniteTidalwaveProjectile>(), damage, knockback, player.whoAmI);
            Main.projectile[proj3].ai[0] = 2;

            int proj4 = Projectile.NewProjectile(source, position, Vector2.Zero, ModContent.ProjectileType<TanzaniteTidalwaveProjectile>(), damage, knockback, player.whoAmI);
            Main.projectile[proj4].ai[0] = 3;

            int proj5 = Projectile.NewProjectile(source, position, Vector2.Zero, ModContent.ProjectileType<TanzaniteTidalwaveProjectile>(), damage, knockback, player.whoAmI);
            Main.projectile[proj5].ai[0] = 4;

            int proj6 = Projectile.NewProjectile(source, position, Vector2.Zero, ModContent.ProjectileType<TanzaniteTidalwaveProjectile>(), damage, knockback, player.whoAmI);
            Main.projectile[proj6].ai[0] = 5;

            return false;

        }
    }
}