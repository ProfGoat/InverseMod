using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace InverseMod.Items.Weapons.Melee
{
    public class Storm : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Storm");
            Tooltip.SetDefault("Zeus's childhood toy.");
        }

        public override void SetDefaults()
        {
            Item.width = 50;
            Item.height = 55;
            Item.damage = 45;
            Item.DamageType = DamageClass.Melee;
            Item.crit = 6;
            Item.useTime = 45;
            Item.useAnimation = 45;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 4;
            Item.value = Item.buyPrice(gold: 1, silver: 75);
            Item.rare = ItemRarityID.Orange;
            Item.UseSound = SoundID.Item66;
            Item.autoReuse = false;
        }

        public override void AddRecipes()
        {
            Recipe r = CreateRecipe();
            r.AddIngredient(ItemID.RainCloud, 30);
            r.AddIngredient(ItemID.HellstoneBar, 20);
            r.AddTile(TileID.Anvils);
            r.Register();
        }
        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            if (Main.rand.NextBool(1))
            {
                Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, ModContent.DustType<Dusts.StormDust>());

            }
        }
        public override void OnHitNPC(Player player, NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.Electrified, 300);
        }
    }
}