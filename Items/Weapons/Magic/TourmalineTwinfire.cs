using InverseMod.Projectiles.Magic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.ID;
using Terraria.ModLoader;

namespace InverseMod.Items.Weapons.Magic
{
    public class TourmalineTwinfire : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 36;
            Item.DamageType = DamageClass.Magic;
            Item.width = 50;
            Item.height = 50;
            Item.useTime = 15;
            Item.useAnimation = 15;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.staff[Item.type] = true;
            Item.noMelee = true;
            Item.knockBack = 7;
            Item.value = 250000;
            Item.rare = ItemRarityID.LightRed;
            Item.UseSound = SoundID.Item32;
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<CursedFlameFireball>();
            Item.shootSpeed = 7;
            Item.crit = 8;
            Item.mana = 8;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {

            Vector2 muzzleOffset = Vector2.Normalize(new Vector2(velocity.X, velocity.Y)) * 50f;
            if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
            {
                position += muzzleOffset;
            }
            // Spawn the Cursed Flame Fireball
            Projectile.NewProjectile(source, position, velocity, ModContent.ProjectileType<CursedFlameFireball>(), damage, knockback, player.whoAmI);

            // Spawn the Shadow Flame Fireball
            Projectile.NewProjectile(source, position, velocity, ModContent.ProjectileType<ShadowflameFireball>(), damage, knockback, player.whoAmI);

            // Return false so that Terraria does not automatically spawn a projectile.
            return false;
        }
        public override void AddRecipes()
        {
            Recipe r = CreateRecipe();
            r.AddIngredient(null, "Tourmaline", 25);
            r.AddTile(TileID.Anvils);
            r.Register();
        }
    }
}