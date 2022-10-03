using GameServer.Controllers.Tcp;

namespace GameServer.Services.SocketServer
{
    public class TcpControllerDependencyProvider
    {
        public List<ITcpController> ProvideDependencies()
        {
            return new List<ITcpController>()
            {
                new SpawnController()
            };
        }
    }
}
