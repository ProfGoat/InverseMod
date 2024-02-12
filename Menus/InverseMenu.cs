using InverseMod.Assets.Textures.Backgrounds;
using InverseMod.Common.Systems;
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
public sealed class InverseMenu : ModMenu
{
    private Asset<Texture2D> logoInverse1;
    private Asset<Texture2D> Sun;
    private Asset<Texture2D> Moon;

    public override Asset<Texture2D> Logo => logoInverse1;

    public override Asset<Texture2D> SunTexture => Sun;

    public override Asset<Texture2D> MoonTexture => Moon;
    public override int Music => MusicLoader.GetMusicSlot(Mod, "Assets/Music/MenuTheme");

    public override string DisplayName => "Inverse Menu";

    public override void OnSelected()
    {
        SoundEngine.PlaySound(rorAudio.Button);
    }
    public override void OnDeselected()
    {
        SoundEngine.PlaySound(rorAudio.Nottub);
    }
    public override ModSurfaceBackgroundStyle MenuBackgroundStyle => ModContent.GetInstance<BackgroundStyle>();
    public override void Load()
    {
        logoInverse1 = Mod.Assets.Request<Texture2D>("Menus/Logo_Inverse1");
        Sun = Mod.Assets.Request<Texture2D>("Menus/Sun");
        Moon = Mod.Assets.Request<Texture2D>("Menus/Moon");
        SkyManager.Instance["InverseMod/Assets/Textures/Backgrounds/RedSky"] = new RedSky();
    }

    public override bool PreDrawLogo(SpriteBatch sb, ref Vector2 logoDrawCenter, ref float logoRotation, ref float logoScale, ref Color drawColor)
    {
        if (logoInverse1?.IsLoaded != true)
        {
            return false;
        }

        var textureSize = logoInverse1.Value.Size();
        var textureCenter = textureSize * 0.5f;

        sb.Draw(logoInverse1.Value, logoDrawCenter, null, drawColor, logoRotation, textureCenter, logoScale, SpriteEffects.None, 0f);

        return false;
    }
}