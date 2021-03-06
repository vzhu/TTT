﻿using TicTacToe.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace TicTacToe.Controllers
{
    public class TTTController : ApiController
    {
        private readonly string CreateToken = "mO256MrttY6MABLj4ZRmKNMQ";
        private readonly string GetToken = "AT4yczKT9VkWaWWyYF5OcIOv";
        private readonly string UpdateToken = "VueU1JC3aKl4Awyl7PmvwjRr";
        private readonly string DeleteToken = "UX9mD35pkvr8UNpJNimoOcci";

        private BoardTable model = BoardTable.Instance;

        public TTTController()
        {
        }

        [HttpPost]
        [Route("api/ttt/create")]
        public IHttpActionResult CreateBoard([FromBody]JObject data)
        {
            string token = data["token"].ToObject<string>();
            if (string.Compare(token, this.CreateToken) != 0)
            {
                return BadRequest("Could not create board, mismatched token");
            }

            string player1 = data["user_name"].ToObject<string>();
            string player2 = data["text"].ToObject<string>();
            string channel = data["channel_id"].ToObject<string>();

            if(!this.model.PreCreateBoardValidation(player1, player2, channel))
            {
                return BadRequest("Could not create board, there is already a session in place");
            }

            if (!this.model.CreateBoard(player1, player2, channel))
            {
                return BadRequest("Could not create board, invalid user input");
            }

            return Ok();
        }

        [HttpPost]
        [Route("api/ttt/delete")]
        public IHttpActionResult DeleteBoard([FromBody]JObject data)
        {
            string token = data["token"].ToObject<string>();
            if (string.Compare(token, this.DeleteToken) != 0)
            {
                return BadRequest("Could not delete board, mismatched token");
            }

            string channel = data["channel_id"].ToObject<string>();
            string player1 = data["user_name"].ToObject<string>();
            string player2 = data["text"].ToObject<string>();

            if (!this.model.DeleteBoard(player1, player2, channel))
            {
                return BadRequest("Could not delete board, invalid user input");
            }

            return Ok();
        }

        [HttpPost]
        [Route("api/ttt/get")]
        public IHttpActionResult GetBoard([FromBody]JObject data)
        {
            string token = data["token"].ToObject<string>();
            if (string.Compare(token, this.GetToken) != 0)
            {
                return BadRequest("Could not get board, mismatched token");
            }

            string channel = data["channel_id"].ToObject<string>();

            Board result = this.model.GetBoard(channel);
            if (result == null)
            {
                return BadRequest("Could not delete board, invalid user input");
            }

            JObject response = FormatBoard(result);
            return Ok(response);
        }

        [HttpPost]
        [Route("api/ttt/update")]
        public IHttpActionResult UpdateBoard([FromBody]JObject data)
        {
            string token = data["token"].ToObject<string>();
            if (string.Compare(token, this.UpdateToken) != 0)
            {
                return BadRequest("Could not update board, mismatched token");
            }

            string player = data["user_name"].ToObject<string>();
            string channel = data["channel_id"].ToObject<string>();
            int position = data["text"].ToObject<int>();

            Board result = this.model.UpdateBoard(player, position, channel);
            if (result == null)
            {
                return BadRequest("Could not delete board, invalid user input");
            }

            JObject response = FormatBoard(result);
            return Ok(response);
        }

        private JObject FormatBoard(Board result)
        {
            JObject response = new JObject();
            response["response_type"] = "in_channel";
            response["text"] = result.Prettify();

            return response;
        }
    }
}
