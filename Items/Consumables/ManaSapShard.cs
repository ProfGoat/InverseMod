using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using Microsoft.Xna.Framework;
using System;
using Terraria.ModLoader.IO;

namespace InverseMod.Items.Consumables
{
    // The overlay used to display the custom mana crystals can be found in Common/UI/ResourceDisplay/VanillaManaOverlay.cs
    internal class ManaSapShard : ModItem
    {
        public const int MaxManaSapShards = 18;
        public const int ManaPerShard = -10;

        public override void SetStaticDefaults()
        {
            // Tooltip.SetDefault($"Permanently decreases maximum mana by 10, down to 20.\nCan only be used once you have consumed max number of mana crystals.\n[c/E50D0D:Warning: Once used you cannot regain the mana you have lost!]");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = -10;
        }

        public override void SetDefaults()
        {
            Item.maxStack = 99;
        }

        public override bool CanUseItem(Player player)
        {
            return player.statManaMax == 200 && player.GetModPlayer<ManaSapShardPlayer>().ManaSapShards < MaxManaSapShards;
        }

        public override Nullable<bool> UseItem(Player player)/* tModPorter Suggestion: Return null instead of false */
        {
            // Do not do this: player.statLifeMax += 2;
            player.statManaMax2 += ManaPerShard;
            player.statMana += ManaPerShard;
            if (Main.myPlayer == player.whoAmI)
            {
                player.ManaEffect(ManaPerShard);
            }

            // This is very important. This is what makes it permanent.
            player.GetModPlayer<ManaSapShardPlayer>().ManaSapShards++;
            // This handles the 2 achievements related to using any life increasing item or getting to exactly 500 hp and 200 mp.
            // Ignored since our item is only useable after this achievement is reached
            // AchievementsHelper.HandleSpecialEvent(player, 2);
            //TODO re-add this when ModAchievement is merged?
            return true;
        }

        // Please see Content/ExampleRecipes.cs for a detailed explanation of recipe creation.
        public override void AddRecipes()
        {
            Recipe r = CreateRecipe();
            r.AddIngredient(ItemID.ManaCrystal);
            r.AddTile(TileID.DemonAltar);
            r.Register();
        }
    }

    public class ManaSapShardPlayer : ModPlayer
    {
        public int ManaSapShards;

        public override void ResetEffects()
        {
            // Increasing health in the ResetEffects hook in particular is important so it shows up properly in the player select menu
            // and so that life regeneration properly scales with the bonus health
            Player.statManaMax2 += ManaSapShards * ManaSapShard.ManaPerShard;
        }

        public override void SyncPlayer(int toWho, int fromWho, bool newPlayer)
        {
            ModPacket packet = Mod.GetPacket();
            packet.Write((byte)Player.whoAmI);
            packet.Write(ManaSapShards);
            packet.Send(toWho, fromWho);
        }

        // NOTE: The tag instance provided here is always empty by default.
        // Read https://github.com/tModLoader/tModLoader/wiki/Saving-and-loading-using-TagCompound to better understand Saving and Loading data.
        public override void SaveData(TagCompound tag)
        {
            tag["ManaSapShards"] = ManaSapShards;
        }

        public override void LoadData(TagCompound tag)
        {
            ManaSapShards = (int)tag["ManaSapShards"];
        }
    }
}
