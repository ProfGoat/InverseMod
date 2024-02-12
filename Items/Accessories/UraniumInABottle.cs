using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.Map;
using System.Linq;

namespace InverseMod.Items.Accessories
{
    internal class UraniumInABottle : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.value = Item.sellPrice(gold: 5);
            Item.rare = ItemRarityID.Green;
            Item.accessory = true;
            Item.UseSound = SoundID.Item20;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<InversePlayer>().uraniumInABottle = true;
            player.GetJumpState<UraniumBoostJump>().Enable();
        }
        public override bool CanEquipAccessory(Player player, int slot, bool modded)
        {
            bool uraniumPayloadEquipped = player.armor.Any(item => item.type == ModContent.ItemType<UraniumPayload>());

            if (uraniumPayloadEquipped)
            {
                return false;
            }

            return true;
        }
    }
    public class UraniumBoostJump : ExtraJump
    {
        public override Position GetDefaultPosition() => AfterBottleJumps;

        public override void OnStarted(Player player, ref bool playSound)
        {
            // Adjust player's velocity for downward boost
            player.velocity.Y += 10f; // Adjust the value as needed

            // Add dust particles
            int offsetY = player.height;
            if (player.gravDir == -1f)
                offsetY = 0;

            offsetY -= 16;

            for (int i = 0; i < 10; i++)
            {
                Dust dust = Dust.NewDustDirect(player.position + new Vector2(-34f, offsetY), 102, 32, DustID.GemEmerald, -player.velocity.X * 0.5f, player.velocity.Y * 0.5f, 100, Color.LawnGreen, 1.5f);
                dust.velocity = dust.velocity * 0.5f - player.velocity * new Vector2(0.1f, 0.3f);
            }

            playSound = true; // Play jump sound
        }

        public override void OnEnded(Player player)
        {
            // Add any visuals or effects you want when the jump ends
        }

        public override float GetDurationMultiplier(Player player)
        {
            return -20; // Adjust as needed
        }

        public override void UpdateHorizontalSpeeds(Player player)
        {
            // Adjust horizontal speed if needed
            player.runAcceleration += 0.1f; // Example adjustment
            player.maxRunSpeed += 1f; // Example adjustment
        }
    }
}