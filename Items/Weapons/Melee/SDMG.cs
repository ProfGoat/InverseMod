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
    public class SDMG : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("S.D.M.G.");
            Tooltip.SetDefault("Stupid Dumb Midget Guy");
        }

        public override void SetDefaults()
        {
            // If you are using the custom AI in your projectile (and not aiStyle 19 and AIType = ProjectileID.JoustingLance), the standard value is 1f.
            // If you are using aiStyle 19 and AIType = ProjectileID.JoustingLance, then multiply the value by about 3.5f.
            Item.DefaultToSpear(ModContent.ProjectileType<Projectiles.Melee.SDMGProjectile>(), 1f, 24);

            Item.shootSpeed = 0.69f;

            Item.DamageType = DamageClass.MeleeNoSpeed;

            Item.UseSound = new SoundStyle("InverseMod/Assets/Sounds/SDMG");

            Item.SetWeaponValues(10, 2f, 6);

            Item.SetShopValues(ItemRarityColor.StrongRed10, Item.buyPrice(0, 0, 2));

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
            r.AddIngredient(ItemID.Mushroom, 50);
            r.AddTile(TileID.WorkBenches);
            r.Register();
        }
    }
}