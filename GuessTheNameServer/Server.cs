using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace GuessTheNameServer
{
    public class Server
    {
        TcpListener _listener;
        List<Player> _players = new List<Player>();
        List<GameRoom> _rooms = new List<GameRoom>();
        Dictionary<string, List<string>> _categories = new Dictionary<string, List<string>>();

         
        public Server()
        {
            _listener = new TcpListener(IPAddress.Any, 5001);
            LoadCategories();
        }


        private void LoadCategories()
        {


            string basePath = AppDomain.CurrentDomain.BaseDirectory;
            string dataFolder = Path.Combine(basePath, "Data"); 

            _categories.Add("Animals", File.ReadAllLines(Path.Combine(dataFolder, "Animals.txt")).ToList());
            _categories.Add("Countries", File.ReadAllLines(Path.Combine(dataFolder, "Countries.txt")).ToList());
            _categories.Add("Movies", File.ReadAllLines(Path.Combine(dataFolder, "Movies.txt")).ToList());
        }


        public void Start()
        {
            _listener.Start();
            Console.WriteLine("Server Started waiting for players");

            while(true)
            {
                TcpClient client=_listener.AcceptTcpClient();
                Console.WriteLine("Client connected");

                Thread clientTh = new Thread(() => HandleClient(client));
                clientTh.Start();
            }
        }

        private void HandleClient(TcpClient client)
        {
            using (NetworkStream stream = client.GetStream())
            using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
            using (StreamWriter writer = new StreamWriter(stream, Encoding.UTF8) { AutoFlush = true })
            {
                Player player = new Player(client, writer);
                _players.Add(player);

                while (true)
                {
                    string message = reader.ReadLine();
                    if (message == null)
                        break;

                    Console.WriteLine($"Received from client: {message}");
                    string[] parts = message.Split(':', 2); 
                    string command = parts[0];
                    string argument = parts.Length > 1 ? parts[1] : string.Empty;

                    switch (command)
                    {
                        case "LOGIN":
                            player.Name = argument;
                            SendAvailableRooms(player);
                            break;

                        case "CREATE_ROOM":
                            if (_categories.ContainsKey(argument))
                            {
                                GameRoom newRoom = new GameRoom(argument, _categories[argument]);
                                _rooms.Add(newRoom);

                                newRoom.Creator = player; 
                                player.Room = newRoom;
                                newRoom.Players.Add(player);

                                //if (!newRoom.Players.Contains(player))
                                //{
                                //    player.Room = newRoom;
                                //    newRoom.Players.Add(player);
                                //}

                                writer.WriteLine($"ROOM_CREATED:{newRoom.RoomId}");
                                _players.ForEach(SendAvailableRooms);
                            }
                            break;

                        case "JOIN_ROOM":
                            GameRoom room = _rooms.Find(r => r.RoomId == argument);

                            if (room != null)
                            {
                                if (room.Creator == null)
                                {
                                    writer.WriteLine("Room is not ready ");
                                    break;
                                }

                                if (room.Players.Count < 2 || room.Creator==player)
                                {
                                    if (!room.Players.Contains(player))
                                    {
                                        room.Players.Add(player);
                                        player.Room = room;

                                       
                                       
                                    }
                                    writer.WriteLine($"JOINED_ROOM:{room.RoomId}");
                                    room.Broadcast($"PLAYER_JOINED:{player.Name}");

                                    _players.ForEach(SendAvailableRooms);

                                    if (room.Players.Count == 2)
                                    {

                                        room.StartGame();
                                    }
                                }
                                else
                                {
                                    writer.WriteLine("room full");
                                }
                            }
                            break;

                        case "GUESS":
                            if (player.Room != null && argument.Length > 0)
                            {
                                char guessedLetter = argument[0];
                                player.Room.ProcessGuess(player, guessedLetter);
                            }
                            break;

                        case "PLAY_AGAIN":
                            if (player.Room != null)
                            {
                                GameRoom room1=player.Room;
                                if (argument == "YES")
                                {
                                    player.Room.Broadcast($"PLAYER_WANTS_RESTART:{player.Name}");
                                    player.Room.RestartGame();
                                }
                                else
                                {
                                   

                                    player.Room.Broadcast($"PLAYER_LEFT:{player.Name}:{room1.RoomId}");
                                    foreach (var p in room1.Players.ToList())
                                    {
                                        p.Room = null;
                                    }
                                    _rooms.Remove(room1);

                                    _players.ForEach (SendAvailableRooms);

                                    
                                }
                                
                            }
                            break;

                        case "JOIN_AS_SPECTATOR":
                            GameRoom specRoom = _rooms.Find(r => r.RoomId == argument);
                            if (specRoom != null)
                            {
                                specRoom.Spectators.Add(player);
                                player.Room = specRoom;
                                writer.WriteLine($"SPECTATOR_JOINED:{specRoom.RoomId}");
                                writer.WriteLine($"WORD:{specRoom.DisplayWord}");
                            }
                            break;

                        case "LEAVE_ROOM":
                            if (player.Room != null)
                            {
                                player.Room.Spectators.Remove(player);
                                _players.Remove(player);
                                player.Room.Broadcast($"SPECTATOR_LEFT:{player.Name}");
                            }
                            break;

                        default:
                            Console.WriteLine($"Unknown command received: {command}");
                            break;
                    }
                }
            }

            Console.WriteLine("Client disconnected");
            _players.RemoveAll(p => p.Client.Equals(client));
        }





        private void SendAvailableRooms(Player player)
        {
            StringBuilder roomList = new StringBuilder("AVAILABLE_ROOMS:");
            foreach (var room in _rooms)
            {
                roomList.Append($"{room.RoomId}:{room.Category}:{room.Players.Count};");
            }
            player.Writer.WriteLine(roomList.ToString());
        }
    }
}





