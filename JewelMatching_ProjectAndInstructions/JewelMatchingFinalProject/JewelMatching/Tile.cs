/*
Jovonny Hailey
Final Project - Bejeweled
CSCI 3005
12/13/2018
 */

namespace JewelMatching
{
    enum JellyCode { BLACK, BLUE, GREEN, PINK, RED, YELLOW, EMPTY };

    class Tile
    {
        /// <summary>
        /// Given a JellyCode, x coordinate, y coordinate, and a name,
        /// this will build a Tile which will store all of the necessary
        /// information for representing the tile on the screen.
        /// </summary>
        /// <param name="code">A code representing the tile</param>
        /// <param name="x">the x position to draw to the screen</param>
        /// <param name="y">the y position to draw to the screen</param>
        /// <param name="name">the name in the image resources file to draw</param>
        public Tile(JellyCode code, int x, int y, string name)
        {
            Code = code;
            X = x;
            Y = y;
            Name = name;
        }

        public JellyCode Code { get; private set; }
        public string Name { get; private set; }
        public int X { get; private set; }
        public int Y { get; private set; }
    }
}