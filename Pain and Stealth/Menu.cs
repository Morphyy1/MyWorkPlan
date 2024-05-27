using System;
using System.Drawing;
using System.Windows.Forms;

using WMPLib;

namespace Pain_and_Stealth
{
    public partial class Menu : Form
    {
        private static Timer SoundTimer;
        private static int TransitionsCount;
        private static WindowsMediaPlayer SfxSound;
        private static WindowsMediaPlayer MenuSong;

        public Menu()
        {
            SetUp();
            InitializeComponent();
            if (TransitionsCount == 0)
            {
                MenuSong = new WindowsMediaPlayer { URL = "Songs\\Menu.mp3" };
                SfxSound = new WindowsMediaPlayer { URL = "Songs\\Sfx.mp3" };
                SfxSound.settings.volume = 40;
                MenuSong.settings.volume = 50;
                SoundTimer = new Timer();
                SoundTimer.Interval = 10;
                SoundTimer.Tick += new EventHandler(SoundRepeat);
                MenuSong.PlayStateChange += new WMPLib._WMPOCXEvents_PlayStateChangeEventHandler(CheckMediaState);
                MenuSong.controls.play();
            }   
        }

        private void SoundRepeat(object s, EventArgs e)
        {
            SoundTimer.Stop();
            MenuSong.controls.play();
        }

        private void CheckMediaState(int state)
        {
            if (state == (int)WMPLib.WMPPlayState.wmppsMediaEnded)
                SoundTimer.Start();
        }

        public void SetUp()
        { 
            BackgroundImage = Image.FromFile("StartMenu.png");
            BackgroundImageLayout = ImageLayout.Stretch;
            var pict = new PictureBox
            {
                Image = Image.FromFile("GameName.png"),
                BackColor = Color.Transparent,
                Location = new Point(115,86),
                Size = new Size(610, 111)
            };
            var startButton = new Button
            {
                Image = Image.FromFile("NewGameButton.png"),
                Size = new Size(270, 50),
                FlatStyle = FlatStyle.Popup,
                BackColor = Color.Transparent,
                Location = new Point(282, 274)
            };
            var trainingButton = new Button
            {
                Image = Image.FromFile("TrainingButton.png"),
                Size = new Size(270, 50),
                FlatStyle = FlatStyle.Popup,
                BackColor = Color.Transparent,
                Location = new Point(282, 355)
            };
            var exitButton = new Button
            {
                Image = Image.FromFile("ExitButton.png"),
                Size = new Size(270, 50),
                FlatStyle = FlatStyle.Popup,
                BackColor = Color.Transparent,
                Location = new Point(282, 436)
            };
            ButtonClicked(startButton, trainingButton, exitButton);

            Controls.Add(pict);
            Controls.Add(startButton);
            Controls.Add(trainingButton);
            Controls.Add(exitButton);

        }

        public void ButtonClicked(Button start, Button training, Button eixt)
        {
            start.Click += (s, e) =>
            {
                Form1 game = new Form1();
                game.Show();
                SfxSound.controls.play();
                Destruction();
                Hide();
            };
            training.Click += (s, e) =>
            {
                var trainingForm = new TrainingForm();
                trainingForm.Show();
                SfxSound.controls.play();
                TransitionsCount = 1;
                Hide();
            };
            eixt.Click += (s, e) =>
            {
                SfxSound.controls.play();
                Application.Exit();
            };
        }

        private void Destruction()
        {
            SoundTimer.Stop();
            MenuSong.controls.stop();
            TransitionsCount = 0;
        }
    }
}
