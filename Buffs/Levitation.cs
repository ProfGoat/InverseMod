using System;
using Terraria;
using Terraria.ModLoader;

namespace InverseMod.Buffs
{
    public class Levitation : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Levitation");
            Description.SetDefault("You are slowly floating upward");
            Main.debuff[Type] = true;
            Main.pvpBuff[Type] = true;
            Main.buffNoSave[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            // Apply the floating effect to the player
            player.velocity.Y -= 0.3f; // Adjust this value to control the floating speed
                                       // Apply the side-to-side movement
            float oscillationSpeed = 0.2f; // Adjust this value to control the side-to-side speed
            float oscillationAmplitude = 2f; // Adjust this value to control the side-to-side distance
            player.position.X += (float)Math.Sin(Main.GameUpdateCount * oscillationSpeed) * oscillationAmplitude;

        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            // Apply the floating effect to the enemy
            npc.velocity.Y -= 0.3f; // Adjust this value to control the floating speed
                                    // Apply the side-to-side movement
            float oscillationSpeed = 0.2f; // Adjust this value to control the side-to-side speed
            float oscillationAmplitude = 2f; // Adjust this value to control the side-to-side distance
            npc.position.X += (float)Math.Sin(Main.GameUpdateCount * oscillationSpeed) * oscillationAmplitude;

        }
    }
}