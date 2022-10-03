using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Net.Sockets;

namespace GameServer.Services.SocketServer
{
    public interface ITcpController
    {
        public String Handle(String message, Socket socket);
        public String GetId();
    }
}
