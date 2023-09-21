using Terraria;
using Terraria.ModLoader;
using Terraria.IO;
using Terraria.WorldBuilding;
using Terraria.ID;

namespace InverseMod.Common.Systems
{
    internal class GemPass : GenPass
    {
        public GemPass(string name, float weight) : base(name, weight) { }

        protected override void ApplyPass(GenerationProgress progress, GameConfiguration configuration)
        {
            progress.Message = "Spawning Inverse Gems";

            // TutorialOre
            int maxToSpawn = (int)(Main.maxTilesX * Main.maxTilesY * 6E-05);
            for (int k = 0; k < (int)(Main.maxTilesX * Main.maxTilesY * 6E-05); k++)
            {
                int x = WorldGen.genRand.Next(0, Main.maxTilesX);

                int y = WorldGen.genRand.Next((int)GenVars.rockLayer, Main.maxTilesY);

                WorldGen.TileRunner(x, y, WorldGen.genRand.Next(3, 6), WorldGen.genRand.Next(2, 6), ModContent.TileType<Tiles.Citrine>());

            }

            for (int k = 0; k < (int)(Main.maxTilesX * Main.maxTilesY * 6E-05); k++)
            {
                int x = WorldGen.genRand.Next(0, Main.maxTilesX);

                int y = WorldGen.genRand.Next((int)GenVars.rockLayer, Main.maxTilesY);

                WorldGen.TileRunner(x, y, WorldGen.genRand.Next(3, 6), WorldGen.genRand.Next(2, 6), ModContent.TileType<Tiles.Tourmaline>());
            }

            for (int k = 0; k < (int)(Main.maxTilesX * Main.maxTilesY * 5E-05); k++)
            {
                int x = WorldGen.genRand.Next(0, Main.maxTilesX);

                int y = WorldGen.genRand.Next((int)GenVars.rockLayer, Main.maxTilesY);

                WorldGen.TileRunner(x, y, WorldGen.genRand.Next(3, 6), WorldGen.genRand.Next(2, 6), ModContent.TileType<Tiles.Tanzanite>());

            }

            for (int k = 0; k < (int)(Main.maxTilesX * Main.maxTilesY * 4E-05); k++)
            {
                int x = WorldGen.genRand.Next(0, Main.maxTilesX);

                int y = WorldGen.genRand.Next((int)GenVars.rockLayer, Main.maxTilesY);

                WorldGen.TileRunner(x, y, WorldGen.genRand.Next(3, 6), WorldGen.genRand.Next(2, 6), ModContent.TileType<Tiles.Jade>());

            }

            for (int k = 0; k < (int)(Main.maxTilesX * Main.maxTilesY * 4E-05); k++)
            {
                int x = WorldGen.genRand.Next(0, Main.maxTilesX);

                int y = WorldGen.genRand.Next((int)GenVars.rockLayer, Main.maxTilesY);

                WorldGen.TileRunner(x, y, WorldGen.genRand.Next(3, 6), WorldGen.genRand.Next(2, 6), ModContent.TileType<Tiles.Garnet>());

            }

            for (int k = 0; k < (int)(Main.maxTilesX * Main.maxTilesY * 3E-05); k++)
            {
                int x = WorldGen.genRand.Next(0, Main.maxTilesX);

                int y = WorldGen.genRand.Next((int)GenVars.rockLayer, Main.maxTilesY);

                WorldGen.TileRunner(x, y, WorldGen.genRand.Next(3, 6), WorldGen.genRand.Next(2, 6), ModContent.TileType<Tiles.Copal>());

            }

            for (int k = 0; k < (int)(Main.maxTilesX * Main.maxTilesY * 2E-05); k++)
            {
                int x = WorldGen.genRand.Next(0, Main.maxTilesX);

                int y = WorldGen.genRand.Next((int)GenVars.rockLayer, Main.maxTilesY);

                WorldGen.TileRunner(x, y, WorldGen.genRand.Next(3, 6), WorldGen.genRand.Next(2, 6), ModContent.TileType<Tiles.Graphene>());

            }
        }
    }
}