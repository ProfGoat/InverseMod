using InverseMod.Projectiles.Magic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.ID;
using Terraria.ModLoader;

namespace InverseMod.Items.Weapons.Magic
{
    internal class JadeDragonStaff : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 120;
            Item.DamageType = DamageClass.Magic;
            Item.width = 50;
            Item.height = 50;
            Item.useTime = 50;
            Item.useAnimation = 50;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.staff[Item.type] = true;
            Item.noMelee = true;
            Item.knockBack = 7;
            Item.value = 200000;
            Item.rare = ItemRarityID.LightRed;
            Item.UseSound = SoundID.Item32;
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<JadeDragonStaffProjectile>();
            Item.shootSpeed = 7;
            Item.crit = 8;
            Item.mana = 30;
        }
        public override void AddRecipes()
        {
            Recipe r = CreateRecipe();
            r.AddIngredient(null, "Jade", 20);
            r.AddIngredient(ItemID.TitaniumBar, 15);
            r.AddTile(TileID.Anvils);
            r.Register();
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Vector2 muzzleOffset = Vector2.Normalize(new Vector2(velocity.X, velocity.Y)) * 50f;
            if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
            {
                position += muzzleOffset;
            }


            if (player.altFunctionUse == 2)
            {

            }
            else
            {

            }


            int index = Projectile.NewProjectile(source, position, velocity, type, Item.damage, knockback, player.whoAmI, 0f);
            Main.projectile[index].originalDamage = Item.damage;
            return false;

        }
    }
}