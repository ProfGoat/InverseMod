using System.Collections;
using System.Collections.Generic;
using System.IO;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace InverseMod.Common.Systems
{
    public class DownedBossSystem : ModSystem
    {
        public static bool downedPeasantSlime = false;

        public override void OnWorldLoad()
        {
            downedPeasantSlime = false;
        }

        public override void OnWorldUnload()
        {
            downedPeasantSlime = false;
        }

        public override void SaveWorldData(TagCompound tag)
        {
            if (downedPeasantSlime)
            {
                tag["downedPeasantSlime"] = true;
            }
        }

        public override void LoadWorldData(TagCompound tag)
        {
            downedPeasantSlime = tag.ContainsKey("downedPeasantSlime");
        }

        public override void NetSend(BinaryWriter writer)
        {
            var flags = new BitsByte();
            flags[0] = downedPeasantSlime;
            writer.Write(flags);
        }

        public override void NetReceive(BinaryReader reader)
        {
            BitsByte flags = reader.ReadByte();
            downedPeasantSlime = flags[0];
        }
    }
}