using System.Drawing;
using System.Windows.Forms;

namespace Pain_and_Stealth
{
    partial class Form1
    {
        private void EventsCollection()
        {
            var playerTimer = new Timer
            {
                Enabled = true,
                Interval = 1
            };
            var moveBulletsTimer = new Timer
            {
                Enabled = true,
                Interval = 1
            };
            var enemyDead = new Timer
            {
                Interval = 1
            };
            var enemyRun = new Timer
            {
                Enabled = true,
                Interval = 1
            };
            var enemyAttack = new Timer
            {
                Enabled = false,
                Interval = 1
            };
            var bossTimer = new Timer
            {
                Enabled = true,
                Interval = 1
            };
            var bossAttack = new Timer
            {
                Enabled = false,
                Interval = 1
            };
            var bossDead = new Timer
            {
                Enabled = false,
                Interval = 1
            };


            var starDown = new Timer { Interval = 1 };
            var starDrop = new Timer { Interval = 1 };
            var explosionStar = new Timer { Interval = 1 };
            var darkTimer = new Timer { Interval = 10000 }; ;

            var youLose = new PictureBox
            {
                Image = Image.FromFile("YouLose.png"),
                Location = new Point(300, 194),
                BackColor = Color.Black,
                Size = new Size(250, 31)
            };
            var newGameButton = new Button
            {
                BackgroundImage = Image.FromFile("NewGamePurple.png"),
                Size = new Size(270, 50),
                Location = new Point(291, 273),
                FlatStyle = FlatStyle.Popup,
                BackColor = Color.Black
            };
            var menuButton = new Button
            {
                BackgroundImage = Image.FromFile("MenuPurple.png"),
                Size = new Size(270, 50),
                FlatStyle = FlatStyle.Popup,
                Location = new Point(291, 344),
                BackColor = Color.Black
            };
            var blackFront = new PictureBox
            {
                BackColor = Color.Black,
                Size = new Size(850, 650),
                Location = new Point(0, 0)
            };

            var finalText = new PictureBox
            {
                Image = Image.FromFile("FinalText.png"),
                BackColor = Color.Black,
                Location = new Point(1000, 1000),
                Size = new Size(486, 289)
            };

            var final = new Timer { Interval = 30 };

            SetStarEvent(darkTimer, starDown, starDrop, explosionStar);
            SetAllEvent(playerTimer, explosionStar, final, enemyAttack, enemyRun, enemyDead, bossTimer,
                bossAttack, bossDead, moveBulletsTimer, menuButton, newGameButton,
                youLose, blackFront, finalText);

            FormClosing += (sender, args) => Application.Exit();
            moveBulletsTimer.Tick += (sender, args) => BulletBehavior(enemyRun, enemyDead, bossTimer, bossAttack, bossDead,
                finalText, blackFront, final);
            KeyDown += (sender, args) => KeyIsDown(args, starDrop, darkTimer);
            KeyUp += (sender, args) => KeyIsUp(args);
        }

        private void MenuButtonClicked(Button menuButton, Button newGameButton)
        {
            menuButton.Click += (s, e) =>
            {
                var menu = new Menu();
                menu.Show();
                TransitionsCount = 0;
                Destroy();
                Hide();
            };
            newGameButton.Click += (s, e) =>
            {
                var newGame = new Form1();
                newGame.Show();
                Destroy();
                TransitionsCount = 1;
                Hide();
            };
        }

        private void FinalEventTick(Timer final, Timer moveBulletsTimer, Timer playerTimer, PictureBox finalText)
        {
            var i = 650;
            final.Tick += (s, e) =>
            {
                i--;
                finalText.Location = new Point(182, i);
                moveBulletsTimer.Stop();
                playerTimer.Stop();
                if (i == 210)
                {
                    var menu = new Menu();
                    menu.Show();
                    TransitionsCount = 0;
                    Destroy();
                    Hide();
                }
            };
        }

        private void Destroy()
        {
            MainSound.controls.stop();
            ShootSound.controls.stop();
        }

        private void SetAllEvent(Timer playerTimer, Timer explosionStar, Timer final, Timer enemyAttack, Timer enemyRun,
            Timer enemyDead, Timer bossTimer, Timer bossAttack, Timer bossDead, Timer moveBulletsTimer, Button menuButton, Button newGameButton,
            PictureBox youLose, PictureBox blackFront, PictureBox finalText)
        {
            SetChoice();
            FinalEventTick(final, moveBulletsTimer, playerTimer, finalText);
            ExplosionEventTick(explosionStar, final, bossTimer, bossAttack, bossDead, finalText, blackFront);
            EnemyAttackTick(enemyAttack, playerTimer, moveBulletsTimer, enemyRun,
                menuButton, newGameButton, youLose, blackFront);
            EnemyRunTick(enemyRun, enemyAttack);
            EnemyDeadTick(enemyDead);
            PlayerTick(playerTimer);
            BossTick(bossTimer, bossAttack);
            BossAttack(bossAttack, bossTimer, playerTimer, moveBulletsTimer, enemyRun,
                menuButton, newGameButton, youLose, blackFront);
            BossDead(bossDead);
            MenuButtonClicked(menuButton, newGameButton);
            FormPaint();
        }

        private void FormPaint()
        {
            Paint += (sender, args) =>
            {
                var g = args.Graphics;

                _animationMap.DrawImage(g);
                DrawTraderImage(g);

                _traderButton.DrawButtonImage(g);
                _animationPlayer.DrawImage(g);
                _boss.DrawImage(g);
                _healthyBar.DrawImage(g);
                if (IsFinalRound)
                    _bossHealthyBar.DrawImage(g);
                if (_enemySpawn.Count != 0)
                    DrawImage(g);
                if (IsSuper)
                    _superStar.DrawImage(g);
                if (IsMap)
                    _explosionStar.DrawImage(g);
                if (_darkSpace)
                    _superButton.DrawButtonImage(g);
            };
        }
    }
}
