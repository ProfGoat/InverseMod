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
    public class UltimaSaberstaff : ModItem
    {
        private int currentProjectileIndex = 0; // Added variable to keep track of the current projectile type

        // Array of projectile types to cycle through
        private int[] saberstaffProjectile2Types = new int[]
        {
            ModContent.ProjectileType<CitrineSaberstaffProjectile2>(),
            ModContent.ProjectileType<TourmalineSaberstaffProjectile2>(),
            ModContent.ProjectileType<TanzaniteSaberstaffProjectile2>(),
            ModContent.ProjectileType<JadeSaberstaffProjectile2>(),
            ModContent.ProjectileType<GarnetSaberstaffProjectile2>(),
            ModContent.ProjectileType<CopalSaberstaffProjectile2>(),
            ModContent.ProjectileType<GrapheneSaberstaffProjectile2>()
        };
        private int[] saberstaffProjectileTypes = new int[]
        {
            ModContent.ProjectileType<CitrineSaberstaffProjectile>(),
            ModContent.ProjectileType<TourmalineSaberstaffProjectile>(),
            ModContent.ProjectileType<TanzaniteSaberstaffProjectile>(),
            ModContent.ProjectileType<JadeSaberstaffProjectile>(),
            ModContent.ProjectileType<GarnetSaberstaffProjectile>(),
            ModContent.ProjectileType<CopalSaberstaffProjectile>(),
            ModContent.ProjectileType<GrapheneSaberstaffProjectile>(),
            ModContent.ProjectileType<DarkSaberstaffProjectile>()
        };
        public override void SetDefaults()
        {
            Item.damage = 300;
            Item.knockBack = 6;
            Item.useStyle = ItemUseStyleID.Thrust;
            Item.useAnimation = 10; // Standard swing animation
            Item.useTime = 10; // Standard use time
            Item.width = 110;
            Item.height = 110;
            Item.UseSound = null;
            Item.DamageType = DamageClass.Melee;
            Item.autoReuse = true;
            Item.rare = ItemRarityID.White;
            Item.value = Item.sellPrice(1, 20, 0, 0);
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
                Item.shoot = saberstaffProjectile2Types[currentProjectileIndex];
                currentProjectileIndex = (currentProjectileIndex + 1) % saberstaffProjectile2Types.Length; // Increment and loop back to 0 if necessary

                Item.shootSpeed = 16f; // Adjust the shoot speed for the boomerang
                return true;
            }
            else // Regular left-click
            {
                Item.shoot = saberstaffProjectileTypes[currentProjectileIndex];
                currentProjectileIndex = (currentProjectileIndex + 1) % saberstaffProjectileTypes.Length; // Increment and loop back to 0 if necessary
                Vector2 positionAbove = player.Center - new Vector2(0, 0);
                Vector2 swingDirection = new Vector2(0, 0);
                Projectile.NewProjectile(player.GetSource_ItemUse(Item), positionAbove, swingDirection, Item.shoot, Item.damage, Item.knockBack, player.whoAmI);
                Item.shootSpeed = 0;
                return true;
            }
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(null, "CitrineSaberstaff")
            .AddIngredient(null, "TourmalineSaberstaff")
            .AddIngredient(null, "TanzaniteSaberstaff")
            .AddIngredient(null, "JadeSaberstaff")
            .AddIngredient(null, "GarnetSaberstaff")
            .AddIngredient(null, "CopalSaberstaff")
            .AddIngredient(null, "CelestialSaberstaff")
            .AddTile(TileID.LunarCraftingStation)
            .Register();
        }
    }
}
