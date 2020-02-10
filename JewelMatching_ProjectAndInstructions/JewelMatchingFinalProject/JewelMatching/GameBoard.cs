/*
Jovonny Hailey
Final Project - Bejeweled
CSCI 3005
12/13/2018
 */

using System;
using System.Collections.Generic;

namespace JewelMatching
{
    internal class GameBoard : IViewable
    {
        private int height;
        private int width;
        
        /// <summary>
        /// Given a height and a width, this will initialize the Jelly Game Board.
        /// </summary>
        /// <param name="width">the board width</param>
        /// <param name="height">the board height</param>
        public GameBoard(int width, int height)
        {
            this.width = width;
            this.height = height;
            

            this.Board = new JellyCode[width, height];

            
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    Board[i,j] = JellyCode.EMPTY;
                }
                
            }
            Randomize();
            Update();
            while (IsMatch)
            {
                Randomize();
                Update();
            }
            this.Score = 0;
        }
        
        /// <summary>
        /// Swaps two jelly icons on the board if they are next to each other.
        /// Each coordinate needs to be an index position into the array and not
        /// a mouse click location (because that's different).
        /// 
        /// </summary>
        /// <param name="i">x coordinate of first jelly</param>
        /// <param name="j">y coordinate of first jelly</param>
        /// <param name="p">x coordinate of second jelly</param>
        /// <param name="q">y coordinate of second jelly</param>
        /// <returns>True if the two jellies are next to each other and the swap is successful, false otherwise</returns>
        public bool Swap(int i, int j, int p, int q)
        {
            int diffip = i - p;
            int diffjq = j - q;

            if ( (diffip == 0 && (diffjq == -1 || diffjq == 1)) || (diffjq == 0 && (diffip == -1 || diffip == 1)))
            {
                JellyCode temp = Board[i, j];
                Board[i, j] = Board[p, q];
                Board[p, q] = temp;
                return true;
            }

            return false;
        }

        /// <summary>
        /// Gets the name of a image based on the JellyCode.
        /// </summary>
        /// <param name="code">A Jelly code</param>
        /// <returns>The string representation of the code.</returns>
        private String GetName(JellyCode code)
        {
            switch (code)
            {
                case JellyCode.BLACK: return "black";
                case JellyCode.BLUE: return "blue";
                case JellyCode.GREEN: return "green";
                case JellyCode.PINK: return "pink";
                case JellyCode.RED: return "red";
                case JellyCode.YELLOW: return "yellow";
            }

            return "empty";
        } 

        /// <summary>
        /// Given a number from 0 to 5, this will return a Jelly code
        /// for that number. If the code is anything other than from 0 to 5,
        /// the method will return a code of "EMPTY".
        /// </summary>
        /// <param name="x">a number from 0 to 5</param>
        /// <returns>A jelly code for the passed number.</returns>
        private JellyCode SelectJelly(int x)
        {
            switch (x)
            {
                case 0: return JellyCode.BLACK;
                case 1: return JellyCode.BLUE;
                case 2: return JellyCode.GREEN;
                case 3: return JellyCode.PINK;
                case 4: return JellyCode.RED;
                case 5: return JellyCode.YELLOW;
            }

            return JellyCode.EMPTY;
        }

        /// <summary>
        /// Randomizes each tile across the visible Gameboard.
        /// </summary>
        public void Randomize()
        {
            Random rand = new Random();
            

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                { 
                    if (Board[i, j] == JellyCode.EMPTY)
                    {
                        int randomJewels = rand.Next(6);
                        Board[i, j] = SelectJelly(randomJewels);
                    }
                }
            }
        }
        
        /// <summary>
        /// The Gravity Function. The candies fall. 
        /// </summary>
        public void Falling()
        {
            IsFalling = true;
            while (IsFalling)
            {
                IsFalling = false;
                for (int i = 0; i < width; i++)
                {
                    for (int j = 0; j < height - 1; j++)
                    {
                        if (Board[i, j] != JellyCode.EMPTY && Board[i, j + 1] == JellyCode.EMPTY)
                        {
                            Swap(i, j, i, j + 1);
                            IsFalling = true;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Updates the board for runs across it
        /// </summary>
        public void Update()
        {
            IsMatch = false;

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    if (Board[i, j] != JellyCode.EMPTY)
                    {
                        if (i < width - 4 && Board[i, j] == Board[i + 1, j] && Board[i, j] == Board[i + 2, j] && Board[i, j] == Board[i + 3, j] && Board[i, j] == Board[i + 4, j])
                        {
                            for (int k = 0; k < 5; k++)
                                Board[i + k, j] = JellyCode.EMPTY;

                            Score += 200;
                            IsMatch = true;
                        }
                        else if (i < width - 3 && Board[i, j] == Board[i + 1, j] && Board[i, j] == Board[i + 2, j] && Board[i, j] == Board[i + 3, j])
                        {
                            for (int k = 0; k < 4; k++)
                                Board[i + k, j] = JellyCode.EMPTY;

                            Score += 100;
                            IsMatch = true;
                        }
                        else if (i < width - 2 && Board[i, j] == Board[i + 1, j] && Board[i, j] == Board[i + 2, j])
                        {
                            for (int k = 0; k < 3; k++)
                                Board[i + k, j] = JellyCode.EMPTY;

                            Score += 50;
                            IsMatch = true;
                        }

                        if (j < height - 4 && Board[i, j] == Board[i, j + 1] && Board[i, j] == Board[i, j + 2] && Board[i, j] == Board[i, j + 3] && Board[i, j] == Board[i, j + 4])
                        {
                            for (int k = 0; k < 5; k++)
                                Board[i, j + k] = JellyCode.EMPTY;

                            Score += 200;
                            IsMatch = true;
                        }
                        else if (j < height - 3 && Board[i, j] == Board[i, j + 1] && Board[i, j] == Board[i, j + 2] && Board[i, j] == Board[i, j + 3])
                        {
                            for (int k = 0; k < 4; k++)
                                Board[i, j + k] = JellyCode.EMPTY;

                            Score += 100;
                            IsMatch = true;
                        }
                        else if (j < height - 2 && Board[i, j] == Board[i, j + 1] && Board[i, j] == Board[i, j + 2])
                        {
                            for (int k = 0; k < 3; k++)
                                Board[i, j + k] = JellyCode.EMPTY;

                            Score += 50;
                            IsMatch = true;
                        }
                    }

                }
            }
        }

        /// <summary>
        /// Gets all of the tiles necessary to be placed on the screen.
        /// </summary>
        /// <returns>The list of tiles to display.</returns>
        public List<Tile> GetTiles()
        {
            List<Tile> tiles = new List<Tile>();

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    String name = GetName(Board[i, j]);
                    tiles.Add(new Tile(Board[i,j], i * ImageSelector.IMAGE_WIDTH, j * ImageSelector.IMAGE_HEIGHT, name));
                }
            }

            return tiles;
        }

        public JellyCode[,] Board { get; private set; }
        public int Score { get; private set; }
        public bool IsMatch { get; private set; }
        public bool IsFalling { get; private set; }
    }
}