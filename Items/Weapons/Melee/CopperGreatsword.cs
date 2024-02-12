using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using InverseMod.Common.Systems;
using InverseMod.Projectiles.Melee; // Ensure this is the correct namespace
using Terraria.GameContent.Creative;
using Terraria.DataStructures;
using Terraria.Audio;

namespace InverseMod.Items.Weapons.Melee
{
    public class CopperGreatsword : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 25;
            Item.knockBack = 8;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useAnimation = 40; // Standard swing animation
            Item.useTime = 40; // Standard use time
            Item.width = 62;
            Item.height = 62;
            Item.UseSound = rorAudio.Slash;
            Item.DamageType = DamageClass.Melee;
            Item.autoReuse = false;
            Item.rare = ItemRarityID.White;
            Item.value = Item.sellPrice(0, 0, 0, 90);
            Item.shoot = ModContent.ProjectileType<CopperGreatswordProjectile>();
            Item.shootSpeed = 0f; // Standard shoot speed
            Item.noUseGraphic = true;
            Item.noMelee = true;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemID.CopperBar, 12)
            .AddTile(TileID.Anvils)
            .Register();
        }
    }
}
