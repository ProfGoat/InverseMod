using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace InverseMod.Items.Accessories
{
    [AutoloadEquip(EquipType.Balloon)]
    internal class Payload : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 22;
            Item.height = 22;
            Item.value = Item.sellPrice(silver: 80);
            Item.rare = ItemRarityID.Green;
            Item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (player.controlDown)
            {
                // Add code to make the player fall faster here
                player.maxFallSpeed = 20;
            }
        }
        public override bool CanEquipAccessory(Player player, int slot, bool modded)
        {
            bool uraniumPayloadEquipped = player.armor.Any(item => item.type == ModContent.ItemType<UraniumPayload>());

            // If either UraniumPayload or UraniumInABottle is equipped, prevent equipping Payload
            if (uraniumPayloadEquipped)
            {
                return false;
            }

            return true;
        }
        public override void AddRecipes()
        {
            Recipe r = CreateRecipe();
            r.AddIngredient(ItemID.EncumberingStone, 1);
            r.AddIngredient(ItemID.Chain, 10);
            r.AddTile(TileID.Anvils);
            r.Register();
        }
    }
}