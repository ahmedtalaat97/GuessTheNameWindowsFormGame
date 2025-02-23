using System.Net.Sockets;

namespace Client
{
    public partial class Form1 : Form
    {
        TcpClient _client;
        NetworkStream _stream;
        StreamReader _reader;
        StreamWriter _writer;
        bool _isMyTurn = false;
        bool _isSpectator = false;
        public Form1()
        {
            InitializeComponent();
            gamePanel.Visible = false;
            roomPanel.Visible = false;
            leaveSpectator.Visible = false;
            spectatorTxt.Visible = false;



            for (char c = 'A'; c <= 'Z'; c++)
            {
                Button button = new Button
                {
                    Text = c.ToString(),
                    Width = 50,
                    Height = 50,
                    BackColor = Color.LightGray,
                    Font= new Font("Showcard Gothic", 9F, FontStyle.Regular, GraphicsUnit.Point, 0),
                    Enabled = false
                };
                button.Click += LetterButton_Click;
                letterButtonsPanel.Controls.Add(button);
            }
        }


        private void LetterButton_Click(object sender, EventArgs e)
        {
            if (_isMyTurn)
            {
                Button button = (Button)sender;
                _writer.WriteLine($"GUESS:{button.Text}");
                button.Enabled = false;
            }
        }

        private void connectButton_Click(object sender, EventArgs e)
        {
            try
            {
                _client = new TcpClient("127.0.0.1", 5001);
                _stream = _client.GetStream();
                _reader = new StreamReader(_stream, System.Text.Encoding.UTF8);
                _writer = new StreamWriter(_stream, System.Text.Encoding.UTF8) { AutoFlush = true };

                _writer.WriteLine($"LOGIN:{nameTextBox.Text}");
                connectButton.Enabled = false;
                nameTextBox.Enabled = false;

                Thread reciveTh = new Thread(ReceiveMessages);
                reciveTh.Start();
                loginPanel.Visible = false;
                roomPanel.Visible = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to connect to server: {ex.Message}");
            }
        }







        private void ReceiveMessages()
        {
            try
            {
                while (true)
                {
                    string message = _reader.ReadLine();
                    if (message == null)
                        break;

                    this.Invoke((MethodInvoker)delegate
                    {
                        ProcessServerMessage(message);
                    });

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error {ex.Message}");
            }
        }


        private void createRoomButton_Click(object sender, EventArgs e)
        {
            if (categoryComboBox.SelectedIndex == -1)
            {
                MessageBox.Show("please select a category");
                return;
            }
            string category = categoryComboBox.SelectedItem.ToString();
            _writer.WriteLine($"CREATE_ROOM:{category}");
        }




        private void ProcessServerMessage(string message)
        {
            if (message.StartsWith("ROOM_CREATED"))
            {
                string roomId = message.Split(":")[1];
                roomListBox.Items.Add(roomId);
                MessageBox.Show($"room created with id{roomId}");
            }
            else if (message.StartsWith("AVAILABLE_ROOMS:"))
            {
                roomListBox.Items.Clear();
                string[] rooms = message.Substring(16).Split(";");
                foreach (string room in rooms)
                {
                    if (!string.IsNullOrWhiteSpace(room))
                    {
                        string[] roomDet = room.Split(":");//=>[roomid][:]
                        string roomId = roomDet[0];

                        roomListBox.Items.Add($"{roomId}");

                    }
                }

            }
            else if (message.StartsWith("JOINED_ROOM:"))
            {
                roomPanel.Visible = false;
                gamePanel.Visible = true;
                ResetGame();
                MessageBox.Show("You have joined . waiting for opp");
            }
            else if (message.StartsWith("PLAYER_JOINED:"))
            {
                MessageBox.Show($"{message.Split(':')[1]} joined");
            }
            else if (message.StartsWith("GAME_STARTED"))
            {
                

                foreach (Button btn in letterButtonsPanel.Controls.OfType<Button>())
                {
                    btn.Enabled = true;
                }
            }
            if (message == "YOUR_TURN")
            {
                _isMyTurn = true;
                turnLabel.Text =    ($"Your Turn: {nameTextBox.Text}");
                turnLabel.ForeColor = Color.Green;
                
                foreach (Control ctrl in letterButtonsPanel.Controls)
                {
                    if (ctrl is Button btn && btn.BackColor == Color.LightGray)
                        btn.Enabled = true;
                }
            }
            else if (message == "OPPONENT_TURN")
            {
                _isMyTurn = false;
                turnLabel.Text = " Opponent Turn Please Wait";
                turnLabel.ForeColor = Color.Red;
                foreach (Control ctrl in letterButtonsPanel.Controls)
                {
                    if (ctrl is Button btn)
                        btn.Enabled = false;
                }
            }
            else if (message.StartsWith("GUESS_RESULT:"))
            {
                string[] parts = message.Split(':');
                char guessedLetter = parts[1][0];
                bool correct = (parts[2] == "CORRECT");
                MessageBox.Show(correct ? $"{guessedLetter} is correct!" : $"{guessedLetter} is incorrect!");
                foreach (Control ctrl in letterButtonsPanel.Controls)
                {
                    if (ctrl is Button btn && btn.Text == guessedLetter.ToString())
                    {
                        btn.Enabled = false;
                        btn.BackColor = correct ? Color.Green : Color.Red;
                    }
                }
            }
            else if (message.StartsWith("WORD:"))
            {
                wordLabel.Text = message.Substring(5);
            }
            else if (message.StartsWith("GAME_OVER:"))
            {
                string winner = message.Split(':')[1];
                string loser = nameTextBox.Text == winner ? "Opponent" : nameTextBox.Text;

                DialogResult result = MessageBox.Show(
                    winner == nameTextBox.Text ?
                    $"Congratulations {winner}! You won! Do you want to play again?" :
                    $"Sorry {loser}, you lost! Do you want to play again?",
                    "Game Over",
                    MessageBoxButtons.YesNo
                );

                _writer.WriteLine($"PLAY_AGAIN:{(result == DialogResult.Yes ? "YES" : "NO")}");
            }
            else if (message.StartsWith("PLAYER_WANTS_RESTART"))
            {
                MessageBox.Show($"{message.Split(':')[1]} wants to restart!");
            }
            else if (message.StartsWith("PLAYER_LEFT"))
            {
                MessageBox.Show($"{message.Split(':')[1]} left the game.");


               
                    string roomToRemove = message.Split(":")[2];
                    for (int i = 0; i < roomListBox.Items.Count; i++)
                    {
                        if (roomListBox.Items[i].ToString().StartsWith(roomToRemove))
                        {
                            roomListBox.Items.RemoveAt(i);
                            break;
                        }
                    }
                

                roomPanel.Visible = true;
                gamePanel.Visible = false;
            }
            else if (message == "GAME_RESTARTED")
            {
                ResetGame();
            }
            else if (message.StartsWith("SPECTATOR_JOINED"))
            {
                MessageBox.Show("You are now spectating this game.");
                _isSpectator = true;
                leaveSpectator.Visible = true;
                spectatorTxt.Visible = true;
                roomPanel.Visible = false;
                gamePanel.Visible = true;
                letterButtonsPanel.Enabled = false;
            }
            else if (message.StartsWith("SPECTATOR_LEFT:"))
            {
                MessageBox.Show($"{message.Split(':')[1]} left as spectator");
            }
            else if (message.StartsWith("room full"))
            {
                MessageBox.Show("room Full");
                return;
            }


        }


        private void ResetGame()
        {
             spectatorTxt.Visible = false;

            foreach (Button button in letterButtonsPanel.Controls)
            {
                button.Enabled = true;
                button.BackColor = Color.LightGray;
            }
            wordLabel.Text = new string('_', wordLabel.Text.Length);
        }
        private void joinRoomButton_Click(object sender, EventArgs e)
        {
            if (roomListBox.SelectedItem == null)
            {
                MessageBox.Show("please select a room first");
                return;
            }
            string roomId = roomListBox.SelectedItem.ToString();
            _writer.WriteLine($"JOIN_ROOM:{roomId}");
        }

        private void spectateRoomButton_Click(object sender, EventArgs e)
        {
            if (roomListBox.SelectedItem == null)
            {
                MessageBox.Show("Please select a room first");
                return;
            }
            string roomId = roomListBox.SelectedItem.ToString();
            _writer.WriteLine($"JOIN_AS_SPECTATOR:{roomId}");
            _isSpectator = true;
        }

        private void leaveSpectator_Click(object sender, EventArgs e)
        {
            _writer.WriteLine("LEAVE_ROOM");
            if(_isSpectator)
            {
                _isSpectator = false;
                leaveSpectator.Visible = false;
                roomPanel.Visible = true;
                gamePanel.Visible = false;
            }
           
        }
    }
}
