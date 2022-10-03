namespace GameServer.Services.SocketServer
{
    public class SocketService
    {
        private readonly SocketServer _server = new SocketServer();
        public void InitSockets(int count)
        {
            for (int i = 0; i < count; i++)
            {
                _server.CreateListener();
            }
        }
    }
}
