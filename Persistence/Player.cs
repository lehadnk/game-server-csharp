using GameServer.Persistence.Models;
using System.Net.Sockets;

namespace GameServer.Persistence
{
    public class Player
    {
        public double X { get; set; }
        public User User { get; set; }
        public string Key { get; set; }
        public Socket Socket { get; set; }
    }
}
