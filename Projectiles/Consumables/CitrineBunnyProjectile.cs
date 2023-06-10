using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace InverseMod.Projectiles.Consumables
{
    public class CitrineBunnyProjectile : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.Bunny);
            Projectile.aiStyle = 0;
            Projectile.timeLeft = 0;
        }

        //When the projectile is killed or despawns, spawn the NPC.
        public override void Kill(int timeLeft)
        {
            if (Main.netMode != NetmodeID.Server)
            {
                NPC.NewNPC(Projectile.GetSource_FromThis(), (int)Projectile.position.X, (int)Projectile.position.Y, ModContent.NPCType<NPCs.Critters.CitrineBunny>());
            }
        }
    }
}