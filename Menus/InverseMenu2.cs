using InverseMod.Assets.Textures.Backgrounds;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.Graphics.Effects;
using Terraria.ID;
using Terraria.ModLoader;

namespace InverseMod.Menus;

[Autoload(Side = ModSide.Client)]
public sealed class InverseMenu2 : ModMenu
{
    private Asset<Texture2D> logoInverse;
    private Asset<Texture2D> Sun;
    private Asset<Texture2D> Moon;

    public override Asset<Texture2D> Logo => logoInverse;

    public override Asset<Texture2D> SunTexture => Sun;

    public override Asset<Texture2D> MoonTexture => Moon;
    public override int Music => MusicLoader.GetMusicSlot(Mod, "Assets/Music/MenuTheme2");

    public override string DisplayName => "Inverse Menu";

    public override void OnSelected()
    {
        System.Media.SoundPlayer player = new System.Media.SoundPlayer("C:\\Users\\Sawyer\\Documents\\My Games\\Terraria\\tModLoader\\ModSources\\InverseMod\\Assets\\Sounds\\Button.wav");
        player.Play();
    }
    public override void OnDeselected()
    {
        System.Media.SoundPlayer player = new System.Media.SoundPlayer("C:\\Users\\Sawyer\\Documents\\My Games\\Terraria\\tModLoader\\ModSources\\InverseMod\\Assets\\Sounds\\Nottub.wav");
        player.Play();
    }
    public override ModSurfaceBackgroundStyle MenuBackgroundStyle => ModContent.GetInstance<BackgroundStyle>();
    public override void Load()
    {
        logoInverse = Mod.Assets.Request<Texture2D>("Menus/Logo_Inverse");
        Sun = Mod.Assets.Request<Texture2D>("Menus/Sun");
        Moon = Mod.Assets.Request<Texture2D>("Menus/Moon");
        SkyManager.Instance["InverseMod/Assets/Textures/Backgrounds/RedSky"] = new RedSky();
    }

    public override bool PreDrawLogo(SpriteBatch sb, ref Vector2 logoDrawCenter, ref float logoRotation, ref float logoScale, ref Color drawColor)
    {
        if (logoInverse?.IsLoaded != true)
        {
            return false;
        }

        var textureSize = logoInverse.Value.Size();
        var textureCenter = textureSize * 0.5f;

        sb.Draw(logoInverse.Value, logoDrawCenter, null, drawColor, logoRotation, textureCenter, logoScale, SpriteEffects.None, 0f);

        return false;
    }
}