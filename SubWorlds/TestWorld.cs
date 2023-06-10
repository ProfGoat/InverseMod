using Terraria;
using SubworldLibrary;
using Terraria.WorldBuilding;
using System.Collections.Generic;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent;
using Terraria.IO;
using Terraria.ID;
using System;

namespace InverseMod.SubWorlds
{
    internal class TestWorld : Subworld
    {
        public override string Name => "TestWorld";

        public override int Width => 1750;
        public override int Height => 750;
        public override bool ShouldSave => false;
        public override bool NoPlayerSaving => false;
        public override bool NormalUpdates => false;

        //public override bool noWorldUpdate => true;
        private const string assetPath = "InverseMod/Assets/Textures/Backgrounds";
        public override void DrawMenu(GameTime gameTime)
        {
            Texture2D MenuBG = (Texture2D)ModContent.Request<Texture2D>($"{assetPath}/LoadingScreen");//Background
            Vector2 zero = Vector2.Zero;
            float width = (float)Main.screenWidth / (float)MenuBG.Width;
            float height = (float)Main.screenHeight / (float)MenuBG.Height;

            if (width != height)
            {
                if (height > width)
                {
                    width = height;
                    zero.X -= ((float)MenuBG.Width * width - (float)Main.screenWidth) * 0.5f;
                }
                else
                {
                    zero.Y -= ((float)MenuBG.Height * width - (float)Main.screenHeight) * 0.5f;
                }
            }

            Main.spriteBatch.Draw(MenuBG, zero, (Rectangle?)null, Color.White, 0f, Vector2.Zero, width, (SpriteEffects)0, 0f);

            base.DrawMenu(gameTime);
        }

        public override List<GenPass> Tasks => new List<GenPass>()
        {
            new TestGenPass(delegate
                {
                Main.worldSurface = 600.0;
                Main.rockLayer = Main.maxTilesY;
                SubworldSystem.hideUnderworld = true;
                 StructureHelper.Generator.GenerateStructure("Structures/Place", new Terraria.DataStructures.Point16((Main.maxTilesX/2) - 8, (Main.maxTilesY/2) + 5), InverseMod.Instance);
                })
        };
    }
    public class TestGenPass : GenPass
    {
        private Action<GenerationProgress> method;

        public TestGenPass(Action<GenerationProgress> method)
            : base("", 1f)
        {
            this.method = method;
        }

        public TestGenPass(float weight, Action<GenerationProgress> method)
            : base("", weight)
        {
            this.method = method;
        }

        protected override void ApplyPass(GenerationProgress progress, GameConfiguration configuration)
        {
            method(progress);
        }
    }
}