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
        public static readonly SoundStyle Slash;
        public static readonly SoundStyle Whoosh;
        public static readonly SoundStyle Hit;
        public static readonly SoundStyle Dave1;
        public static readonly SoundStyle Dave2;
        public static readonly SoundStyle Dave3;
        static rorAudio()
        {
            Button = new SoundStyle("InverseMod/Assets/Sounds/Button", (SoundType)0);
            Nottub = new SoundStyle("InverseMod/Assets/Sounds/Nottub", (SoundType)0);
            Slash = new SoundStyle("InverseMod/Assets/Sounds/Slash", (SoundType)0);
            Whoosh = new SoundStyle("InverseMod/Assets/Sounds/Whoosh", (SoundType)0);
            Hit = new SoundStyle("InverseMod/Assets/Sounds/Hit", (SoundType)0);
            Dave1 = new SoundStyle("InverseMod/Assets/Sounds/Dave1", (SoundType)0);
            Dave2 = new SoundStyle("InverseMod/Assets/Sounds/Dave2", (SoundType)0);
            Dave3 = new SoundStyle("InverseMod/Assets/Sounds/Dave3", (SoundType)0);
        }
    }
}
