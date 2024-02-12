using InverseMod.Items.Ores;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using InverseMod.Items.Ores;

namespace InverseMod.Common
{
    public class ExtractinatorGlobalItem : GlobalItem
    {
        public override void ExtractinatorUse(int extractType, int extractinatorBlockType, ref int resultType, ref int resultStack)
        {
            // If the extractinator type isn't torch, we won't change anything
            if (extractType != ItemID.AshBlock)
                return;

            int randValue = Main.rand.Next(200); // Generates a random value between 0 and 49

            if (randValue < 2 && randValue > -1)
            {
                // 1 in 50 chance to get Citrine
                resultType = ModContent.ItemType<Citrine>();
                resultStack = 1;
            }
            else if (randValue < 4 && randValue > 1)
            {
                // 1 in 50 chance to get Hellstone Ore
                resultType = ItemID.Hellstone;
                resultStack = 1;
            }
            else if (randValue == 13)
            {
                // 1 in 50 chance to get Hellstone Ore
                resultType = ItemID.ObsidianRose;
                resultStack = 1;
            }
            else if (randValue < 7 && randValue > 3)
            {
                resultType = ItemID.Obsidian;
                resultStack = 1;
            }
            else if (randValue > 90)
            {
                // Default result if no special item is chosen
                resultType = ItemID.CopperCoin;
                resultStack = 1;
            }
            else if (randValue < 90 && randValue > 80)
            {
                // Default result if no special item is chosen
                resultType = ItemID.CopperCoin;
                resultStack = 20;
            }
            else if (randValue < 78 && randValue > 75)
            {
                // Default result if no special item is chosen
                resultType = ItemID.SilverCoin;
                resultStack = 20;
            }
            else if (randValue < 71 && randValue > 70)
            {
                // Default result if no special item is chosen
                resultType = ItemID.GoldCoin;
                resultStack = 2;
            }
            else
            {
                resultType = ItemID.CopperCoin;
                resultStack = 5;
            }
        }
    }

    public class ExtractinatorModSystem : ModSystem
    {
        internal static List<int> TorchItems;

        public override void PostSetupContent()
        {
            // Here we iterate through all items and find items that place tiles that are indicated as being torch tiles. We set these items to the extractinator mode of ItemID.Torch to indicate that they all share the torch extractinator result pool.
            ItemID.Sets.ExtractinatorMode[ItemID.AshBlock] = ItemID.AshBlock;
            TorchItems = new List<int>();

            for (int i = 0; i < ItemLoader.ItemCount; i++)
            {
                int createTile = ContentSamples.ItemsByType[i].createTile;
                if (createTile != -1 && i == ModContent.ItemType<Citrine>())
                {
                    TorchItems.Add(i);
                }
            }
        }
    }
}
