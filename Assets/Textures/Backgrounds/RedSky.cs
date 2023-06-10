using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.Graphics.Effects;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace InverseMod.Assets.Textures.Backgrounds;
public class RedSky : CustomSky
{
    private readonly Random _random = new Random();
    private bool isActive = false;
    private int activeTime = 0;
    private const float maxIntensity = 0.3f;
    private float intensity = 0f;

    public override void OnLoad()
    {
    }

    public override void Update(GameTime gameTime)
    {
        if (isActive)  // Keep track of time for 5 seconds (300 frames)
        {
            activeTime++;
        }
    }

    private float GetIntensity()
    {
        if (activeTime < 60)
        {
            intensity += 0.01f;
            if (intensity > maxIntensity)
            {
                intensity = maxIntensity;
            }
        }
        if (activeTime > 590)
        {
            intensity -= 0.01f;
            if (intensity < 0f)
            {
                intensity = 0f;
            }
        }
        return intensity;
    }

    public override Color OnTileColor(Color inColor)
    {
        float intensity = GetIntensity() * 3;
        return Color.Lerp(inColor, Color.OrangeRed, intensity);
    }

    public override void Draw(SpriteBatch spriteBatch, float minDepth, float maxDepth)
    {
        if (maxDepth >= 0f && minDepth < 0f)
        {
            float intensity = GetIntensity();
            spriteBatch.Draw((Texture2D)Terraria.GameContent.TextureAssets.BlackTile, new Rectangle(0, 0, Main.screenWidth, Main.screenHeight), Color.DarkRed * intensity);
        }
    }

    public override float GetCloudAlpha()
    {
        return 1f;
    }

    public override void Activate(Vector2 position, params object[] args)
    {
        isActive = true;
        activeTime = 0;
    }

    public override void Deactivate(params object[] args)
    {
        isActive = false;
    }

    public override void Reset()
    {
        isActive = false;
    }

    public override bool IsActive()
    {
        return isActive;
    }
}