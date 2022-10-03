using GameServer.Controllers.Requests;
using GameServer.Dto;
using GameServer.Framework;
using GameServer.Persistence;
using GameServer.Services.WorldService;
using Microsoft.AspNetCore.Mvc;

namespace GameServer.Controllers.Http
{
    [ApiController]
    [Route("authorization")]
    public class AuthorizationController
    {
        WorldService WorldService = new WorldService();

        [HttpPost("authorize")]
        public Response Authorize(AuthorizationRequest request)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var user = db.Users.SingleOrDefault(u => u.Username == request.username && u.Password == request.password);

                if (user == null)
                {
                    return new Response()
                    {
                        isSuccess = false
                    };
                }

                var player = WorldService.Login(user);
                var playerList = WorldService.GetPlayerList();
                var resultPlayerList = new List<Player>();
                foreach (KeyValuePair<int, Player> p in playerList)
                {
                    resultPlayerList.Add(p.Value);
                }

                return new Response()
                {
                    isSuccess = true,
                    data = new Dictionary<string, object>() {
                        {"player", player },
                        { "playerList", resultPlayerList }
                    }
                };
            }
        }
    }
}
