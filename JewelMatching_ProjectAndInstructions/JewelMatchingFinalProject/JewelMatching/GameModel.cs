/*
Jovonny Hailey
Final Project - Bejeweled
CSCI 3005
12/13/2018
 */

using System;
using System.Collections.Generic;
using System.Windows;

namespace JewelMatching
{
    class GameModel : IViewable
    {
        /// <summary>
        /// Public constructor that takes in the width and height of the game board
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public GameModel(int width, int height)
        {
            this.Board = new GameBoard(width, height);
            this.Clicks = true;
            this.X = 0;
            this.Y = 0;
        }

        internal GameBoard Board { get; private set; }
        public bool Clicks { get; private set; }
        public int X { get; private set; }
        public int Y { get; private set; }

        public void RegisterClick (int x, int y)
        {
         
            if (Clicks)
            {
                Clicks = false;
                this.X = x / ImageSelector.IMAGE_WIDTH;
                this.Y = y / ImageSelector.IMAGE_HEIGHT;
            }

            else
            {
                Clicks = true;
                Board.Swap(X, Y, x / ImageSelector.IMAGE_WIDTH, y / ImageSelector.IMAGE_HEIGHT);
                Board.Update();
                Board.Falling();
                Board.Randomize();
            }
        }

        /// <summary>
        /// Returns the GameBoard object's GetTiles content
        /// </summary>
        /// <returns></returns>
        public List<Tile> GetTiles()
        {
            return Board.GetTiles();
        }
    }
}