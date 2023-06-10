using InverseMod.Items.Bars;
using InverseMod.Projectiles;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace InverseMod.Items.Weapons.Ranged
{
    public class HarpyParty : ModItem
    {
        private int shotCounter = 0;
        private int shotDelay = 7;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Harpy Party");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.damage = 22;
            Item.knockBack = 2;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.useAnimation = 30;
            Item.useTime = 30;
            Item.width = 24;
            Item.height = 20;
            Item.UseSound = SoundID.Item7;
            Item.DamageType = DamageClass.Ranged;
            Item.autoReuse = true;
            Item.noMelee = true;

            Item.rare = ItemRarityID.Blue;
            Item.value = Item.sellPrice(0, 2, 20, 5);

            Item.useAmmo = AmmoID.Arrow;
            Item.shoot = ProjectileID.HarpyFeather;
            Item.shootSpeed = 6;
        }

        public override void AddRecipes()
        {
            Recipe r = CreateRecipe();
            r.AddIngredient(ItemID.Feather, 15);
            r.AddIngredient(ItemID.SunplateBlock, 10);
            r.AddTile(TileID.Anvils);
            r.Register();
        }
        public override bool CanUseItem(Player player)
        {
            if (shotCounter <= 0)
            {
                shotCounter = 3;
            }
            return base.CanUseItem(player);
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (shotCounter > 0)
            {
                shotCounter--;

                // Add random angle to the projectile velocity
                float spread = 4f; // The angle spread in degrees
                float angle = MathHelper.ToRadians(spread);

                Vector2 rotatedVelocity = velocity.RotatedBy(Main.rand.NextFloat(-angle, angle));

                int a = Projectile.NewProjectile(source, position, rotatedVelocity, ProjectileID.HarpyFeather, damage, knockback, player.whoAmI);
                Main.projectile[a].friendly = true;
                Main.projectile[a].hostile = false;

                if (shotCounter > 0)
                {
                    Item.useTime = shotDelay;
                    Item.useAnimation = shotDelay;
                }
                else
                {
                    Item.useTime = 30;
                    Item.useAnimation = 30;
                }
            }
            return false;
        }
    }
}