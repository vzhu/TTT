using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TicTacToe.Models
{
    public enum Owners
    {
        None = 0,
        One,
        Two
    };

    public class Board
    {
        private int MAX_SIZE = 3;

        public string Player1 { get; private set; }
        public string Player2 { get; private set; }
        public string Channel { get; private set; }

        public Owners[,] Grid { get; set; }
        public Board(string player1, string player2, string channel)
        {
            Grid = new Owners[MAX_SIZE, MAX_SIZE];
            
            for (int i = 0; i < MAX_SIZE; i++)
            {
                for (int j = 0; j < MAX_SIZE; j++)
                {
                    Grid[i,j] = Owners.None;
                }
            }

            Player1 = player1;
            Player2 = player2;
            Channel = channel;
        }
    }
}