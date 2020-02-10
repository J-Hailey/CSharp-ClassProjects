using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotContra
{
    enum TileCode { WALL, LEDGE, START, END, OPEN, PLAYER, ENEMY, PROJECTILE };

    class Tile
    {
        public Tile(TileCode code, int x, int y, string name)
        {
            Code = code;
            X = x;
            Y = y;
            Name = name;
        }

        public TileCode Code { get; private set; }
        public string Name { get; private set; }
        public int X { get; private set; }
        public int Y { get; private set; }
    }
}
