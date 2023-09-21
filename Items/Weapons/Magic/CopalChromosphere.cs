using InverseMod.Projectiles.Magic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace InverseMod.Items.Weapons.Magic
{
    public class CopalChromosphere : ModItem
    {
        public override void SetStaticDefaults()
        {
            // Tooltip.SetDefault("The wrath of the sun, held within your grasp...");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.damage = 80; // Adjust this for the desired damage
            Item.DamageType = DamageClass.Magic;
            Item.width = 40;  // The width of the item's hitbox in pixels.
            Item.height = 40; // The height of the item's hitbox in pixels.
            Item.useTime = 40; // The time span of using the item in frames.
            Item.useAnimation = 40; // The time span of the using animation of the item in frames.
            Item.reuseDelay = 10;
            Item.staff[Item.type] = true;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noMelee = true; // This ensures the item doesn't do melee damage
            Item.knockBack = 5;  // Adjust this for the desired knockback
            Item.value = 300000;  // Adjust this for the desired value (in copper coins)
            Item.rare = ItemRarityID.Lime; // Adjust this for the desired rarity
            Item.UseSound = SoundID.Item20;  // Adjust this for the desired sound
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<CopalChromosphereProjectile>(); // The type of projectile this item should shoot
            Item.shootSpeed = 16f;  // The speed of the projectile (in pixels per frame)
            Item.mana = 20;  // Adjust this for the desired mana cost
        }

        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                // When the player is right-clicking
                return true; // Or add your condition for right-clicking here
            }
            else
            {
                // When the player is left-clicking
                return player.ownedProjectileCounts[Item.shoot] < 6; // Limit the number of mini suns
            }
        }
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (player.altFunctionUse == 2)
            {
                // Right-click behavior
                int sunsKilled = 0;

                for (int i = 0; i < Main.maxProjectiles; i++)
                {
                    Projectile proj = Main.projectile[i];

                    if (proj.active && proj.type == ModContent.ProjectileType<CopalChromosphereProjectile>() && proj.owner == player.whoAmI)
                    {
                        proj.Kill();
                        sunsKilled++;
                    }
                }

                if (sunsKilled > 0)
                {
                    // Check if a GiantSun already exists
                    if (player.GetModPlayer<InversePlayer>().giantSunProjectile >= 0 && Main.projectile[player.GetModPlayer<InversePlayer>().giantSunProjectile].active && Main.projectile[player.GetModPlayer<InversePlayer>().giantSunProjectile].type == ModContent.ProjectileType<GiantSun>())
                    {
                        // If it does, kill it
                        Main.projectile[player.GetModPlayer<InversePlayer>().giantSunProjectile].Kill();
                    }

                    // Spawn a giant sun above the player
                    Vector2 giantSunPosition = player.Center + new Vector2(0, -200f); // 200 pixels above the player
                    int giantSunDamage = damage * sunsKilled; // Scale damage with the number of killed suns
                    float giantSunScale = 1f + 1f * sunsKilled; // Scale size with the number of killed suns

                    // Spawn the GiantSun and store its ID
                    player.GetModPlayer<InversePlayer>().giantSunProjectile = Projectile.NewProjectile(source, giantSunPosition, Vector2.Zero, ModContent.ProjectileType<GiantSun>(), giantSunDamage, knockback, player.whoAmI, ai0: giantSunScale);
                }
            }
            else
            {
                // Left-click behavior
                Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI);
            }

            return false; // return false so it doesn't shoot the default projectile
        }
        public override void AddRecipes()
        {
            Recipe r = CreateRecipe();
            r.AddIngredient(null, "Copal", 20);
            r.AddIngredient(ItemID.LihzahrdBrick, 15);
            r.AddIngredient(ItemID.EyeoftheGolem, 1);
            r.AddTile(TileID.LihzahrdFurnace);
            r.Register();
        }
    }
}