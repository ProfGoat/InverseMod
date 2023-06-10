using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace InverseMod.Items.Weapons.Melee
{
    public class Days_Frontier : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Day's Frontier");
            Tooltip.SetDefault("Sworn enemy of the Night's Edge.");
        }

        public override void SetDefaults()
        {
            Item.width = 50;
            Item.height = 55;
            Item.damage = 50;
            Item.DamageType = DamageClass.Melee;
            Item.crit = 6;
            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 9;
            Item.value = Item.buyPrice(gold: 5);
            Item.rare = ItemRarityID.Orange;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
        }

        public override void AddRecipes()
        {
            Recipe r = CreateRecipe();
            r.AddIngredient(null, "Dark_Destroyer", 1);
            r.AddIngredient(null, "SkySword", 1);
            r.AddIngredient(null, "Asamarum", 1);
            r.AddIngredient(null, "Storm", 1);
            r.AddTile(TileID.DemonAltar);
            r.Register();
        }
        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            if (Main.rand.NextBool(1))
            {
                Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, ModContent.DustType<Dusts.InverseModDust>());

            }
        }
        public override void OnHitNPC(Player player, NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.OnFire, 300);
        }
    }
}