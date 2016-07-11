using TicTacToe.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace TicTacToe.Controllers
{
    public class TTTController : ApiController
    {
        List<Board> boards;

        public TTTController()
        {
            boards = new List<Board>();
            var board = new Board();

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    board.Grid[i, j] = Player.One;
                }
            }

            boards.Add(board);
        }

        public List<Board> GetAllBoards()
        {
            return boards;
        }

        public IHttpActionResult GetBoard(int id)
        {
            return Ok(boards);
        }
    }
}
