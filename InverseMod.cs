using System;
using System.IO;
using InverseMod.Core.Configuration;
using InverseMod.DamageClasses;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.UI;
using ReLogic.Content.Sources;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;

namespace InverseMod;
public class InverseMod : Mod
    {
    private static InverseMod instance;
    public static InverseMod Instance => instance ?? throw new InvalidOperationException("An instance of the mod has not yet been created.");

    public static readonly string PersonalDirectory = Path.Combine(Main.SavePath, "InverseMod");
    public InverseMod()
    {
        instance = this;

        Directory.CreateDirectory(PersonalDirectory);
    }
    public override void HandlePacket(BinaryReader reader, int whoAmI)
    {

        InverseModMessageType msgType = (InverseModMessageType)reader.ReadByte();
        switch (msgType)
        {
            // This message syncs InversePlayer.InverseLifeFruits
            case InverseModMessageType.InversePlayerSyncPlayer:
                byte playernumber = reader.ReadByte();
                InversePlayer inversePlayer = Main.player[playernumber].GetModPlayer<InversePlayer>();
                int DeathShards = reader.ReadInt32();
                inversePlayer.DeathShards = DeathShards;
                // SyncPlayer will be called automatically, so there is no need to forward this data to other clients.
                break;
            case InverseModMessageType.InverseTeleportToStatue:
                if (Main.npc[reader.ReadByte()].ModNPC is NPCs.TownNPCs.Troll person)
                {
                    person.StatueTeleport();
                }
                break;
            default:
                Logger.WarnFormat("InverseMod: Unknown Message type: {0}", msgType);
                break;
        }
    }
}

internal enum InverseModMessageType : byte
{
        InversePlayerSyncPlayer,
        InverseTeleportToStatue
}