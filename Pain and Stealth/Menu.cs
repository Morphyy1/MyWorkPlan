using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pain_and_Stealth
{
    public partial class Menu : Form
    {
        public Menu()
        {
            SetUp();
            InitializeComponent();
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
                Hide();
            };
            training.Click += (s, e) =>
            {
                var trainingForm = new TrainingForm();
                trainingForm.Show();
                Hide();
            };
            eixt.Click += (s, e) => Close();
        }
    }
}
