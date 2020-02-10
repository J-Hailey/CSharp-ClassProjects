using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotContra
{
    class EnemyManager : IViewable
    {
        private readonly Random rand;

        public EnemyManager()
        {
            this.Enemies = new List<Enemy>();
            this.rand = new Random();
        }

        public void GenerateEnemy(Hero hero)
        {
            int rollToGenerate = rand.Next(80);

            if (rollToGenerate == 0)
            {
                int x = hero.X + 600;
                int y = hero.Y;

                ZombieLad lad = new ZombieLad(x, y, -2);

                this.Enemies.Add(lad);
            }
        }

        public List<Tile> GetTiles()
        {
            List<Tile> tiles = new List<Tile>();
            foreach (Enemy enemy in Enemies)
            {
                tiles.AddRange(enemy.GetTiles());
            }

            return tiles;
        }

        public List<Enemy> Enemies { get; private set; }

        internal void UpdateEnemies(Terrain terrain)
        {
            Enemies = Enemies.FindAll(enemy => enemy.EnemyRemainsOnScreen > 0);

            foreach (Enemy enemy in Enemies)
            {
                enemy.Update(terrain);
            }
        }

        internal void CollideWithHero(Hero hero)
        {
            foreach (Enemy enemy in Enemies)
            {
                int x = enemy.X + ImageSelector.IMAGE_WIDTH / 2;
                int y = enemy.Y + ImageSelector.IMAGE_HEIGHT / 2;

                if (x > hero.X &&
                    x < hero.X + ImageSelector.IMAGE_WIDTH &&
                    y > hero.Y &&
                    y < hero.Y + ImageSelector.IMAGE_HEIGHT &&
                    !enemy.IsDead())
                {
                    hero.Dies();
                }
            }
        }
    }
}
