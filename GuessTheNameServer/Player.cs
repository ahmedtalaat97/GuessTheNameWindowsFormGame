using System.Net.Sockets;

namespace GuessTheNameServer
{
    public class Player
    {
        public TcpClient Client { get; }
        public StreamWriter Writer { get; }
        public string Name { get; set; }
        public GameRoom Room { get; set; }

        public Guid PlayerId { get; } = Guid.NewGuid();

        public Player(TcpClient client, StreamWriter writer)
        {
            Client = client;
            Writer = writer;
        }
    }
}