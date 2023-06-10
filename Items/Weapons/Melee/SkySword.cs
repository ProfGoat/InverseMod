using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace InverseMod.Items.Weapons.Melee
{
    public class SkySword : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Sky Sword");
            Tooltip.SetDefault("Yeah the name is kind of dumb but whatever.");
        }

        public override void SetDefaults()
        {
            Item.width = 68;
            Item.height = 68;
            Item.damage = 30;
            Item.DamageType = DamageClass.Melee;
            Item.crit = 4;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 5;
            Item.value = Item.buyPrice(gold: 1, silver: 5);
            Item.rare = ItemRarityID.Orange;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = false;
            Item.shoot = ProjectileID.HarpyFeather;
            Item.shootSpeed = 10;
        }

        public override void AddRecipes()
        {
            Recipe r = CreateRecipe();
            r.AddIngredient(ItemID.Feather, 12);
            r.AddIngredient(ItemID.SunplateBlock, 15);
            r.AddIngredient(ItemID.FallenStar, 5);
            r.AddTile(TileID.Anvils);
            r.Register();
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            int a = Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI);
            Main.projectile[a].friendly = true;
            Main.projectile[a].hostile = false;
            return false;
        }

        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            if (Main.rand.NextBool(4))
            {
                Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, ModContent.DustType<Dusts.SkySwordDust>());

            }
        }
        public override void OnHitNPC(Player player, NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.Confused, 60);
        }
    }
}