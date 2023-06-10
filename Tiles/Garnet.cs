using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.IO;
using Terraria.ModLoader;
using Terraria.WorldBuilding;

namespace InverseMod.Tiles
{
    public class Garnet : ModTile
    {
        static int[] gems = { ItemID.Amber, ItemID.Diamond, ItemID.Ruby, ItemID.Emerald, ItemID.Sapphire, ItemID.Topaz, ItemID.Amethyst, ModContent.ItemType<Items.Ores.Citrine>(), ModContent.ItemType<Items.Ores.Tourmaline>(), ModContent.ItemType<Items.Ores.Tanzanite>(), ModContent.ItemType<Items.Ores.Jade>(), ModContent.ItemType<Items.Ores.Garnet>(), ModContent.ItemType<Items.Ores.Copal>(), ModContent.ItemType<Items.Ores.Graphene>() };
        public override void SetStaticDefaults()
        {
            TileID.Sets.Ore[Type] = true;
            Main.tileSpelunker[Type] = true;
            Main.tileOreFinderPriority[Type] = 500;
            Main.tileShine2[Type] = true;
            Main.tileShine[Type] = 1200;
            Main.tileMergeDirt[Type] = true;
            Main.tileStone[Type] = true;
            Main.tileSolid[Type] = true;
            Main.tileBlockLight[Type] = true;

            ModTranslation name = CreateMapEntryName();
            name.SetDefault("Garnet");
            AddMapEntry(new Color(153, 0, 54), name);

            DustType = 117;
            ItemDrop = ModContent.ItemType<Items.Ores.Garnet>();
            HitSound = SoundID.Tink;
            MineResist = 3f;
            MinPick = 195;
        }
    }
}