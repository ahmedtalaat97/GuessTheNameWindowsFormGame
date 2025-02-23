namespace Client
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            loginPanel = new Panel();
            connectButton = new Button();
            nameTextBox = new TextBox();
            nameLabel = new Label();
            roomPanel = new Panel();
            label2 = new Label();
            label1 = new Label();
            categoryComboBox = new ComboBox();
            spectateRoomButton = new Button();
            joinRoomButton = new Button();
            createRoomButton = new Button();
            roomListBox = new ListBox();
            gamePanel = new Panel();
            spectatorTxt = new Label();
            leaveSpectator = new Button();
            letterButtonsPanel = new FlowLayoutPanel();
            turnLabel = new Label();
            wordLabel = new Label();
            loginPanel.SuspendLayout();
            roomPanel.SuspendLayout();
            gamePanel.SuspendLayout();
            SuspendLayout();
            // 
            // loginPanel
            // 
            loginPanel.BackColor = Color.Transparent;
            loginPanel.Controls.Add(connectButton);
            loginPanel.Controls.Add(nameTextBox);
            loginPanel.Controls.Add(nameLabel);
            loginPanel.Location = new Point(12, 616);
            loginPanel.Name = "loginPanel";
            loginPanel.Size = new Size(579, 81);
            loginPanel.TabIndex = 0;
            // 
            // connectButton
            // 
            connectButton.Font = new Font("Showcard Gothic", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            connectButton.Location = new Point(289, 28);
            connectButton.Name = "connectButton";
            connectButton.Size = new Size(94, 29);
            connectButton.TabIndex = 2;
            connectButton.Text = "Connect";
            connectButton.UseVisualStyleBackColor = true;
            connectButton.Click += connectButton_Click;
            // 
            // nameTextBox
            // 
            nameTextBox.Font = new Font("Showcard Gothic", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            nameTextBox.Location = new Point(97, 28);
            nameTextBox.Name = "nameTextBox";
            nameTextBox.Size = new Size(168, 26);
            nameTextBox.TabIndex = 1;
            // 
            // nameLabel
            // 
            nameLabel.AutoSize = true;
            nameLabel.Font = new Font("Showcard Gothic", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            nameLabel.Location = new Point(18, 28);
            nameLabel.Name = "nameLabel";
            nameLabel.Size = new Size(73, 29);
            nameLabel.TabIndex = 0;
            nameLabel.Text = "Name\r\n";
            // 
            // roomPanel
            // 
            roomPanel.BackColor = Color.Transparent;
            roomPanel.Controls.Add(label2);
            roomPanel.Controls.Add(label1);
            roomPanel.Controls.Add(categoryComboBox);
            roomPanel.Controls.Add(spectateRoomButton);
            roomPanel.Controls.Add(joinRoomButton);
            roomPanel.Controls.Add(createRoomButton);
            roomPanel.Controls.Add(roomListBox);
            roomPanel.Location = new Point(12, 12);
            roomPanel.Name = "roomPanel";
            roomPanel.Size = new Size(612, 505);
            roomPanel.TabIndex = 1;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Showcard Gothic", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.Location = new Point(10, 36);
            label2.Name = "label2";
            label2.Size = new Size(210, 37);
            label2.TabIndex = 7;
            label2.Text = "Room Lobby";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Showcard Gothic", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.Location = new Point(413, 125);
            label1.Name = "label1";
            label1.Size = new Size(135, 26);
            label1.TabIndex = 6;
            label1.Text = "Categories ";
            // 
            // categoryComboBox
            // 
            categoryComboBox.BackColor = SystemColors.ActiveCaption;
            categoryComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            categoryComboBox.Font = new Font("Showcard Gothic", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            categoryComboBox.ForeColor = Color.Gray;
            categoryComboBox.FormattingEnabled = true;
            categoryComboBox.Items.AddRange(new object[] { "Animals", "Movies", "Countries" });
            categoryComboBox.Location = new Point(413, 160);
            categoryComboBox.Name = "categoryComboBox";
            categoryComboBox.Size = new Size(151, 26);
            categoryComboBox.TabIndex = 4;
            // 
            // spectateRoomButton
            // 
            spectateRoomButton.Font = new Font("Showcard Gothic", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            spectateRoomButton.Location = new Point(277, 241);
            spectateRoomButton.Name = "spectateRoomButton";
            spectateRoomButton.Size = new Size(94, 26);
            spectateRoomButton.TabIndex = 5;
            spectateRoomButton.Text = "Watch";
            spectateRoomButton.Click += spectateRoomButton_Click;
            // 
            // joinRoomButton
            // 
            joinRoomButton.Font = new Font("Showcard Gothic", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            joinRoomButton.Location = new Point(277, 335);
            joinRoomButton.Name = "joinRoomButton";
            joinRoomButton.Size = new Size(94, 29);
            joinRoomButton.TabIndex = 2;
            joinRoomButton.Text = "Join Room";
            joinRoomButton.UseVisualStyleBackColor = true;
            joinRoomButton.Click += joinRoomButton_Click;
            // 
            // createRoomButton
            // 
            createRoomButton.BackColor = Color.Transparent;
            createRoomButton.Font = new Font("Showcard Gothic", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            createRoomButton.Location = new Point(277, 127);
            createRoomButton.Name = "createRoomButton";
            createRoomButton.Size = new Size(94, 29);
            createRoomButton.TabIndex = 1;
            createRoomButton.Text = "Create";
            createRoomButton.UseVisualStyleBackColor = false;
            createRoomButton.Click += createRoomButton_Click;
            // 
            // roomListBox
            // 
            roomListBox.Font = new Font("Showcard Gothic", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            roomListBox.FormattingEnabled = true;
            roomListBox.ItemHeight = 18;
            roomListBox.Location = new Point(10, 126);
            roomListBox.Name = "roomListBox";
            roomListBox.Size = new Size(248, 238);
            roomListBox.TabIndex = 0;
            // 
            // gamePanel
            // 
            gamePanel.AutoSize = true;
            gamePanel.BackColor = Color.Transparent;
            gamePanel.Controls.Add(spectatorTxt);
            gamePanel.Controls.Add(leaveSpectator);
            gamePanel.Controls.Add(letterButtonsPanel);
            gamePanel.Controls.Add(turnLabel);
            gamePanel.Controls.Add(wordLabel);
            gamePanel.Location = new Point(12, 12);
            gamePanel.Name = "gamePanel";
            gamePanel.Size = new Size(899, 775);
            gamePanel.TabIndex = 2;
            // 
            // spectatorTxt
            // 
            spectatorTxt.AutoSize = true;
            spectatorTxt.Font = new Font("Showcard Gothic", 24F, FontStyle.Regular, GraphicsUnit.Point, 0);
            spectatorTxt.Location = new Point(32, 47);
            spectatorTxt.Name = "spectatorTxt";
            spectatorTxt.Size = new Size(248, 50);
            spectatorTxt.TabIndex = 4;
            spectatorTxt.Text = "Spectator";
            // 
            // leaveSpectator
            // 
            leaveSpectator.BackColor = Color.White;
            leaveSpectator.Font = new Font("Showcard Gothic", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            leaveSpectator.Location = new Point(757, 508);
            leaveSpectator.Name = "leaveSpectator";
            leaveSpectator.Size = new Size(114, 23);
            leaveSpectator.TabIndex = 3;
            leaveSpectator.Text = "Leave";
            leaveSpectator.UseVisualStyleBackColor = false;
            leaveSpectator.Click += leaveSpectator_Click;
            // 
            // letterButtonsPanel
            // 
            letterButtonsPanel.Location = new Point(136, 302);
            letterButtonsPanel.Name = "letterButtonsPanel";
            letterButtonsPanel.Size = new Size(529, 229);
            letterButtonsPanel.TabIndex = 2;
            // 
            // turnLabel
            // 
            turnLabel.AutoSize = true;
            turnLabel.BackColor = Color.Transparent;
            turnLabel.Font = new Font("Showcard Gothic", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            turnLabel.Location = new Point(136, 241);
            turnLabel.Name = "turnLabel";
            turnLabel.Size = new Size(418, 37);
            turnLabel.TabIndex = 1;
            turnLabel.Text = "Waiting for your turn...";
            // 
            // wordLabel
            // 
            wordLabel.AutoSize = true;
            wordLabel.Font = new Font("Showcard Gothic", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            wordLabel.Location = new Point(383, 158);
            wordLabel.Name = "wordLabel";
            wordLabel.Size = new Size(140, 37);
            wordLabel.TabIndex = 0;
            wordLabel.Text = "\"_ _ _ _ _\"";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            BackgroundImage = Properties.Resources.toji;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(997, 775);
            Controls.Add(gamePanel);
            Controls.Add(loginPanel);
            Controls.Add(roomPanel);
            Name = "Form1";
            Text = "Form1";
            loginPanel.ResumeLayout(false);
            loginPanel.PerformLayout();
            roomPanel.ResumeLayout(false);
            roomPanel.PerformLayout();
            gamePanel.ResumeLayout(false);
            gamePanel.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel loginPanel;
        private TextBox nameTextBox;
        private Label nameLabel;
        private Button connectButton;
        private Panel roomPanel;
        private Button spectateRoomButton;
        private Button joinRoomButton;
        private Button createRoomButton;
        private ListBox roomListBox;
        private Panel gamePanel;
        private Label wordLabel;
        private FlowLayoutPanel letterButtonsPanel;
        private Label turnLabel;
        private ComboBox categoryComboBox;
        private Button leaveSpectator;
        private Label label1;
        private Label label2;
        private Label spectatorTxt;
    }
}
