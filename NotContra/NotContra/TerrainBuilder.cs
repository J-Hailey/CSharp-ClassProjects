using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotContra
{
    class TerrainBuilder
    {
        public TerrainBuilder(Stream stream)
        {
            Stream = stream;
            Terrain = new Terrain();
        }

        public void Build()
        {
            StreamReader reader = new StreamReader(Stream);

            int y = 0;
            while (!reader.EndOfStream)
            {
                y = AddImageToTerrain(reader, y);
            }

            Terrain.BuildLedgeMap();
        }

        private int AddImageToTerrain(StreamReader reader, int y)
        {
            string letters = reader.ReadLine();

            int x = 0;
            foreach (char letter in letters)
            {
                SelectTile(y, x, letter);
                x += ImageSelector.IMAGE_WIDTH;
            }
            y += ImageSelector.IMAGE_HEIGHT;
            return y;
        }

        private void SelectTile(int y, int x, char letter)
        {
            switch (letter)
            {
                case 'L':
                    Terrain.Add(new Tile(TileCode.LEDGE, x, y, "world_ledge"));
                    break;
                case 'W':
                    Terrain.Add(new Tile(TileCode.WALL, x, y, ""));
                    break;
                case 'S':
                    Terrain.Add(new Tile(TileCode.START, x, y, "world_start"));
                    break;
                case 'D':
                    Terrain.Add(new Tile(TileCode.OPEN, x, y, "world_dirt"));
                    break;
                case 'E':
                    Terrain.Add(new Tile(TileCode.END, x, y, "world_end"));
                    break;
            }
        }

        public Stream Stream { get; private set; }
        public Terrain Terrain { get; private set; }
    }
}
