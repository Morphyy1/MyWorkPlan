using System.Drawing;
using System.Windows.Forms;
using WMPLib;

namespace Pain_and_Stealth
{
    public partial class TrainingForm : Form
    {
        private static WindowsMediaPlayer SfxSound;
        public TrainingForm()
        {
            InitializeComponent();
            SetUp();

            SfxSound = new WindowsMediaPlayer { URL = "Songs\\Sfx.mp3" };
            SfxSound.settings.volume = 40;
        }

        public void SetUp()
        {
            BackgroundImage = Image.FromFile("StartMenu.png");
            BackgroundImageLayout = ImageLayout.Stretch;

            var trainingFirst = new PictureBox
            {
                Image = Image.FromFile("Training1.png"),
                BackColor = Color.Transparent,
                Size = new Size(206, 358),
                Location = new Point(87, 123)
            };

            var trainingSecond = new PictureBox
            {
                Image = Image.FromFile("Training2.png"),
                BackColor = Color.Transparent,
                Size = new Size(206, 358),
                Location = new Point(327, 123)
            };

            var trainingThird = new PictureBox
            {
                Image = Image.FromFile("Training3.png"),
                BackColor = Color.Transparent,
                Size = new Size(206, 358),
                Location = new Point(560, 123)
            };

            var backButton = new Button
            {
                BackgroundImage = Image.FromFile("BackButton.png"),
                FlatStyle = FlatStyle.Popup,
                Location = new Point(661, 27),
                Size = new Size(152, 45),
                BackColor = Color.Transparent
            };

            backButton.Click += (s, e) =>
            {
                var menu = new Menu();
                menu.Show();
                SfxSound.controls.play();
                Hide();
            };

            FormClosing += (s, e) => Application.Exit();

            SetControls(trainingFirst, trainingSecond, trainingThird, backButton);
        }

        private void SetControls(PictureBox First, PictureBox Second, PictureBox Third, Button back)
        {
            Controls.Add(First);
            Controls.Add(Second);
            Controls.Add(Third);
            Controls.Add(back);
        }
    }
}
