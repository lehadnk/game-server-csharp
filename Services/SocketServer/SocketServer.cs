using System.Net;
using System.Net.Sockets;
using System.Text;

namespace GameServer.Services.SocketServer
{
    public class SocketServer
    {
        private readonly Socket _listener;
        private readonly MessageRouter _messsageRouter = new MessageRouter();
        
        public SocketServer()
        {
            IPAddress ipAddress = IPAddress.Parse("127.0.0.1");
            IPEndPoint ipEndpoint = new(ipAddress, 11000);
            _listener = new(
                ipEndpoint.AddressFamily,
                SocketType.Stream,
                ProtocolType.Tcp
            );

            _listener.Bind(ipEndpoint);

            var dependencyProvider = new TcpControllerDependencyProvider();
            var controllers = dependencyProvider.ProvideDependencies();

            foreach (var controller in controllers)
            {
                _messsageRouter.RegisterController(controller.GetId(), controller);
            }
            
        }

        async public void CreateListener()
        {
            _listener.Listen(100);
            var handler = await _listener.AcceptAsync();

            while (true)
            {
                Thread.Sleep(500);

                var buffer = new byte[1024];
                var received = await handler.ReceiveAsync(buffer, SocketFlags.None);
                var message = Encoding.UTF8.GetString(buffer, 0, received);

                var eom = "EOM";
                if (message.IndexOf(eom) > -1)
                {
                    message = message.Replace(eom, "");
                    Console.WriteLine($"Socket server received message: \"{message}\"");

                    var controller = _messsageRouter.GetController(message);
                    if (controller != null)
                    {
                        var responseMessage = controller.Handle(message, handler);
                        var responseEchoBytes = Encoding.UTF8.GetBytes(responseMessage + "<|EOM|>");
                        await handler.SendAsync(responseEchoBytes, 0);
                    }

                    var ackMessage = "<|ACK|>";
                    var echoBytes = Encoding.UTF8.GetBytes(ackMessage);
                    await handler.SendAsync(echoBytes, 0);
                    Console.WriteLine($"Socket server sent acknowledgment: \"{ackMessage}\"");
                }
            }
        }
    }
}
