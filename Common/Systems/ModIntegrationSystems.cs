using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Terraria.ModLoader;

namespace InverseMod.Common.Systems
{
    public class ModIntegrationsSystem : ModSystem
    {
        public override void PostSetupContent()
        {
            DoCensusIntegration();
            DoBossChecklistIntegration();
        }

        private void DoCensusIntegration()
        {
            if (!ModLoader.TryGetMod("Census", out Mod censusMod))
            {
                return;
            }

            int npcType = ModContent.NPCType<NPCs.TownNPCs.Troll>();

            string message = $"Have a Peasant Slime Trophy [i:{ModContent.ItemType<Items.Furniture.PeasantSlimeTrophy>()}] in your inventory";

            censusMod.Call("TownNPCCondition", npcType, message);
        }

        private void DoBossChecklistIntegration()
        {
            if (!ModLoader.TryGetMod("BossChecklist", out Mod bossChecklistMod))
            {
                return;
            }

            if (bossChecklistMod.Version < new Version(1, 3, 1))
            {
                return;
            }

            string bossName = "Peasant Slime";

            int bossType = ModContent.NPCType<NPCs.Bosses.PeasantSlimeBody>();

            float weight = 0.7f;

            Func<bool> downed = () => DownedBossSystem.downedPeasantSlime;

            Func<bool> available = () => true;

            List<int> collection = new List<int>()
            {
                ModContent.ItemType<Items.Furniture.PeasantSlimeRelic>(),
                ModContent.ItemType<Items.Furniture.PeasantSlimeTrophy>()
            };

            int summonItem = ModContent.ItemType<Items.Consumables.PeasantSlimeSummonItem>();

            string spawnInfo = $"Use a [i:{summonItem}]";

            string despawnInfo = null;

            var customBossPortrait = (SpriteBatch sb, Rectangle rect, Color color) => {
                Texture2D texture = ModContent.Request<Texture2D>("InverseMod/Assets/Textures/Bestiary/PeasantSlime_Preview").Value;
                Vector2 centered = new Vector2(rect.X + (rect.Width / 2) - (texture.Width / 2), rect.Y + (rect.Height / 2) - (texture.Height / 2));
                sb.Draw(texture, centered, color);
            };

            bossChecklistMod.Call(
                "AddBoss",
                Mod,
                bossName,
                bossType,
                weight,
                downed,
                available,
                collection,
                summonItem,
                spawnInfo,
                despawnInfo,
                customBossPortrait
            );
        }
    }
}