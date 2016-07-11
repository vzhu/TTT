using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TicTacToe.Models
{
    public enum Player
    {
        None = 0,
        One,
        Two
    };

    public class Board
    {
        private int MAX_SIZE = 3;

        public Player[,] Grid { get; set; }
        public Board()
        {
            Grid = new Player[MAX_SIZE, MAX_SIZE];
            
            for (int i = 0; i < MAX_SIZE; i++)
            {
                for (int j = 0; j < MAX_SIZE; j++)
                {
                    Grid[i,j] = Player.None;
                }
            }
        }

        public Board CreateNewBoard()
        {
            return new Board();
        }
    }
}