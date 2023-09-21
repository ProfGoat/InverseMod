using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;
using Terraria.ModLoader;

namespace InverseMod.Items.Consumables
{
    internal class GrapheneBunny : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Graphene Bunny");
        }

        public override void SetDefaults()
        {
            Item.useStyle = 1;
            Item.maxStack = 999;
            Item.consumable = true;
            Item.width = 16;
            Item.height = 16;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.noMelee = true;
            Item.UseSound = SoundID.Item1;
            Item.value = 140000;
            Item.rare = 1;
            Item.noUseGraphic = true;
            Item.shoot = ModContent.ProjectileType<Projectiles.Consumables.GrapheneBunnyProjectile>();
        }
    }
}