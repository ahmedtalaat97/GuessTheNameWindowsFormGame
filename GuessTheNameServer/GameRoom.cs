using System.Text;

namespace GuessTheNameServer
{
    public class GameRoom
    {
        public string RoomId { get; } = Guid.NewGuid().ToString();
        public string Category { get; }
        public List<string> Words { get; }
        public Player Creator { get; set; }
        public string HiddenWord { get; private set; }
        public string DisplayWord { get; private set; }
        public List<Player> Players { get; } = new List<Player>();
        public List<Player> Spectators { get; } = new List<Player>();
        public int CurrentPlayerIndex { get; set; } = 0;

        public GameRoom(string category, List<string> words)
        {
            Category = category;
            Words = words;
            HiddenWord = words[new Random().Next(words.Count)];
            DisplayWord = new string('_', HiddenWord.Length);
        }

        public void StartGame()
        {
            Broadcast("GAME_STARTED");
            Players[0].Writer.WriteLine("YOUR_TURN");
            Players[1].Writer.WriteLine("OPPONENT_TURN");
        }

        public void ProcessGuess(Player player, char guessedLetter)
        {
            bool correctGuess = false;
            StringBuilder newDisplayWord = new StringBuilder(DisplayWord);

            for (int i = 0; i < HiddenWord.Length; i++)
            {
                if (char.ToUpper(HiddenWord[i]) == char.ToUpper(guessedLetter))
                {
                    newDisplayWord[i] = HiddenWord[i];
                    correctGuess = true;
                }
            }

            DisplayWord = newDisplayWord.ToString();
            Broadcast($"GUESS_RESULT:{guessedLetter}:{(correctGuess ? "CORRECT" : "INCORRECT")}");
            Broadcast($"WORD:{DisplayWord}");

            if (IsGameOver())
            {
                Broadcast($"GAME_OVER:{Players[CurrentPlayerIndex].Name}");
                SaveGameResult();
            }
            else
            {
                SwitchTurn();
            }
        }



        public void RestartGame()
        {
             HiddenWord = Words[new Random().Next(Words.Count)];
            DisplayWord=new string('_', HiddenWord.Length);
            CurrentPlayerIndex = 0;
            Broadcast("GAME_RESTARTED");
            Players[0].Writer.WriteLine("YOUR_TURN");
            if(Players.Count > 1)
            {
                Players[1].Writer.WriteLine("OPPONENT_TURN");

            }
        }

        
        public bool IsGameOver()
        {
            return !DisplayWord.Contains('_');
        }

        public void SwitchTurn()
        {
            CurrentPlayerIndex = (CurrentPlayerIndex + 1) % Players.Count;
            Players[CurrentPlayerIndex].Writer.WriteLine("YOUR_TURN");
            Players[(CurrentPlayerIndex + 1) % 2].Writer.WriteLine("OPPONENT_TURN");
        }

        public void Broadcast(string message)
        {
            foreach (var player in Players.Concat(Spectators))
            {
                player.Writer.WriteLine(message);
            }
        }

        private void SaveGameResult()
        {
            string basePath = AppDomain.CurrentDomain.BaseDirectory; 
            string dataFolder = Path.Combine(basePath, "Data"); 
            string filePath = Path.Combine(dataFolder, "GameResults.txt"); 


            if (!Directory.Exists(dataFolder))
            {
                Directory.CreateDirectory(dataFolder);
            }

            string result = $"Player1 name \"{Players[0].Name}\", Player2 name \"{Players[1].Name}\"";
            File.AppendAllText(filePath, result + Environment.NewLine);
        }
    }
}