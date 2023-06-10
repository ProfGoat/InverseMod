using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace InverseMod.Items.Weapons.Melee
{
    public class Asamarum : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Asamarum");
            Tooltip.SetDefault("I think you get the idea...");
        }

        public override void SetDefaults()
        {
            Item.width = 74;
            Item.height = 58;
            Item.damage = 16;
            Item.DamageType = DamageClass.Melee;
            Item.crit = 8;
            Item.useTime = 10;
            Item.useAnimation = 10;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 1;
            Item.value = Item.buyPrice(gold: 5);
            Item.rare = ItemRarityID.Green;
            Item.UseSound = SoundID.Item7;
            Item.autoReuse = true;
        }

        public override void AddRecipes()
        {
            Recipe r = CreateRecipe();
            r.AddIngredient(ItemID.Muramasa, 1);
            r.AddIngredient(ItemID.Wood, 10);
            r.AddTile(TileID.Anvils);
            r.Register();
        }

        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            if (Main.rand.NextBool(1))
            {
                Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, ModContent.DustType<Dusts.AsamarumDust>());

            }
        }
    }
}