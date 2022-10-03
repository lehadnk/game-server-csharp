using GameServer.Persistence;
using GameServer.Persistence.Models;
using System.Text;

namespace GameServer.Services.WorldService
{
    public class WorldService
    {
        public static World World = new World();

        public Player Login(User user)
        {
            var player = new Player();
            player.User = user;
            player.X = 0;
            player.Key = user.Id + "";

            World.Players.Add(user.Id, player);

            return player;
        }

        public Dictionary<int, Player> GetPlayerList()
        {
            return World.Players;
        }

        public Player? GetPlayerByKey(string key)
        {
            foreach (var player in World.Players)
            {
                if (player.Value.Key == key)
                {
                    return player.Value;
                }
            }

            return null;
        }

        public void BroadcastMessage(string message)
        {
            foreach (var player in World.Players)
            {
                var echoBytes = Encoding.UTF8.GetBytes(message + "<|EOM|>");
                if (player.Value.Socket == null)
                {
                    continue;
                }

                player.Value.Socket.SendAsync(echoBytes, 0);
            }
        }
    }
}
