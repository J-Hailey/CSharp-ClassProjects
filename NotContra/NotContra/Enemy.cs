using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotContra
{
    abstract class Enemy : IViewable
    {

        public Enemy(int hitpoints, int x, int y, int movementX, int movementY)
        {
            HitPoints = hitpoints;
            X = x;
            Y = y;
            MovementX = movementX;
            MovementY = movementY;
            IsJumping = false;
            EnemyRemainsOnScreen = 25;
        }

        public void TakeDamage(int damage)
        {
            if (damage < 1)
            {
                throw new ArgumentException("damage must be positive.");
            }

            HitPoints -= damage;
        }

        public bool IsDead() => HitPoints <= 0;

        public void Update(Terrain terrain)
        {
            X += MovementX;
            Y += MovementY;

            if (IsJumping)
            {
                MovementY += 1;
            }

            if (!IsJumping && (
                !terrain.IsLedgeAt(X, Y + ImageSelector.IMAGE_HEIGHT) &&
                !terrain.IsLedgeAt(X + ImageSelector.IMAGE_WIDTH/2, Y + ImageSelector.IMAGE_HEIGHT) &&
                !terrain.IsLedgeAt(X + ImageSelector.IMAGE_WIDTH, Y + ImageSelector.IMAGE_HEIGHT)
                ))
            {
                IsJumping = true;
            }

            if (terrain.IsLedgeAt(X, Y+ImageSelector.IMAGE_HEIGHT))
            {
                IsJumping = false;
                MovementY = 0;
            }

            if (MovementY == 0 && terrain.IsLedgeAt(X, Y + ImageSelector.IMAGE_HEIGHT - 1))
            {
                Y = (int)(Y / ImageSelector.IMAGE_HEIGHT) * ImageSelector.IMAGE_HEIGHT;
            }

            if (IsDead())
            {
                EnemyRemainsOnScreen--;
            }
        }

        public List<Tile> GetTiles()
        {
            return new List<Tile>
            {
                new Tile(TileCode.ENEMY, X, Y, GetImage())
            };

        }

        public abstract string GetImage();

        public int HitPoints { get; private set; }
        public bool IsJumping { get; private set; }
        public int MovementX { get; set; }
        public int MovementY { get; set; }
        public int X { get; private set; }
        public int Y { get; private set; }
        public int EnemyRemainsOnScreen { get; private set; }
    }
}
