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

        public bool PreCreateBoardValidation(string channel)
        {
            if (this.channelToBoardDb.Keys.Contains(channel))
            {
                return false;
            }

            return true;
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

            // add the key, game
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