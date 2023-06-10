using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using InverseMod.Dusts;
using Terraria.ModLoader;

namespace InverseMod.Projectiles
{
    public delegate void ExtraAction();

    public static class ProjectileExtras
    {
        public static void Explode(int index, int sizeX, int sizeY, ExtraAction visualAction = null, bool weakerExplosion = false)
        {
            Projectile projectile = Main.projectile[index];
            if (!projectile.active)
                return;

            projectile.tileCollide = false;
            projectile.alpha = 255;
            projectile.position = projectile.Center;
            projectile.width = sizeX;
            projectile.height = sizeY;
            projectile.position.X -= (projectile.width / 2f);
            projectile.position.Y -= (projectile.height / 2f);
            if (weakerExplosion)
            {
                projectile.damage = (int)(projectile.damage * .75f);
            }
            projectile.Damage();
            Main.projectileIdentity[projectile.owner, projectile.identity] = -1;
            projectile.position.X = projectile.position.X + (float)(projectile.width / 2);
            projectile.position.Y = projectile.position.Y + (float)(projectile.height / 2);
            projectile.width = (int)((float)sizeX / 5.8f);
            projectile.height = (int)((float)sizeY / 5.8f);
            projectile.position.X = projectile.position.X - (float)(projectile.width / 2);
            projectile.position.Y = projectile.position.Y - (float)(projectile.height / 2);
            visualAction();
        }
    }
}