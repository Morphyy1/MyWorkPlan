using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pain_and_Stealth
{
    public partial class TrainingForm : Form
    {
        public TrainingForm()
        {
            InitializeComponent();
            SetUp();
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
                Hide();
            };

            FormClosing += (s, e) => Application.Exit();

            Controls.Add(trainingFirst);
            Controls.Add(trainingSecond);
            Controls.Add(trainingThird);
            Controls.Add(backButton);
        }
    }
}
