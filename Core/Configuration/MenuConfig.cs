using System.ComponentModel;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;

namespace InverseMod.Core.Configuration
{
    public class MenuConfig : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ClientSide;

        [Label("Display Patreon Message on World Entry")]
        [DefaultValue(true)]
        public bool DisplayPatreonMessage { get; set; }

        [Label("Use Alternate Soundtrack")]
        [DefaultValue(false)]
        public bool UseAlternateSoundtrack { get; set; }
    }
}