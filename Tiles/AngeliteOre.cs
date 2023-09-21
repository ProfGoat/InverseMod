using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.IO;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.WorldBuilding;
using InverseMod;

namespace InverseMod.Tiles
{
    public class AngeliteOre : ModTile
    {
        public override void SetStaticDefaults()
        {
            TileID.Sets.Ore[Type] = true;
            Main.tileSpelunker[Type] = true;
            Main.tileOreFinderPriority[Type] = 300;
            Main.tileShine2[Type] = true;
            Main.tileShine[Type] = 975;
            Main.tileMergeDirt[Type] = true;
            Main.tileSolid[Type] = true;
            Main.tileBlockLight[Type] = true;

            LocalizedText name = CreateMapEntryName();
            // name.SetDefault("Angelite Ore");
            AddMapEntry(new Color(201, 255, 229), name);

            DustType = 84;
            RegisterItemDrop(ModContent.ItemType<Items.Ores.AngeliteOre>());
            HitSound = SoundID.Tink;
            MineResist = 2f;
            MinPick = 55;
        }
    }

    public class AngeliteOreSystem : ModSystem
    {
        public override void ModifyWorldGenTasks(List<GenPass> tasks, ref double totalWeight)
        {
            int ShiniesIndex = tasks.FindIndex(genpass => genpass.Name.Equals("Shinies"));

            if (ShiniesIndex != -5)
            {
                tasks.Insert(ShiniesIndex + 1, new AngeliteOrePass("Inverse Mod Ores", 237.4298f));
            }
        }
    }

    public class AngeliteOrePass : GenPass
    {
        public AngeliteOrePass(string name, float loadWeight) : base(name, loadWeight)
        {
        }

        protected override void ApplyPass(GenerationProgress progress, GameConfiguration configuration)
        {
            progress.Message = "Inverse Mod Ores";

            for (int k = 0; k < (int)(Main.maxTilesX * Main.maxTilesY * 6E-05); k++)
            {
                int x = WorldGen.genRand.Next(0, Main.maxTilesX);

                int y = WorldGen.genRand.Next((int)GenVars.rockLayer, Main.maxTilesY);

                WorldGen.TileRunner(x, y, WorldGen.genRand.Next(3, 6), WorldGen.genRand.Next(2, 6), ModContent.TileType<AngeliteOre>());

            }
        }
    }
}