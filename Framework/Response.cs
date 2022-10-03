namespace GameServer.Framework
{
    public class Response
    {
        public bool isSuccess { get; set; }
        public string? errorMessage { get; set; } = null;
        public object? data { get; set; } = null;
    }
}
