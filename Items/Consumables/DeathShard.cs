using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using Microsoft.Xna.Framework;
using System;
using Terraria.ModLoader.IO;

namespace InverseMod.Items.Consumables
{
    // The overlay used to display the custom life fruit can be found in Common/UI/ResourceDisplay/VanillaLifeOverlay.cs
    internal class DeathShard : ModItem
    {
        public const int MaxDeathShards = 38;
        public const int LifePerShard = -10;

        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault($"Permanently decreases maximum health by 10, down to 20.\nCan only be used once you have consumed max number of life crystals.\n[c/E50D0D: WARNING: ONCE USED, YOU CANNOT REGAIN THE HEALTH YOU HAVE LOST!]");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 38;
        }

        public override void SetDefaults()
        {
            Item.maxStack = 99;
        }

        public override bool CanUseItem(Player player)
        {
            // Any mod that changes statLifeMax to be greater than 500 is broken and needs to fix their code.
            // This check also prevents this item from being used before vanilla health upgrades are maxed out.
            return player.statLifeMax == 400 && player.GetModPlayer<DeathShardPlayer>().DeathShards < MaxDeathShards;
        }

        public override Nullable<bool> UseItem(Player player)/* tModPorter Suggestion: Return null instead of false */
        {
            // Do not do this: player.statLifeMax += 2;
            player.statLifeMax2 += LifePerShard;
            player.statLife += LifePerShard;
            if (Main.myPlayer == player.whoAmI)
            {
                player.HealEffect(LifePerShard);
            }

            // This is very important. This is what makes it permanent.
            player.GetModPlayer<DeathShardPlayer>().DeathShards++;
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
            r.AddIngredient(ItemID.LifeCrystal);
            r.AddTile(TileID.DemonAltar);
            r.Register();
        }
    }

    public class DeathShardPlayer : ModPlayer
    {
        public int DeathShards;

        public override void ResetEffects()
        {
            // Increasing health in the ResetEffects hook in particular is important so it shows up properly in the player select menu
            // and so that life regeneration properly scales with the bonus health
            Player.statLifeMax2 += DeathShards * DeathShard.LifePerShard;
        }

        public override void SyncPlayer(int toWho, int fromWho, bool newPlayer)
        {
            ModPacket packet = Mod.GetPacket();
            packet.Write((byte)Player.whoAmI);
            packet.Write(DeathShards);
            packet.Send(toWho, fromWho);
        }

        // NOTE: The tag instance provided here is always empty by default.
        // Read https://github.com/tModLoader/tModLoader/wiki/Saving-and-loading-using-TagCompound to better understand Saving and Loading data.
        public override void SaveData(TagCompound tag)
        {
            tag["DeathShards"] = DeathShards;
        }

        public override void LoadData(TagCompound tag)
        {
            DeathShards = (int)tag["DeathShards"];
        }
    }
}
