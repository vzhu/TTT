using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TicTacToe.Models;

namespace TicTacToeUnitTests
{
    [TestClass]
    public class ModelUnitTests
    {
        [TestMethod]
        public void Board_WithValidArguements_CreatesExpectedBoard()
        {
            // Arrange
            string player1 = "player1";
            string player2 = "player2";
            string channel = "channel";

            // Act
            Board board = new Board(player1, player2, channel);

            // Assert
            Assert.AreEqual(player1, board.Player1);
            Assert.AreEqual(player2, board.Player2);
            Assert.AreEqual(channel, board.Channel);
            Assert.AreEqual(player1, board.Turn);
            Assert.AreEqual(Owners.None, board.Winner);

            for (int y = 0; y < Board.MAX_SIZE; y++)
            {
                for (int x = 0; x < Board.MAX_SIZE; x++)
                {
                    Assert.AreEqual(Owners.None, board.Grid[y,x]);
                }
            }
        }

        [TestMethod]
        public void Board_UpdateTwiceAsSamePlayer_FailsAsExpected()
        {
            // Arrange
            string player1 = "player1";
            string player2 = "player2";
            string channel = "channel";
            Board board = new Board(player1, player2, channel);

            // Act
            bool result1 = board.updateBoard(player1, 5);
            bool result2 = board.updateBoard(player1, 6);

            // Assert
            Assert.IsTrue(result1);
            Assert.IsFalse(result2);
            Assert.AreEqual(Owners.None, board.Winner);
        }

        [TestMethod]
        public void Board_UpdateOutOfRange_FailsAsExpected()
        {
            // Arrange
            string player1 = "player1";
            string player2 = "player2";
            string channel = "channel";
            Board board = new Board(player1, player2, channel);

            // Act
            bool result1 = board.updateBoard(player1, -1);
            bool result2 = board.updateBoard(player2, 9);

            // Assert
            Assert.IsFalse(result1);
            Assert.IsFalse(result2);
            Assert.AreEqual(Owners.None, board.Winner);
        }

        [TestMethod]
        public void Board_UpdateVertical036Wins_SucceedsAsExpected()
        {
            // Arrange
            string player1 = "player1";
            string player2 = "player2";
            string channel = "channel";
            Board board = new Board(player1, player2, channel);

            // Act
            board.updateBoard(player1, 0);
            board.updateBoard(player2, 1);
            board.updateBoard(player1, 3);
            board.updateBoard(player2, 4);
            board.updateBoard(player1, 6);

            // Assert
            Assert.AreEqual(Owners.PlayerOne, board.Winner);
        }

        [TestMethod]
        public void Board_UpdateVertical147Wins_SucceedsAsExpected()
        {
            // Arrange
            string player1 = "player1";
            string player2 = "player2";
            string channel = "channel";
            Board board = new Board(player1, player2, channel);

            // Act
            board.updateBoard(player1, 1);
            board.updateBoard(player2, 0);
            board.updateBoard(player1, 4);
            board.updateBoard(player2, 3);
            board.updateBoard(player1, 7);

            // Assert
            Assert.AreEqual(Owners.PlayerOne, board.Winner);
        }

        [TestMethod]
        public void Board_UpdateVertical258Wins_SucceedsAsExpected()
        {
            // Arrange
            string player1 = "player1";
            string player2 = "player2";
            string channel = "channel";
            Board board = new Board(player1, player2, channel);

            // Act
            board.updateBoard(player1, 2);
            board.updateBoard(player2, 0);
            board.updateBoard(player1, 5);
            board.updateBoard(player2, 3);
            board.updateBoard(player1, 8);

            // Assert
            Assert.AreEqual(Owners.PlayerOne, board.Winner);
        }

        [TestMethod]
        public void Board_UpdateHorizontal012Wins_SucceedsAsExpected()
        {
            // Arrange
            string player1 = "player1";
            string player2 = "player2";
            string channel = "channel";
            Board board = new Board(player1, player2, channel);

            // Act
            board.updateBoard(player1, 0);
            board.updateBoard(player2, 3);
            board.updateBoard(player1, 1);
            board.updateBoard(player2, 4);
            board.updateBoard(player1, 2);

            // Assert
            Assert.AreEqual(Owners.PlayerOne, board.Winner);
        }

        [TestMethod]
        public void Board_UpdateHorizontal345Wins_SucceedsAsExpected()
        {
            // Arrange
            string player1 = "player1";
            string player2 = "player2";
            string channel = "channel";
            Board board = new Board(player1, player2, channel);

            // Act
            board.updateBoard(player1, 3);
            board.updateBoard(player2, 0);
            board.updateBoard(player1, 4);
            board.updateBoard(player2, 1);
            board.updateBoard(player1, 5);

            // Assert
            Assert.AreEqual(Owners.PlayerOne, board.Winner);
        }

        [TestMethod]
        public void Board_UpdateHorizontal678Wins_SucceedsAsExpected()
        {
            // Arrange
            string player1 = "player1";
            string player2 = "player2";
            string channel = "channel";
            Board board = new Board(player1, player2, channel);

            // Act
            board.updateBoard(player1, 6);
            board.updateBoard(player2, 0);
            board.updateBoard(player1, 7);
            board.updateBoard(player2, 1);
            board.updateBoard(player1, 8);

            // Assert
            Assert.AreEqual(Owners.PlayerOne, board.Winner);
        }

        [TestMethod]
        public void Board_UpdateDiagonal048Wins_SucceedsAsExpected()
        {
            // Arrange
            string player1 = "player1";
            string player2 = "player2";
            string channel = "channel";
            Board board = new Board(player1, player2, channel);

            // Act
            board.updateBoard(player1, 0);
            board.updateBoard(player2, 1);
            board.updateBoard(player1, 4);
            board.updateBoard(player2, 5);
            board.updateBoard(player1, 8);

            // Assert
            Assert.AreEqual(Owners.PlayerOne, board.Winner);
        }

        [TestMethod]
        public void Board_UpdateDiagonal246Wins_SucceedsAsExpected()
        {
            // Arrange
            string player1 = "player1";
            string player2 = "player2";
            string channel = "channel";
            Board board = new Board(player1, player2, channel);

            // Act
            board.updateBoard(player1, 2);
            board.updateBoard(player2, 1);
            board.updateBoard(player1, 4);
            board.updateBoard(player2, 5);
            board.updateBoard(player1, 6);

            // Assert
            Assert.AreEqual(Owners.PlayerOne, board.Winner);
        }
    }
}
