using InverseMod;
using InverseMod.Dusts;
using InverseMod.Items;
using InverseMod.Tiles;
using InverseMod.Tiles.Furniture;
using Microsoft.Xna.Framework;
using System;
using System.Linq;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.Utilities;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent;
using Terraria.GameContent.Personalities;
using Terraria.DataStructures;
using System.Collections.Generic;
using ReLogic.Content;
using Terraria.ModLoader.IO;
using InverseMod.Items.Consumables;
using InverseMod.Items.Ammo;

namespace InverseMod.NPCs.TownNPCs
{
    [AutoloadHead]
    public class Troll : ModNPC
    {
        public int NumberOfTimesTalkedTo = 0;
        public const string ShopName = "Store";
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[Type] = 25;

            NPCID.Sets.ExtraFramesCount[Type] = 9;
            NPCID.Sets.AttackFrameCount[Type] = 4;
            NPCID.Sets.DangerDetectRange[Type] = 700;
            NPCID.Sets.AttackType[Type] = 0;
            NPCID.Sets.AttackTime[Type] = 90;
            NPCID.Sets.AttackAverageChance[Type] = 30;
            NPCID.Sets.HatOffsetY[Type] = 4;

            NPCID.Sets.NPCBestiaryDrawModifiers drawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
            {
                Velocity = 1f,
                Direction = 1
            };

            NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, drawModifiers);

            NPC.Happiness
                .SetBiomeAffection<ForestBiome>(AffectionLevel.Hate)
                .SetBiomeAffection<SnowBiome>(AffectionLevel.Hate)
                .SetBiomeAffection<JungleBiome>(AffectionLevel.Hate)
                .SetBiomeAffection<HallowBiome>(AffectionLevel.Hate)
                .SetBiomeAffection<DesertBiome>(AffectionLevel.Hate)
                .SetBiomeAffection<OceanBiome>(AffectionLevel.Hate)
                .SetBiomeAffection<MushroomBiome>(AffectionLevel.Hate)
                .SetBiomeAffection<UndergroundBiome>(AffectionLevel.Hate)
                .SetBiomeAffection<CorruptionBiome>(AffectionLevel.Love)
                .SetBiomeAffection<CrimsonBiome>(AffectionLevel.Love)
                .SetNPCAffection(NPCID.Dryad, AffectionLevel.Hate)
                .SetNPCAffection(NPCID.Guide, AffectionLevel.Hate)
                .SetNPCAffection(NPCID.Merchant, AffectionLevel.Hate)
                .SetNPCAffection(NPCID.Demolitionist, AffectionLevel.Hate)
                .SetNPCAffection(NPCID.TaxCollector, AffectionLevel.Love)
                .SetNPCAffection(NPCID.Nurse, AffectionLevel.Hate)
                .SetNPCAffection(NPCID.Painter, AffectionLevel.Hate)
                .SetNPCAffection(NPCID.DyeTrader, AffectionLevel.Hate)
                .SetNPCAffection(NPCID.Angler, AffectionLevel.Like)
                .SetNPCAffection(NPCID.PartyGirl, AffectionLevel.Hate)
                .SetNPCAffection(NPCID.Golfer, AffectionLevel.Hate)
                .SetNPCAffection(NPCID.Stylist, AffectionLevel.Hate)
                .SetNPCAffection(NPCID.ArmsDealer, AffectionLevel.Hate)
                .SetNPCAffection(NPCID.GoblinTinkerer, AffectionLevel.Dislike)
                .SetNPCAffection(NPCID.WitchDoctor, AffectionLevel.Hate)
                .SetNPCAffection(NPCID.Clothier, AffectionLevel.Hate)
                .SetNPCAffection(NPCID.Mechanic, AffectionLevel.Hate)
                .SetNPCAffection(NPCID.Pirate, AffectionLevel.Hate)
                .SetNPCAffection(NPCID.Truffle, AffectionLevel.Hate)
                .SetNPCAffection(NPCID.Wizard, AffectionLevel.Hate)
                .SetNPCAffection(NPCID.Princess, AffectionLevel.Hate)
                .SetNPCAffection(NPCID.Steampunker, AffectionLevel.Hate)
                .SetNPCAffection(NPCID.Cyborg, AffectionLevel.Hate)
                .SetNPCAffection(NPCID.SantaClaus, AffectionLevel.Hate)
                .SetNPCAffection(NPCID.TownBunny, AffectionLevel.Hate)
                .SetNPCAffection(NPCID.TownDog, AffectionLevel.Hate)
                .SetNPCAffection(NPCID.TownCat, AffectionLevel.Hate)
            ;
        }

        public override void SetDefaults()
        {
            NPC.townNPC = true;
            NPC.friendly = true;
            NPC.width = 18;
            NPC.height = 40;
            NPC.aiStyle = 7;
            NPC.damage = 10;
            NPC.defense = 50;
            NPC.lifeMax = 1000;
            NPC.HitSound = SoundID.NPCHit21;
            NPC.DeathSound = SoundID.NPCDeath59;
            NPC.knockBackResist = 0.5f;

            AnimationType = NPCID.Guide;
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheUnderworld,

				new FlavorTextBestiaryInfoElement("You done F****d up bringing him here."),
            });
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            if (NPCID.Sets.NPCBestiaryDrawOffset.TryGetValue(Type, out NPCID.Sets.NPCBestiaryDrawModifiers drawModifiers))
            {
                drawModifiers.Rotation += 0.001f;

                NPCID.Sets.NPCBestiaryDrawOffset.Remove(Type);
                NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, drawModifiers);
            }

            return true;
        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            int num = NPC.life > 0 ? 1 : 5;

            for (int k = 0; k < num; k++)
            {
                Dust.NewDust(NPC.position, NPC.width, NPC.height, ModContent.DustType<LOLDust>());
            }
        }

        public override bool CanTownNPCSpawn(int numTownNPCs)/* tModPorter Suggestion: Copy the implementation of NPC.SpawnAllowed_Merchant in vanilla if you to count money, and be sure to set a flag when unlocked, so you don't count every tick. */
        {
            for (int k = 0; k < 255; k++)
            {
                Player player = Main.player[k];
                if (!player.active)
                {
                    continue;
                }

                if (player.inventory.Any(item => item.type == ModContent.ItemType<Items.Furniture.PeasantSlimeTrophy>() || item.type == ModContent.ItemType<Items.Furniture.PeasantSlimeRelic>()))
                {
                    return true;
                }
            }

            return false;
        }

        public override ITownNPCProfile TownNPCProfile()
        {
            return new TrollProfile();
        }

        public override List<string> SetNPCNameList()/* tModPorter Suggestion: Return a list of names */
        {
            return new List<string>() {
                "Gimble Meister",
                "Wobble Goomer",
                "Pondle Snoog",
                "Geeble Weebler"
            };
        }

        public override void FindFrame(int frameHeight)
        {
        }

        public override string GetChat()
        {
            WeightedRandom<string> chat = new WeightedRandom<string>();

            int partyGirl = NPC.FindFirstNPC(NPCID.PartyGirl);
            if (partyGirl >= 0 && Main.rand.NextBool(4))
            {
                chat.Add(Language.GetTextValue("Mods.InverseMod.Dialogue.Troll.PartyGirlDialogue", Main.npc[partyGirl].GivenName));
            }

            chat.Add(Language.GetTextValue("Trolllolololollololololoolllolol!"));
            chat.Add(Language.GetTextValue("https://www.youtube.com/watch?v=xvFZjo5PgG0"));
            chat.Add(Language.GetTextValue("https://www.youtube.com/watch?v=gkTb9GP9lVI"));
            chat.Add(Language.GetTextValue("Sawcon deez nuts."), 5.0);
            chat.Add(Language.GetTextValue("Kill me."), 0.1);

            NumberOfTimesTalkedTo++;
            if (NumberOfTimesTalkedTo >= 10)
            {
                chat.Add(Language.GetTextValue("Please..."));
            }

            return chat;
        }

        public override void SetChatButtons(ref string button, ref string button2)
        {
            button = Language.GetTextValue("LegacyInterface.28");
        }

        public override void OnChatButtonClicked(bool firstButton, ref string shop)
        {
            if (firstButton)
            {
                shop = ShopName;
            }
        }
        public override void AddShops()
        {
            var npcShop = new NPCShop(Type, ShopName)
                .Add<MusketBall>();

            npcShop.Register(); // Name of this shop tab
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Items.Ammo.MusketBall>(), 1, 150, 300));
        }
        public override bool CanGoToStatue(bool toKingStatue) => true;

        public override void OnGoToStatue(bool toKingStatue)
        {
            if (Main.netMode == NetmodeID.Server)
            {
                ModPacket packet = Mod.GetPacket();
                packet.Write("OOGA BOOGA!");
                packet.Write((byte)NPC.whoAmI);
                packet.Send();
            }
            else
            {
                StatueTeleport();
            }
        }

        public void StatueTeleport()
        {
            for (int i = 0; i < 30; i++)
            {
                Vector2 position = Main.rand.NextVector2Square(-20, 21);
                if (Math.Abs(position.X) > Math.Abs(position.Y))
                {
                    position.X = Math.Sign(position.X) * 20;
                }
                else
                {
                    position.Y = Math.Sign(position.Y) * 20;
                }

                Dust.NewDustPerfect(NPC.Center + position, ModContent.DustType<Dusts.LOLDust>(), Vector2.Zero).noGravity = true;
            }
        }

        public override void TownNPCAttackStrength(ref int damage, ref float knockback)
        {
            damage = 20;
            knockback = 4f;
        }

        public override void TownNPCAttackCooldown(ref int cooldown, ref int randExtraCooldown)
        {
            cooldown = 3;
            randExtraCooldown = 3;
        }

        public override void TownNPCAttackProj(ref int projType, ref int attackDelay)
        {
            projType = ProjectileID.BouncyDynamite;
            attackDelay = 1;
        }

        public override void TownNPCAttackProjSpeed(ref float multiplier, ref float gravityCorrection, ref float randomOffset)
        {
            multiplier = 20f;
            randomOffset = 5f;
        }

        public override void LoadData(TagCompound tag)
        {
            NumberOfTimesTalkedTo = tag.GetInt("numberOfTimesTalkedTo");
        }

        public override void SaveData(TagCompound tag)
        {
            tag["numberOfTimesTalkedTo"] = NumberOfTimesTalkedTo;
        }
    }

    public class TrollProfile : ITownNPCProfile
    {
        public int RollVariation() => 0;
        public string GetNameForVariant(NPC npc) => npc.getNewNPCName();

        public Asset<Texture2D> GetTextureNPCShouldUse(NPC npc)
        {
            if (npc.IsABestiaryIconDummy && !npc.ForcePartyHatOn)
                return ModContent.Request<Texture2D>("InverseMod/NPCs/TownNPCs/Troll");

            if (npc.altTexture == 1)
                return ModContent.Request<Texture2D>("InverseMod/NPCs/TownNPCs/Troll_Party");

            return ModContent.Request<Texture2D>("InverseMod/NPCs/TownNPCs/Troll");
        }

        public int GetHeadTextureIndex(NPC npc) => ModContent.GetModHeadSlot("InverseMod/NPCs/TownNPCs/Troll_Head");
    }
}