using GameServer.Services.SocketServer;
using GameServer.Services.WorldService;
using System.Net.Sockets;

namespace GameServer.Controllers.Tcp
{
    public class SpawnController: ITcpController
    {
        private readonly WorldService _worldService = new WorldService();
        public String GetId()
        {
            return "spawn";
        }

        public string Handle(string message, Socket socket)
        {
            var chunks = message.Split(' ');
            if (chunks.Length < 2)
            {
                return "error";
            }

            var key = chunks[1];
            var player = _worldService.GetPlayerByKey(key);
            if (player == null)
            {
                return "error";
            }
            player.Socket = socket;

            _worldService.BroadcastMessage("spawn " + player.User.Id + " " + player.X);
            return "ok";
        }
    }
}
