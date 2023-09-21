using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;
using Terraria.GameContent.UI.BigProgressBar;
using InverseMod.NPCs.Bosses;
using Terraria.DataStructures;

namespace InverseMod.BossBars
{
    [AutoloadBossHead]
    // Showcases a custom boss bar with basic logic for displaying the icon, life, and shields properly.
    // Has no custom texture, meaning it will use the default vanilla boss bar texture
    public class PeasantSlimeBossBar : ModBossBar
    {
        private int bossHeadIndex = -1;

        public override Asset<Texture2D> GetIconTexture(ref Rectangle? iconFrame)
        {
            // Display the previously assigned head index
            if (bossHeadIndex != -1)
            {
            return TextureAssets.NpcHeadBoss[bossHeadIndex];
            }
            return base.GetIconTexture(ref iconFrame);
        }

        public override bool PreDraw(SpriteBatch spriteBatch, NPC npc, ref BossBarDrawParams drawParams)
        {
            bossHeadIndex = npc.GetBossHeadTextureIndex();
            // Make the bar shake the less health the NPC has
            float lifePercent = drawParams.Life / drawParams.LifeMax;
            float shakeIntensity = Utils.Clamp(1f - lifePercent - 0.2f, 0f, 1f);
            drawParams.BarCenter.Y -= 20f;
            drawParams.BarCenter += Main.rand.NextVector2Circular(0.5f, 0.5f) * shakeIntensity * 15f;

            drawParams.IconTexture = (Texture2D)TextureAssets.NpcHeadBoss[bossHeadIndex];

            return true;
        }
    }
}