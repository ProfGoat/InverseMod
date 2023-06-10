using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using InverseMod;
using Microsoft.Xna.Framework;

namespace InverseMod.Dusts
{
    public class LOLDust : ModDust
    {
        public override void OnSpawn(Dust dust)
        {
            dust.noGravity = true;
            dust.noLight = false;
            dust.scale = 1.5f;

        }

        public override bool Update(Dust dust)
        {
            dust.position = dust.velocity;
            dust.rotation = dust.velocity.X;
            dust.scale = 0.05f;

            float light = 0.6f * dust.scale;

            Lighting.AddLight(dust.position, 1f, 1f, 1f);

            if (dust.scale < 0.5f)
            {
                dust.active = false;
            }

            return true;
        }
        public override Color? GetAlpha(Dust dust, Color lightColor)
        {
            return new Color(lightColor.R, lightColor.G, lightColor.B, 25);
        }
    }

}