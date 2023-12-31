﻿using Terraria.ID;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;
using System;
using InverseMod;

namespace InverseMod.Items.Ores
{
    public class Jade : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 15;
            ItemID.Sets.SortingPriorityMaterials[Item.type] = 175;
        }

        public override void SetDefaults()
        {
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTurn = true;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.autoReuse = true;
            Item.rare = ItemRarityID.LightRed;
            Item.maxStack = 9999;
            Item.consumable = true;
            Item.createTile = ModContent.TileType<Tiles.JadePlaced>();
            Item.width = 12;
            Item.height = 12;
            Item.value = 45000;
        }
    }
}