using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace InverseMod.Items.Accessories
{
    [AutoloadEquip(EquipType.Balloon)]
    internal class UraniumPayload : ModItem
    {
        public int speed;
        public override void SetDefaults()
        {
            Item.width = 22;
            Item.height = 22;
            Item.value = Item.sellPrice(gold: 10);
            Item.rare = ItemRarityID.Orange;
            Item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (player.controlDown)
            {
                player.GetModPlayer<InversePlayer>().uraniumInABottle = true;
                player.GetJumpState<UraniumBoostJump>().Enable();
                // Add code to make the player fall faster here
                player.maxFallSpeed = speed;
                speed = speed + 10;
            }
        }
        public override bool CanEquipAccessory(Player player, int slot, bool modded)
        {
            // Check if the player has Payload or UraniumInABottle equipped
            bool payloadEquipped = player.armor.Any(item => item.type == ModContent.ItemType<Payload>());
            bool uraniumInABottleEquipped = player.armor.Any(item => item.type == ModContent.ItemType<UraniumInABottle>());

            // If either Payload or UraniumInABottle is equipped, prevent equipping UraniumPayload
            if (payloadEquipped || uraniumInABottleEquipped)
            {
                return false;
            }

            return true;
        }
        public override void AddRecipes()
        {
            Recipe r = CreateRecipe();
            r.AddIngredient(ModContent.ItemType<UraniumInABottle>(), 1);
            r.AddIngredient(ModContent.ItemType<Payload>(), 1);
            r.AddTile(TileID.TinkerersWorkbench);
            r.Register();
        }
    }
}