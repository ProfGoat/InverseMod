using Microsoft.Xna.Framework;
using Mono.Cecil;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace InverseMod.Items.Weapons.Ranged
{
    public class SacredFeatherBow : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Sacred Feather Bow");
            Tooltip.SetDefault("Ascends your enemies from the mortal plane.");
        }

        public override void SetDefaults()
        {
            Item.damage = 18;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 40;
            Item.height = 20;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noMelee = true;
            Item.knockBack = 2;
            Item.value = Item.sellPrice(0, 1, 22, 0);
            Item.rare = ItemRarityID.Green;
            Item.UseSound = SoundID.Item32;
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<Projectiles.Ranged.SacredFeather>();
            Item.shootSpeed = 16f;
            Item.useAmmo = AmmoID.Arrow;
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-3, 0);
        }

        public override void AddRecipes()
        {
            Recipe r = CreateRecipe();
            r.AddIngredient(ItemID.BloodRainBow, 1);
            r.AddIngredient(null, "AngeliteBar", 8);
            r.AddTile(TileID.Anvils);
            r.Register();
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            int numberOfProjectiles = 3;
            for (int i = 0; i < numberOfProjectiles; i++)
            {
                // Get random angle for the falling projectiles
                float angle = MathHelper.ToRadians(Main.rand.Next(-25, 16));
                Vector2 offset = new Vector2(0f, -1f).RotatedBy(angle) * 3f; // Apply a falling speed (3f in this case)

                // Set spawn position near the mouse cursor
                float offsetX = Main.rand.Next(-70, 71);
                float offsetY = Main.rand.Next(-20, 50);
                Vector2 spawnPosition = Main.MouseWorld + new Vector2(offsetX, offsetY);

                // Create the projectile
                Projectile.NewProjectile(source, spawnPosition, offset, ModContent.ProjectileType<Projectiles.Ranged.SacredFeather>(), damage, knockback, player.whoAmI);
            }
            return false;
        }
    }
}
