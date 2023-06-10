using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using Microsoft.Xna.Framework;
using Terraria.GameContent.Creative;
using Terraria.Enums;
using Terraria.Localization;
using System;

namespace InverseMod.Items.Weapons.Melee
{
    public class YardStick : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Yard Stick");
            Tooltip.SetDefault("Terrarians aren't very tall...");
        }

        public override void SetDefaults()
        {
            // If you are using the custom AI in your projectile (and not aiStyle 19 and AIType = ProjectileID.JoustingLance), the standard value is 1f.
            // If you are using aiStyle 19 and AIType = ProjectileID.JoustingLance, then multiply the value by about 3.5f.
            Item.DefaultToSpear(ModContent.ProjectileType<Projectiles.Melee.YardStickProjectile>(), 1f, 24);

            Item.shootSpeed = 0.69f;

            Item.DamageType = DamageClass.MeleeNoSpeed;

            Item.UseSound = SoundID.Item1;

            Item.SetWeaponValues(18, 6f, 0);

            Item.SetShopValues(ItemRarityColor.Blue1, Item.buyPrice(0, 3));

            Item.channel = true;

            Item.StopAnimationOnHurt = true;
        }

        public override bool MeleePrefix()
        {
            return true;
        }

        public override void AddRecipes()
        {
            Recipe r = CreateRecipe();
            r.AddIngredient(ItemID.Ruler, 3);
            r.AddTile(TileID.TinkerersWorkbench);
            r.Register();
        }
    }
}