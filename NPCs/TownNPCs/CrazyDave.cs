using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using Microsoft.Xna.Framework;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.GameContent.Personalities;
using System.Collections.Generic;
using InverseMod.Common.Systems;
using Terraria.Audio;

namespace InverseMod.NPCs.TownNPCs
{
    [AutoloadHead]
    public class CrazyDave : ModNPC
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Crazy Dave"); // Automatically assigned from localization files
            Main.npcFrameCount[NPC.type] = Main.npcFrameCount[NPCID.Merchant]; // Use the same frame count as Guide NPC
            NPCID.Sets.DangerDetectRange[NPC.type] = 700;
            NPCID.Sets.AttackType[NPC.type] = 0;
            NPCID.Sets.AttackTime[NPC.type] = 90;
            NPCID.Sets.AttackAverageChance[NPC.type] = 30;
            NPCID.Sets.HatOffsetY[NPC.type] = 4; // Adjust if your NPC's hat is not sitting right

            // Adjust NPC happiness
            NPC.Happiness
                .SetBiomeAffection<ForestBiome>(AffectionLevel.Like) // Crazy Dave likes Forest
                .SetBiomeAffection<JungleBiome>(AffectionLevel.Dislike) // Dislikes Jungle
                                                                        // Add more as needed
                .SetNPCAffection(NPCID.Dryad, AffectionLevel.Like); // Likes Dryad
                                                                    // Add more as needed
        }

        public override void SetDefaults()
        {
            NPC.townNPC = true; // This NPC is a town NPC.
            NPC.friendly = true; // The NPC is friendly to the player
            NPC.width = 20; // The width of the NPC's hitbox
            NPC.height = 46; // The height of the NPC's hitbox
            NPC.aiStyle = 7; // The AI style of the town NPCs
            NPC.damage = 10; // The damage of the NPC
            NPC.defense = 15; // The defense of the NPC
            NPC.lifeMax = 250; // The max life of the NPC
            NPC.HitSound = SoundID.NPCHit1; // The sound the NPC makes when hit.
            NPC.DeathSound = SoundID.NPCDeath1; // The sound the NPC makes when it dies.
            NPC.knockBackResist = 0.5f; // The knockback resistance of the NPC

            AnimationType = NPCID.Merchant; // Use Guide's animations
        }

        public override void FindFrame(int frameHeight)
        {
            // If you have a unique animation, you'll need to code that here.
        }

        public override bool CanTownNPCSpawn(int numTownNPCs)
        {
            // Conditions for Crazy Dave to spawn
            return true; // You can add your own conditions here
        }

        public override List<string> SetNPCNameList()
        {
            // Names for Crazy Dave
            return new List<string>() { "Crazy Dave", "Dave", "The Dave" };
        }

        public override string GetChat()
        {
            switch (Main.rand.Next(3))
            {
                case 0:
                    SoundEngine.PlaySound(rorAudio.Dave1);
                    break;
                case 1:
                    SoundEngine.PlaySound(rorAudio.Dave2);
                    break;
                case 2:
                    SoundEngine.PlaySound(rorAudio.Dave3);
                    break;
            }
            InversePlayer modPlayer = Main.LocalPlayer.GetModPlayer<InversePlayer>();

            if (!modPlayer.HasTalkedToCrazyDave)
            {
                modPlayer.HasTalkedToCrazyDave = true;
                if (Main.netMode == NetmodeID.SinglePlayer)
                {
                    Main.LocalPlayer.QuickSpawnItem(NPC.GetSource_GiftOrReward(), ItemID.Sunflower);
                }
                return "Greetings, neighbor! The name's Crazy Dave. But you can just call me Crazy Dave. Listen, I've got a surprise for you. Take this sunflower and go find a nice spot to plant it.";
            }
            else
            {
                // Subsequent dialogues after the first encounter
                switch (Main.rand.Next(3)) // Adjust number based on the remaining dialogue options
                {
                    case 0:
                        return "I've got a pan on my head!";
                    case 1:
                        return "Where did my magic taco go?!?";
                    default:
                        return "Plant some peas for me, will ya?";
                }
            }
        }



        public override void SetChatButtons(ref string button, ref string button2)
        {
            button = Language.GetTextValue("LegacyInterface.28"); // "Shop"
            // button2 can be set for a second option. Leaving it as null means there's only one option.
        }

        public override void OnChatButtonClicked(bool firstButton, ref string shop)
        {
            if (firstButton)
            {
                // Open shop
                shop = "Crazy Dave's TwiddyDinkies"; // Or any name for the shop
            }
        }

        public override void AddShops()
        {
            // Define what Crazy Dave sells in his shop
            var shop = new NPCShop(Type, "Crazy Dave's TwiddyDinkies")
                .Add(ItemID.Sunflower);

            shop.Register();
        }

        // You can add more functionality as needed, such as town NPC attack methods, movement, etc.
    }
}
