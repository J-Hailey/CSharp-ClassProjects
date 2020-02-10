using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotContra
{
    class Hero : IViewable
    {
        public Hero(Tile start)
        {
            if (start == null)
            {
                throw new ArgumentNullException("start tile cannot be null");
            }

            this.X = start.X;
            this.Y = start.Y;
            this.MovementX = 0;
            this.MovementY = 0;
            this.Image = "hero_idle";
            this.IsJumping = false;
            this.JumpSpeed = 20;
            this.Projectiles = new List<Projectile>();
            this.Direction = 1; // 1 is Right, -1 is Left
            this.HeroRemainsOnScreen = 100;
            this.TimeBetweenShots = 10;
            this.TimeTilNextShot = 0;
        }

        public string Image { get; private set; }
        public int X { get; private set; }
        public int Y { get; private set; }
        public bool IsJumping { get; private set; }
        public int JumpSpeed { get; private set; }
        public int MovementX { get; private set; }
        public int MovementY { get; private set; }
        public List<Projectile> Projectiles { get; private set; }
        public int Direction { get; private set; }
        public bool IsDead { get; private set; }
        public int HeroRemainsOnScreen { get; private set; }
        public int TimeBetweenShots { get; private set; }
        public int TimeTilNextShot { get; private set; }

        public List<Tile> GetTiles()
        {
            List<Tile> tiles = new List<Tile>
            {
                new Tile(TileCode.PLAYER, X, Y, Image)
            };

            foreach (var projectile in Projectiles)
            {
                tiles.AddRange(projectile.GetTiles());
            }

            return tiles;
        }

        internal void ShootEnemies(List<Enemy> enemies)
        {
            foreach (Projectile projectile in this.Projectiles)
            {
                int x = projectile.X + ImageSelector.IMAGE_WIDTH / 2;
                int y = projectile.Y + ImageSelector.IMAGE_HEIGHT / 2;

                foreach (Enemy enemy in enemies)
                {
                    if (x > enemy.X &&
                        x < enemy.X + ImageSelector.IMAGE_WIDTH &&
                        y > enemy.Y &&
                        y < enemy.Y + ImageSelector.IMAGE_HEIGHT &&
                        !enemy.IsDead())
                    {
                        enemy.TakeDamage(1);
                        projectile.Dissolve();
                    }
                }
            }
        }

        internal void Dies()
        {
            this.IsDead = true;

            if (Direction < 0)
            {
                Image = "hero_dead_left";
            }

            else
            {
                Image = "hero_dead";
            }
        }

        internal void Left()
        {
            if (!IsDead)
            {
                Direction = -1;
                MovementX = -5;
                Image = "hero_run_left";
            }
        }

        internal void Right()
        {
            if (!IsDead)
            {
                Direction = 1;
                MovementX = 5;
                Image = "hero_run";
            }
        }

        internal void Shoot()
        {
            if (!IsDead && TimeTilNextShot == 0)
            {
                this.Projectiles.Add(new Projectile(this.X, this.Y, this.Direction));
                TimeTilNextShot = TimeBetweenShots;

                if (Direction < 0)
                {
                    Image = "hero_shoot_left";
                }
                else
                {
                    Image = "hero_shoot";
                }
            }
        }

        internal void Update(Terrain terrain)
        {
            this.Projectiles = this.Projectiles.FindAll(
                projectile => projectile.TimeToLive > 0
            );

            if (TimeTilNextShot > 0)
            {
                TimeTilNextShot--;
            }

            if (Y > 800)
            {
                IsDead = true;
            }

            if (IsDead)
            {
                HeroRemainsOnScreen--;
            }

            X += MovementX;
            Y += MovementY;

            if (IsJumping && MovementY <= JumpSpeed)
            {
                MovementY += 1;
            }

            if (!IsJumping && !terrain.IsLedgeAt(X, Y+ImageSelector.IMAGE_HEIGHT) &&
                !terrain.IsLedgeAt(X + ImageSelector.IMAGE_WIDTH / 2, Y + ImageSelector.IMAGE_HEIGHT) &&
                !terrain.IsLedgeAt(X + ImageSelector.IMAGE_WIDTH, Y + ImageSelector.IMAGE_HEIGHT))
            {
                IsJumping = true;
            }

            if (MovementY >= 0 && terrain.IsLedgeAt(X, Y + ImageSelector.IMAGE_HEIGHT))
            {
                IsJumping = false;
                MovementY = 0;
            }

            if (MovementY == 0 && terrain.IsLedgeAt(X, Y + ImageSelector.IMAGE_HEIGHT - 1))
            {
                Y = (int)(Y / ImageSelector.IMAGE_HEIGHT) * ImageSelector.IMAGE_HEIGHT;
            }

                foreach (var projectile in Projectiles)
            {
                projectile.Update();
            }
        }

        internal void StopRunning()
        {
            MovementX = 0;

            if (!IsDead && !IsJumping)
            {
                if (Image == "hero_run")
                {
                    Image = "hero_idle";
                }

                if (Image == "hero_run_left")
                {
                    Image = "hero_idle_left";
                }
            }
        }

        internal void Jump()
        {
            if (!IsJumping && !IsDead)
            {
                MovementY = -JumpSpeed;
                IsJumping = true;
            }
        }
    }
}
