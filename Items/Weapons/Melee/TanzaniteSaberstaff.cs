using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using InverseMod.Projectiles.Melee; // Ensure this is the correct namespace
using Terraria.GameContent.Creative;
using InverseMod.Common.Systems;
using Terraria.Audio;

namespace InverseMod.Items.Weapons.Melee
{
    public class TanzaniteSaberstaff : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 105;
            Item.knockBack = 6;
            Item.useStyle = ItemUseStyleID.Thrust;
            Item.useAnimation = 25; // Standard swing animation
            Item.useTime = 25; // Standard use time
            Item.width = 110;
            Item.height = 110;
            Item.UseSound = null;
            Item.DamageType = DamageClass.Melee;
            Item.autoReuse = true;
            Item.rare = ItemRarityID.White;
            Item.value = Item.sellPrice(0, 5, 20, 0);
            Item.shootSpeed = 0f; // Standard shoot speed
            Item.noUseGraphic = true;
            Item.noMelee = true;
        }
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool CanUseItem(Player player)
        {
            // Check for existing active TanzaniteSaberstaffProjectile2
            bool projectileExists = false;
            for (int i = 0; i < Main.maxProjectiles; i++)
            {
                // Check if the projectile is active, belongs to the player, and is of the correct type
                if (Main.projectile[i].active && Main.projectile[i].owner == player.whoAmI && Main.projectile[i].type == ModContent.ProjectileType<TanzaniteSaberstaffProjectile2>())
                {
                    projectileExists = true;
                    break; // Exit the loop early if we find an active projectile
                }
            }

            if (!projectileExists)
            {

                // Play custom sound with pitch variation here
                float pitchOffset = Main.rand.NextFloat(-0.2f, 0.2f);
                SoundStyle customSoundWithPitch = new SoundStyle(rorAudio.Whoosh.SoundPath)
                {
                    Volume = rorAudio.Whoosh.Volume,
                    Pitch = pitchOffset,
                    PitchVariance = 0f,
                    MaxInstances = rorAudio.Whoosh.MaxInstances,
                    Type = rorAudio.Whoosh.Type,
                };
                SoundEngine.PlaySound(customSoundWithPitch, player.position);

                if (player.altFunctionUse == 2) // If the use is a right-click
                {
                    // Set up the item for the boomerang-like behavior
                    Item.shoot = ModContent.ProjectileType<TanzaniteSaberstaffProjectile2>(); // Different projectile for boomerang functionality
                    Item.shootSpeed = 10f; // Adjust the shoot speed for the boomerang
                    Item.autoReuse = false;
                    return true;
                }
                else // Regular left-click
                {
                    Item.shoot = ModContent.ProjectileType<TanzaniteSaberstaffProjectile>(); // Different projectile for boomerang functionality
                    Vector2 positionAbove = player.Center - new Vector2(0, 0);
                    Vector2 swingDirection = new Vector2(0, 0);
                    Projectile.NewProjectile(player.GetSource_ItemUse(Item), positionAbove, swingDirection, Item.shoot, Item.damage, Item.knockBack, player.whoAmI);
                    Item.autoReuse = true;
                    Item.shootSpeed = 0;
                    return true;
                }
            }
            else
            {
                return false; // Prevent the item from being used if a projectile is already active
            }
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(null, "Tanzanite", 15)
            .AddIngredient(ItemID.HallowedBar, 20)
            .AddTile(TileID.MythrilAnvil)
            .Register();
        }
    }
}
