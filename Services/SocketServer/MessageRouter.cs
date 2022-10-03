using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace GameServer.Services.SocketServer
{
    public class MessageRouter
    {
        private static Dictionary<string, ITcpController> Controllers = new();

        public ITcpController? GetController(String message)
        {
            var chunks = message.Split(' ');
            if (chunks.Length == 0)
            {
                return null;
            }

            var controllerName = chunks[0];
            if (!Controllers.ContainsKey(controllerName))
            {
                return null;
            }

            
            return Controllers[controllerName];
        }

        public void RegisterController(string id, ITcpController controller)
        {
            Controllers[id] = controller;
        }
    }
}
