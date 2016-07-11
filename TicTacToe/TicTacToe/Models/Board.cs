using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TicTacToe.Models
{
    public enum Owners
    {
        None = 0,
        PlayerOne,
        PlayerTwo
    };

    public class Board
    {
        private int MAX_SIZE = 3;

        public string Turn { get; private set; }
        public string Player1 { get; private set; }
        public string Player2 { get; private set; }
        public string Channel { get; private set; }

        public Owners Winner { get; private set; }
        public Owners[,] Grid { get; set; }
        public Board(string player1, string player2, string channel)
        {
            this.Grid = new Owners[MAX_SIZE, MAX_SIZE];
            
            for (int i = 0; i < MAX_SIZE; i++)
            {
                for (int j = 0; j < MAX_SIZE; j++)
                {
                    this.Grid[i,j] = Owners.None;
                }
            }

            this.Player1 = player1;
            this.Player2 = player2;
            this.Channel = channel;
            this.Turn = Player1;
            this.Winner = Owners.None;
        }

        public bool updateBoard(string player, int position)
        {
            if (string.Compare(player, this.Turn) != 0 || this.Winner != Owners.None)
            {
                return false;
            }

            if (position < 0 || position > MAX_SIZE * MAX_SIZE - 1)
            {
                return false;
            }

            int y = position / MAX_SIZE;
            int x = position % MAX_SIZE;

            if (this.Grid[y,x] != Owners.None)
            {
                return false;
            }

            this.Grid[y, x] = (string.Compare(Player1, this.Turn) == 0) ? Owners.PlayerOne : Owners.PlayerTwo;
            this.Turn = (string.Compare(Player1, this.Turn) == 0) ? Player2 : Player1;

            this.Winner = CheckForCompleteBoard();
            return true;
        }

        private Owners CheckForCompleteBoard()
        {
            for (int y = 0; y < MAX_SIZE; y++)
            {
                Owners target = this.Grid[y, 0];
                if (target == Owners.None)
                {
                    continue;
                }

                bool solved = true;
                for (int x = 1; x < MAX_SIZE; x++)
                {
                    if (this.Grid[y,x] != target)
                    {
                        solved = false;
                        break;
                    }
                }
                
                if (solved)
                {
                    return target;
                }
            }

            for (int x = 0; x < MAX_SIZE; x++)
            {
                Owners target = this.Grid[0, x];
                if (target == Owners.None)
                {
                    continue;
                }

                bool solved = true;
                for (int y = 1; y < MAX_SIZE; y++)
                {
                    if (this.Grid[y, x] != target)
                    {
                        solved = false;
                        break;
                    }
                }

                if (solved)
                {
                    return target;
                }
            }

            {
                bool solved = true;
                Owners target = this.Grid[0, 0];
                if (target == Owners.None)
                {
                    solved = false;
                }
                else
                {
                    for (int x = 1, y = 1; x < MAX_SIZE && y < MAX_SIZE; x++, y++)
                    {
                        if (this.Grid[y, x] != target)
                        {
                            solved = false;
                            break;
                        }
                    }
                }

                if (solved)
                {
                    return target;
                }
            }

            {
                bool solved = true;
                Owners target = this.Grid[MAX_SIZE - 1, 0];
                for (int x = 1, y = MAX_SIZE - 2; x < MAX_SIZE && y >= 0; x++, y--)
                {
                    if (this.Grid[y, x] != target)
                    {
                        solved = false;
                        break;
                    }
                }

                if (solved)
                {
                    return target;
                }
            }

            return Owners.None;
        }

        public string Prettify()
        {
            string result = "Current Turn: " + this.Turn + "\n";
            
            for (int y = 0; y < MAX_SIZE; y++)
            {
                for (int x = 0; x < MAX_SIZE; x++)
                {
                    result += PrettifyLocation(this.Grid[y, x]);

                    if (x != MAX_SIZE - 1)
                    {
                        result += " ";
                    }
                }

                if (y != MAX_SIZE - 1)
                {
                    result += "\n";
                }
            }

            if (this.Winner != Owners.None)
            {
                result += "\nGame is complete.\nWinner is: " + ((this.Winner == Owners.PlayerOne) ? Player1 : Player2);
            }

            return result;
        }

        private string PrettifyLocation(Owners pos)
        {
            switch (pos)
            {
                case Owners.PlayerOne:
                    return "X";
                case Owners.PlayerTwo:
                    return "O";
                case Owners.None:
                default:
                    return "-";
            }
        }
    }
}