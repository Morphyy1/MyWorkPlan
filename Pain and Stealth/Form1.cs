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
    public partial class Form1 : Form
    {
        private AnimationMap AnimationMap = new AnimationMap();
        private AnimationPlayer AnimationPlayer = new AnimationPlayer();
        private List<AnimationEnimies> list = new List<AnimationEnimies>
        {
            new AnimationEnimies { X = 800},
            new AnimationEnimies { X = 950},
            new AnimationEnimies { X = 1100}
        };
        private AnimationEnimies AnimationEnimies1 = new AnimationEnimies { X = 300};
        private AnimationEnimies AnimationEnimies2 = new AnimationEnimies { X = 500};

        public Form1()
        {
            InitializeComponent();
            SetUp();
            EventsCollection();
        }

        private void SetUp()
        {
            BackgroundImage = Image.FromFile("StartMap.png");
            BackgroundImageLayout = ImageLayout.Stretch;
            DoubleBuffered = true;

            SetEnemyImage();
            AnimationPlayer.SetPlayerImage();
            AnimationMap.SetImagesMaps();
        }

        private void EventsCollection()
        {
            var playerTimer = new System.Windows.Forms.Timer
            {
                Enabled = true,
                Interval = 10
            };
            Paint += (sender, args) =>
            {
                var g = args.Graphics;
                AnimationMap.DrawImage(g);
                DrawImage(g);
                AnimationPlayer.DrawImage(g);
            };
            playerTimer.Tick += (sender, args) =>
            {
                //Animation();
                AnimationPlayer.Animation(AnimationMap, list);
                Invalidate();
            };
            KeyDown += (sender, args) => KeyIsDown(args);
            KeyUp += (sender, args) => KeyIsUp(args);
        }

        private void KeyIsDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.D)
                AnimationPlayer.goRight = true;
            if (e.KeyCode == Keys.A)
                AnimationPlayer.goLeft = true;
            if (!AnimationPlayer.IsJump)
            {
                if (e.KeyCode == Keys.Space)
                {
                    AnimationPlayer.IsJump = true;
                    AnimationPlayer.force = 10;
                }
            }
        }

        private void KeyIsUp(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.D)
                AnimationPlayer.goRight = false;
            if (e.KeyCode == Keys.A)
                AnimationPlayer.goLeft = false;
            AnimationPlayer.AnimatePlayer(AnimationPlayer.playerMovementRight, 0, 0);
        }

        private void SetEnemyImage()
        {
            foreach (var enemy in list)
                enemy.SetEnimiesImage();
        }

        private void DrawImage(Graphics g)
        {
            foreach (var enemy in list)
                enemy.DrawImage(g);
        }

        private void Animation()
        {
            foreach (var enemy in list)
                enemy.Animation();
        }
    }
}
