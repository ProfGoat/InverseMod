using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace InverseMod.Items.Weapons.Melee
{
    public class Dark_Destroyer : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Dark Destroyer");
            Tooltip.SetDefault("Destroys all darkness, as long as it's pretty weak.");
        }

        public override void SetDefaults()
        {
            Item.width = 45;
            Item.height = 45;
            Item.damage = 20;
            Item.DamageType = DamageClass.Melee;
            Item.crit = 4;
            Item.useTime = 22;
            Item.useAnimation = 22;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 5;
            Item.value = Item.buyPrice(gold: 1, silver: 50);
            Item.rare = ItemRarityID.Blue;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = false;
        }

        public override void AddRecipes()
        {
            Recipe r = CreateRecipe();
            r.AddIngredient(null, "AngeliteBar", 10);
            r.AddTile(TileID.Anvils);
            r.Register();
        }
        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            if (Main.rand.NextBool(4))
            {
                Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, ModContent.DustType<Dusts.DarkDestroyerDust>());

            }
        }
    }
}