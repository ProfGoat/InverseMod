using InverseMod.Mounts;
using System;
using Terraria;
using Terraria.ModLoader;

namespace InverseMod.Buffs
{
    public class CitrineMinecartBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Citrine Minecart"); //name tbd?
            Description.SetDefault("Sparkles.");
            Main.buffNoTimeDisplay[Type] = true;
            Main.buffNoSave[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.mount.SetMount(ModContent.MountType<CitrineMinecart>(), player);
            player.buffTime[buffIndex] = 10;

            //if (Math.Abs(player.velocity.X) > 3)
            //	player.armorEffectDrawShadow = true;
        }
    }
}