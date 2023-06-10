using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace InverseMod.Dusts
{
    public class CitrineCatalystDust : ModDust
    {
        public override void OnSpawn(Dust dust)
        {
            dust.velocity *= 0.1f;
            dust.noGravity = true;
            dust.noLight = false;
            dust.scale = 1.5f;
        }

        public override bool Update(Dust dust)
        {
            dust.position += dust.velocity;
            dust.scale -= 0.0333333f; // 1.5f scale / 45 ticks (1.5 seconds) = 0.0333333f
            if (dust.scale <= 0)
            {
                dust.active = false;
            }
            return false;
        }
    }
}