using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.GameContent.Creative;

namespace InverseMod.Items.Weapons.Melee
{
    public class GlovedBlade : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Gloved Blade");
            // Tooltip.SetDefault("Kinda hurts to hold.");
        }

        public override void SetDefaults()
        {
            Item.width = 36;
            Item.height = 38;
            Item.damage = 5;
            Item.DamageType = DamageClass.Melee;
            Item.crit = 4;
            Item.useTime = 10;
            Item.useAnimation = 10;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 30f;
            Item.value = Item.buyPrice(gold: 1, silver: 5);
            Item.rare = ItemRarityID.Green;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemID.TitanGlove)
            .AddIngredient(ItemID.PlatinumBroadsword)
            .AddTile(TileID.TinkerersWorkbench)
            .Register();

            CreateRecipe()
            .AddIngredient(ItemID.BladedGlove)
            .AddTile(TileID.TinkerersWorkbench)
            .Register();
        }

        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.Confused, 60);
        }
    }
}