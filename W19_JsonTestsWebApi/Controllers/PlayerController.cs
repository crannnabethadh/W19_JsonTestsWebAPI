using Dapper;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using W19_WebApi.Models;

namespace W19_WebApi.Controllers
{
    [Authorize]
    [RoutePrefix("api/Player")]
    public class PlayerController : ApiController
    {
        // POST api/Player/InsertNewPlayer
        [HttpPost]
        [Route("InsertNewPlayer")]
        public IHttpActionResult InsertNewPlayer(PlayerModel player)
        {
            using (IDbConnection cnn = new ApplicationDbContext().Database.Connection)
            {
                string sql = "INSERT INTO dbo.Players(Id, FirstName, LastName, Email, DateOfBirth, NickName, City, BlobUri) " +
                    $"VALUES('{player.Id}','{player.FirstName}','{player.LastName}','{player.Email}','{player.DateOfBirth}','{player.NickName}','{player.City}','{player.BlobUri}')";
                try
                {
                    cnn.Execute(sql);
                }
                catch (Exception ex)
                {
                    return BadRequest("Error inserting player in database: " + ex.Message);
                }

                return Ok();
            }
        }

        // GET api/Player/GetPlayerInfo
        [HttpGet]
        [Route("GetPlayerInfo")]
        public PlayerModel GetPlayerInfo()
        {
            string authenticatedAspNetUserId = RequestContext.Principal.Identity.GetUserId();
            using (IDbConnection cnn = new ApplicationDbContext().Database.Connection)
            {
                string sql = $"SELECT Id, Email, FirstName, LastName, NickName, City, DateOfBirth, DateJoined, BlobUri FROM dbo.Players " +
                    $"WHERE Id LIKE '{authenticatedAspNetUserId}'";
                // Use LIKE instead of = to avoid problems with hyphens - in ids
                var player = cnn.Query<PlayerModel>(sql).FirstOrDefault(); // There should be only 1
                return player;
            }
        }

        // GET api/Player/GetPlayerDateJoined
        [HttpGet]
        [Route("GetPlayerDateJoined")]
        public string GetPlayerDateJoined()
        {
            string authenticatedAspNetUserId = RequestContext.Principal.Identity.GetUserId();
            using (IDbConnection cnn = new ApplicationDbContext().Database.Connection)
            {
                string sql = $"SELECT DateJoined FROM dbo.Players WHERE Id LIKE '{authenticatedAspNetUserId}'";
                DateTime dateJoined = cnn.Query<DateTime>(sql).FirstOrDefault(); // There should be only 1
                return dateJoined.ToShortDateString();
            }
        }

    }
}
