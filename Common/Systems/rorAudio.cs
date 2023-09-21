using InverseMod.Assets.Textures.Backgrounds;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.Graphics.Effects;
using Terraria.ID;
using Terraria.ModLoader;

namespace InverseMod.Common.Systems
{
    internal class rorAudio : ModSystem
    {
        public static readonly SoundStyle Button;
        public static readonly SoundStyle Nottub;
        static rorAudio()
        {
            Button = new SoundStyle("InverseMod/Assets/Sounds/Button", (SoundType)0);
            Nottub = new SoundStyle("InverseMod/Assets/Sounds/Nottub", (SoundType)0);
        }
    }
}
