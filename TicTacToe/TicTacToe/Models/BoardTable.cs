using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TicTacToe.Models
{
    public class BoardTable
    {
        private static string delimiter = "__";
        private static BoardTable instance;

        public Dictionary<string, Board> userToBoardDb { get; private set; }
        public Dictionary<string, Board> channelToBoardDb { get; private set; }

        private BoardTable()
        {
            this.userToBoardDb = new Dictionary<string, Board>();
            this.channelToBoardDb = new Dictionary<string, Board>();
        }

        public static BoardTable Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new BoardTable();
                }
                return instance;
            }
        }

        public bool PreCreateBoardValidation(string player1, string player2, string channel)
        {
            Board board;
            this.channelToBoardDb.TryGetValue(channel, out board);

            if (board == null)
                return true;

            // if someone has won, a new game can be created
            if (board.Winner != Owners.None)
            {
                DeleteBoard(player1, player2, channel);
                return true;
            }

            return false;
        }

        public bool CreateBoard(string player1, string player2, string channel)
        {
            if (string.IsNullOrEmpty(player1) || string.IsNullOrEmpty(player2) || (string.Compare(player1, player2) == 0))
            {
                return false;
            }

            string player1Key = CreateKey(player1, channel);
            string player2Key = CreateKey(player2, channel);

            // already contains the key
            if (this.userToBoardDb.ContainsKey(player1Key) || this.userToBoardDb.ContainsKey(player2Key))
                return false;

            // add the key --> game to dbs
            Board board = new Board(player1, player2, channel);
            this.userToBoardDb.Add(player1Key, board);
            this.userToBoardDb.Add(player2Key, board);
            this.channelToBoardDb.Add(channel, board);

            return true;
        }

        public bool DeleteBoard(string player1, string player2, string channel)
        {
            string player1Key = CreateKey(player1, channel);
            string player2Key = CreateKey(player2, channel);

            if (this.userToBoardDb.ContainsKey(player1Key))
            {
                this.userToBoardDb.Remove(player1Key);
            }

            if (this.userToBoardDb.ContainsKey(player2Key))
            {
                this.userToBoardDb.Remove(player2Key);
            }

            if (this.channelToBoardDb.ContainsKey(channel))
            {
                this.channelToBoardDb.Remove(channel);
            }

            return true;
        }

        public Board UpdateBoard(string player, int pos, string channel)
        {
            string player1Key = CreateKey(player, channel);
            if (!this.userToBoardDb.ContainsKey(player1Key))
            {
                return null;
            }

            Board board;
            this.channelToBoardDb.TryGetValue(channel, out board);

            if (board == null)
            {
                return null;
            }

            if (!board.updateBoard(player, pos))
            {
                return null;
            }

            return board;
        }

        public Board GetBoard(string channel)
        {
            if (string.IsNullOrEmpty(channel) || !this.channelToBoardDb.ContainsKey(channel))
            {
                return null;
            }

            Board result;
            this.channelToBoardDb.TryGetValue(channel, out result);

            return result;
        }

        private string CreateKey(string player, string channel)
        {
            if (string.IsNullOrEmpty(player) || string.IsNullOrEmpty(channel))
            {
                return string.Empty;
            }

            return player + delimiter + channel;
        }
    }
}