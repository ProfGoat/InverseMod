using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using SubworldLibrary;
using InverseMod.SubWorlds;
using Terraria.DataStructures;

namespace InverseMod.Items.Consumables
{
    internal class SubworldTestItem2 : ModItem
    {
        public override void SetStaticDefaults()
        {
            // Tooltip.SetDefault("Teleports you to the subworld.");
            Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(4, 8));
            ItemID.Sets.AnimatesAsSoul[Item.type] = true;
        }

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.maxStack = 1;
            Item.value = 10000;
            Item.rare = ItemRarityID.Green;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.UseSound = SoundID.Item130;
            Item.consumable = false;
        }

        public override bool? UseItem(Player player)
        {
            SubworldSystem.Exit();
            return true;
        }
    }
}