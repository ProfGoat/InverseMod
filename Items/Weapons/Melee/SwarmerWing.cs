using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace InverseMod.Items.Weapons.Melee
{
    public class SwarmerWing : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Swarmer Wing");
            // Tooltip.SetDefault("Fwoosh.");
        }

        public override void SetDefaults()
        {
            Item.width = 30;
            Item.height = 69;
            Item.damage = 22;
            Item.DamageType = DamageClass.Melee;
            Item.crit = 6;
            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 10;
            Item.value = Item.buyPrice(gold: 1);
            Item.rare = ItemRarityID.Green;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = false;
        }

        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            if (Main.rand.NextBool(5))
            {
                Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, ModContent.DustType<Dusts.SwarmerWingDust>());

            }
        }
    }
}