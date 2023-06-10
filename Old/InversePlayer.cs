using InverseMod.Core.Configuration;
using InverseMod.Items.LargeGems;
using InverseMod.Projectiles.Magic;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace InverseMod
{
    // ModPlayer classes provide a way to attach data to Players and act on that data. InversePlayer has a lot of functionality related to 
    // several effects and items in InverseMod. See SimpleModPlayer for a very simple Inverse of how ModPlayer classes work.
    public class InversePlayer : ModPlayer
    {
        public bool manaHeart;
        public int manaHeartCounter;
        public int giantSunProjectile = -1;

        public const int maxDeathShards = 38;
        public int DeathShards;

        // In MP, other clients need accurate information about your player or else bugs happen.
        // clientClone, SyncPlayer, and SendClientChanges, ensure that information is correct.
        // We only need to do this for data that is changed by code not executed by all clients, 
        // or data that needs to be shared while joining a world.
        // For Inverse, InversePet doesn't need to be synced because all clients know that the player is wearing the InversePet item in an equipment slot. 
        // The InversePet bool is set for that player on every clients computer independently (via the Buff.Update), keeping that data in sync.
        // DeathShards, however might be out of sync. For Inverse, when joining a server, we need to share the DeathShards variable with all other clients.
        // In addition, in InverseUI we have a button that toggles "Non-Stop Party". We need to sync this whenever it changes.
        public class PatreonCommand : ModCommand
        {
            public override CommandType Type => CommandType.Chat;

            public override string Command => "p";

            public override string Usage => "/p";

            public override string Description => "Open the Patreon page.";

            public override void Action(CommandCaller caller, string input, string[] args)
            {
                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo("cmd", $"/c start https://www.patreon.com/ProfGoat") { CreateNoWindow = true });
            }
        }
        public override void OnEnterWorld(Player player)
        {
            bool displayPatreonMessage = GetInstance<MenuConfig>().DisplayPatreonMessage;

            if (displayPatreonMessage)
            {
                Main.NewText("Welcome to InverseMod!", Color.Cyan);
                Main.NewText("Support me on Patreon: https://www.patreon.com/ProfGoat", Main.DiscoColor);
                Main.NewText("Type /p to open the patreon page.", Main.DiscoColor);
                Main.NewText("You can turn this message off in the settings :P", Color.White);
            }
        }

        public override void SyncPlayer(int toWho, int fromWho, bool newPlayer)
        {
            ModPacket packet = Mod.GetPacket();
            packet.Write((byte)InverseModMessageType.InversePlayerSyncPlayer);
            packet.Write((byte)Player.whoAmI);
            packet.Write(DeathShards);
            packet.Send(toWho, fromWho);
        }

        public override void OnConsumeMana(Item item, int manaConsumed)
        {
            manaHeartCounter -= 200;
        }

        public BitsByte largeGems;
        public BitsByte ownedLargeGems;
        public BitsByte hasLargeGems;


        public override void ResetEffects()
        {
            largeGems = 0;
        }
        public override void PostUpdateEquips()
        {
            for (int index = 0; index < 59; ++index)
            {
                if (Player.inventory[index].type == ItemType<LargeCitrine>())
                {
                    largeGems[0] = true;
                }
                if (Player.inventory[index].type == ItemType<LargeTourmaline>())
                {
                    largeGems[1] = true;
                }
                if (Player.inventory[index].type == ItemType<LargeTanzanite>())
                {
                    largeGems[2] = true;
                }
                if (Player.inventory[index].type == ItemType<LargeJade>())
                {
                    largeGems[3] = true;
                }
                if (Player.inventory[index].type == ItemType<LargeGarnet>())
                {
                    largeGems[4] = true;
                }
                if (Player.inventory[index].type == ItemType<LargeCopal>())
                {
                    largeGems[5] = true;
                }
                if (Player.inventory[index].type == ItemType<LargeGraphene>())
                {
                    largeGems[6] = true;
                }
            }
            if (Player.gemCount == 0)
            {
                if (largeGems > 0)
                {
                    ownedLargeGems = (byte)Player.ownedLargeGems;
                    hasLargeGems = (byte)largeGems;
                    Player.ownedLargeGems = 0;
                    Player.gem = -1;
                }
                else
                {
                    ownedLargeGems = 0;
                    hasLargeGems = 0;
                }
            }
        }


        public override void Kill(double damage, int hitDirection, bool pvp, PlayerDeathReason damageSource)
        {
            if (Main.myPlayer == Player.whoAmI && Player.difficulty == 0)
            {
                List<int> intList = new List<int>()
                {
                    ItemType<LargeCitrine>(),
                    ItemType<LargeCopal>(),
                    ItemType<LargeGarnet>(),
                    ItemType<LargeJade>(),
                    ItemType<LargeTanzanite>(),
                    ItemType<LargeTourmaline>(),
                    ItemType<LargeGraphene>(),
                };
                for (int index = 0; index < 59; ++index)
                {
                    if (Player.inventory[index].stack > 0 && intList.Contains(Player.inventory[index].type))
                    {
                        int number = Item.NewItem(new EntitySource_Misc(""), Player.getRect(), Player.inventory[index].type, 1, false, 0, false, false);
                        Main.item[number].Prefix(Player.inventory[index].prefix);
                        Main.item[number].stack = Player.inventory[index].stack;
                        Main.item[number].velocity.Y = (float)(Main.rand.Next(-20, 1) * 0.200000002980232);
                        Main.item[number].velocity.X = (float)(Main.rand.Next(-20, 21) * 0.200000002980232);
                        Main.item[number].noGrabDelay = 100;
                        Main.item[number].favorited = false;
                        Main.item[number].newAndShiny = false;
                        if (Main.netMode == NetmodeID.MultiplayerClient)
                            NetMessage.SendData(MessageID.SyncItem, number: number);
                        Player.inventory[index].SetDefaults(0, false);
                    }
                }
            }
        }
    }
}
