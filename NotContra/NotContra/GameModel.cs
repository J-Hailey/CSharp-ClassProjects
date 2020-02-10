using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace NotContra
{
    class GameModel : IViewable
    {
        public GameModel(Terrain terrain, Hero hero)
        {
            if (terrain == null)
            {
                throw new ArgumentNullException("Terrain is null.");
            }

            if (hero == null)
            {
                throw new ArgumentNullException("Hero is null");
            }

            Manager = new EnemyManager();
            Terrain = terrain;
            Hero = hero;
        }

        public bool ShouldRestart() => Hero.HeroRemainsOnScreen < 0;

        public Terrain Terrain { get; private set; }
        public Hero Hero { get; private set; }
        public EnemyManager Manager { get; private set; }

        public List<Tile> GetTiles()
        {
            List<Tile> tiles = new List<Tile>();
            tiles.AddRange(Terrain.GetTiles());
            tiles.AddRange(Hero.GetTiles());
            tiles.AddRange(Manager.GetTiles());
            return tiles;
        }

        internal void Update()
        {
            this.Hero.Update(Terrain);
            this.Manager.GenerateEnemy(Hero);
            this.Manager.UpdateEnemies(Terrain);
            this.Manager.CollideWithHero(Hero);
            this.Hero.ShootEnemies(this.Manager.Enemies);

            // Check for end of game
            Tile end = Terrain.GetEnd();

            int x = Hero.X + ImageSelector.IMAGE_WIDTH / 2;
            int y = Hero.Y + ImageSelector.IMAGE_HEIGHT / 2;

            if (x > end.X &&
                x < end.X + ImageSelector.IMAGE_WIDTH &&
                y > end.Y &&
                y < end.Y + ImageSelector.IMAGE_HEIGHT)
            {
                MessageBox.Show("Congrats! You win!");
            }
        }
    }
}
