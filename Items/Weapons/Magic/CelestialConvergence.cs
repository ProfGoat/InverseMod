using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace InverseMod.Items.Weapons.Magic
{
    public class CelestialConvergence : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Celestial Convergence");
            Tooltip.SetDefault("Summons celestial elements that converge at the cursor's location");
            Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(4, 16));
            ItemID.Sets.AnimatesAsSoul[Item.type] = true;
        }

        public override void SetDefaults()
        {
            Item.damage = 100;
            Item.DamageType = DamageClass.Magic;
            Item.width = 62;
            Item.height = 62;
            Item.useTime = 40;
            Item.useAnimation = 40;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noMelee = true;
            Item.knockBack = 6;
            Item.value = Item.sellPrice(gold: 10);
            Item.rare = ItemRarityID.Cyan;
            Item.UseSound = SoundID.Item20;
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<Projectiles.Magic.CelestialElement>();
            Item.shootSpeed = 0f;
            Item.mana = 15;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            for (int i = 0; i < 4; i++)
            {
                int projectileId = Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI, i);
            }
            return false;
        }
        public override void AddRecipes()
        {
            Recipe r = CreateRecipe();
            r.AddIngredient(null, "Graphene", 20);
            r.AddIngredient(ItemID.FragmentNebula, 10);
            r.AddIngredient(ItemID.FragmentVortex, 10);
            r.AddIngredient(ItemID.FragmentSolar, 10);
            r.AddIngredient(ItemID.FragmentStardust, 10);
            r.AddTile(TileID.LunarCraftingStation);
            r.Register();
        }

    }
}