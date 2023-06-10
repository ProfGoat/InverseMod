using InverseMod.Projectiles.Magic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.ID;
using Terraria.ModLoader;

namespace InverseMod.Items.Weapons.Magic
{
    public class CitrineCatalyst : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 36;
            Item.DamageType = DamageClass.Magic;
            Item.width = 50;
            Item.height = 50;
            Item.useTime = 110;
            Item.useAnimation = 110;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.staff[Item.type] = true;
            Item.noMelee = true;
            Item.knockBack = 7;
            Item.value = 200000;
            Item.rare = ItemRarityID.LightRed;
            Item.UseSound = SoundID.Item113;
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<CitrineCatalystProjectile>();
            Item.shootSpeed = 7;
            Item.crit = 8;
            Item.mana = 50;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            // Spawn 5 projectiles around the player
            for (int i = 0; i < 5; i++)
            {
                int proj = Projectile.NewProjectile(source, position, Vector2.Zero, ModContent.ProjectileType<CitrineCatalystProjectile>(), damage, knockback, player.whoAmI);
                Main.projectile[proj].ai[0] = i * 72 - 3; // Initial angle for each projectile adjusted by -3
            }

            return false;
        }
        public override void AddRecipes()
        {
            Recipe r = CreateRecipe();
            r.AddIngredient(null, "Citrine", 20);
            r.AddIngredient(ItemID.HellstoneBar, 15);
            r.AddTile(TileID.Anvils);
            r.Register();
        }
    }
}