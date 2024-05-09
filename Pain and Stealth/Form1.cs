using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pain_and_Stealth
{
    public partial class Form1 : Form
    {
        public int BulletSpeed;
        public bool Fire;
        public bool IsTraderButton;
        public int EnemySpeed;
        private int damage;
        private int steps;

        private List<Trader> traders;
        private TraderButton traderButton;
        private Bullets _bullet;
        private PictureBox[] _bullets;
        private HealthyBar _healthyBar;
        private AnimationMap _animationMap;
        private AnimationPlayer _animationPlayer;
        private List<AnimationEnimies> _enemySpawn;


        public Form1()
        {
            InitializeComponent();
            SetUp();
            EventsCollection();
        }

        private void SetUp()
        {
            _animationPlayer = new AnimationPlayer();
            _animationMap = new AnimationMap();
            _healthyBar = new HealthyBar();
            traderButton = new TraderButton();
            _bullet = new Bullets();
            BulletSpeed = 10;
            EnemySpeed = 2;

            SetAllImages();
        }

        private void SetChoice()
        {
            var traderAnimation = new Timer
            {
                Enabled = true,
                Interval = 1
            };
            var firstChoiceButton = new Button
            {
                BackgroundImage = Image.FromFile("FirstChoice.png"),
                FlatStyle = FlatStyle.Popup,
                Size = new Size(206, 223),
                Location = new Point(322, 27),
                BackColor = Color.Transparent
            };
            var secondChoiceButton = new Button
            {
                BackgroundImage = Image.FromFile("SecondChoice.png"),
                FlatStyle = FlatStyle.Popup,
                Size = new Size(206, 223),
                Location = new Point(220, 27),
                BackColor = Color.Transparent
            };
            var thirdChoiceButton = new Button
            {
                BackgroundImage = Image.FromFile("ThirdChoice.png"),
                FlatStyle = FlatStyle.Popup,
                Size = new Size(206, 223),
                Location = new Point(440, 27),
                BackColor = Color.Transparent
            };
            var fourthChoiceButton = new Button
            {
                BackgroundImage = Image.FromFile("FourthChoice.png"),
                FlatStyle = FlatStyle.Popup,
                Size = new Size(206, 223),
                Location = new Point(220, 27),
                BackColor = Color.Transparent
            };
            var fifthChoiceButton = new Button
            {
                BackgroundImage = Image.FromFile("FifthChoice.png"),
                FlatStyle = FlatStyle.Popup,
                Size = new Size(206, 223),
                Location = new Point(440, 27),
                BackColor = Color.Transparent
            };
            ButtonClicked(firstChoiceButton, secondChoiceButton, thirdChoiceButton, fourthChoiceButton, fifthChoiceButton);
            SetTrederChoice(firstChoiceButton, secondChoiceButton, thirdChoiceButton, fourthChoiceButton, fifthChoiceButton, traderAnimation);
        }

        private void ButtonClicked(Button first, Button second, Button third, Button fourth, Button fifth)
        {
            first.Click += (sender, args) =>
            {
                Controls.Remove(first);
                _animationPlayer.PressStart = true;
                _animationPlayer.StartPosition = false;
                IsTraderButton = false;
            };

            second.Click += (sender, args) =>
            {
                Controls.Remove(second);
                Controls.Remove(third);
                _animationPlayer.Speed += 1;
                IsTraderButton = false;
            };

            third.Click += (sender, args) =>
            {
                Controls.Remove(second);
                Controls.Remove(third);
                _animationPlayer.PressThirdChoice = true;
                _animationPlayer.PressFourthChoice = false;
                _animationPlayer.PressStart = false;
                BulletSpeed += 1;
                IsTraderButton = false;
            };

            fourth.Click += (sender, args) =>
            {
                Controls.Remove(fifth);
                Controls.Remove(fifth);
                _animationPlayer.PressFourthChoice = true;
                _animationPlayer.PressThirdChoice = false;
                _animationPlayer.PressStart = false;
                _animationPlayer.Speed += 1;
                BulletSpeed += 5;
                IsTraderButton = false;
            };

            fifth.Click += (sender, args) =>
            {
                Controls.Remove(fifth);
                Controls.Remove(fifth);
                BulletSpeed += 3;
                IsTraderButton = false;
            };
        }

        public void SetTrederChoice(Button first, Button second, Button third, Button fourth, Button fifth, Timer trader)
        {
            trader.Tick += (sender, args) =>
            {
                TraderAnimation();
                if (IsTraderButton)
                {
                    switch (_animationPlayer.Id)
                    {
                        case 1:
                            Controls.Add(first);
                            break;
                        case 2:
                            Controls.Add(second);
                            Controls.Add(third);
                            break;
                        case 3:
                            Controls.Add(fourth);
                            Controls.Add(fifth);
                            break;
                    }
                }
            };
        }

        public void EnemyAttackTick(Timer enemy, Timer playerTimer, Timer moveBulletsTimer, Timer enemyRun)
        {
            enemy.Tick += (sender, args) =>
            {
                AnimationAttack();
                steps++;
                if (steps == 18)
                    damage++;
                if (steps > 18)
                    steps = 0;
                else if (damage == 5)
                {
                    playerTimer.Stop();
                    moveBulletsTimer.Stop();
                    enemyRun.Stop();
                    Fire = false;
                }
                _healthyBar.AnimationHealthy(damage);
            };
        }

        public void EnemyRunTick(Timer enemy, Timer enemyAttack)
        {
            enemy.Tick += (sender, args) =>
            {
                if (_enemySpawn.Count != 0)
                {
                    if (_enemySpawn[0].X <= 900)
                    {
                        AnimationRun();
                        _enemySpawn[0].X -= EnemySpeed;
                    }
                    if (_enemySpawn[0].X <= _animationPlayer.X + 55)
                    {
                        enemy.Stop();
                        enemyAttack.Start();
                    }
                    else
                    {
                        steps = 0;
                        enemyAttack.Stop();
                        enemy.Start();
                    }
                }
            };
        }

        public void EnemyDeadTick(Timer enemy)
        {
            enemy.Tick += (sender, args) =>
            {
                if (_enemySpawn.Count != 0)
                    AnimationDead();
            };
        }

        public void PlayerTick(Timer player)
        {
            player.Tick += (sender, args) =>
            {

                if (_enemySpawn.Count != 0)
                {
                    if (_animationPlayer.X + 50 > _enemySpawn[0].X)
                        _animationPlayer.Conflict = true;
                    else
                        _animationPlayer.Conflict = false;
                }
                _animationPlayer.Animation(_animationMap, _enemySpawn,
                    _bullet, traders, traderButton);
                Invalidate();
            };
        }

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
                Enabled= true,
                Interval = 1
            };
            var enemyAttack = new Timer
            {
                Enabled = false,
                Interval = 1
            };

            SetChoice();
            EnemyAttackTick(enemyAttack, playerTimer, moveBulletsTimer, enemyRun);
            EnemyRunTick(enemyRun, enemyAttack);
            EnemyDeadTick(enemyDead);
            PlayerTick(playerTimer);

            Paint += (sender, args) =>
            {
                var g = args.Graphics;

                _animationMap.DrawImage(g);
                DrawTraderImage(g);

                traderButton.DrawButtonImage(g);


                _animationPlayer.DrawImage(g);
                _healthyBar.DrawImage(g);
                if (_enemySpawn.Count != 0)
                    DrawImage(g);
            };

            FormClosing += (sender, args) => Application.Exit();
            moveBulletsTimer.Tick += (sender, args) => BulletBehavior(enemyRun, enemyDead);
            KeyDown += (sender, args) => KeyIsDown(args);
            KeyUp += (sender, args) => KeyIsUp(args);
        }

        private void AnimationRun() => _enemySpawn[0].AnimationRun();
        private void AnimationDead() => _enemySpawn[0].AnimationDead();
        private void AnimationAttack() => _enemySpawn[0].AnimationAttack();
        private void TraderAnimation()
        {
            foreach (var tard in traders)
                tard.Animate();
        }

        public void SetUpForBullets()
        {
            _bullets = new PictureBox[1];
            for (var i = 0; i < _bullets.Length; i++)
            {
                _bullets[i] = new PictureBox();
                _bullets[i].Image = Image.FromFile("Bullet.png");
                _bullets[i].BackColor = Color.Transparent;
                Controls.Add(_bullets[i]);
            }
        }

        public void BulletBehavior(Timer enemyRun, Timer enemyDead)
        {
            for (var i = 0; i < _bullets.Length; i++)
            {
                if (_enemySpawn.Count != 0)
                {
                    if (_bullets[i].Left < _enemySpawn[0].X)
                        _bullets[i].Left += BulletSpeed;
                    if (_bullets[i].Left > 850)
                    {
                        Fire = false;
                        _bullets[i].Left = -10000;
                        _bullets[i].Top = 10000;
                    }
                    if (_bullets[i].Left > _enemySpawn[0].X && Fire)
                    {
                        enemyRun.Stop();
                        if (_enemySpawn[0].anim)
                            enemyDead.Start();
                        _bullets[i].Left = -10000;
                        _enemySpawn[0].Healthy--;
                        Fire = false;
                    }
                    if (!_enemySpawn[0].anim)
                    {
                        _enemySpawn[0].anim = true;
                        enemyDead.Stop();
                        enemyRun.Start();
                    }
                    if (_enemySpawn[0].Healthy == 0)
                        _enemySpawn.Remove(_enemySpawn[0]);
                }
                else if (_bullets[i].Left > 850)
                {
                    Fire = false;
                    _bullets[i].Left = -10000;
                    _bullets[i].Top = 10000;
                }
                else
                    _bullets[i].Left += BulletSpeed;
            }
        }
        

        public void SetAllImages()
        { 
            SetBackgroundImage();
            SetUpForBullets();
            SetPositionEnemy();
            SetPositionTrade();


            if (_enemySpawn.Count != 0)
                SetEnemyImage();
            _animationPlayer.SetPlayerImage();
            _animationMap.SetImagesMaps();
            SetTraderImage();
            traderButton.SetButtonImage();
            _healthyBar.SetIamage();

        }

        public void SetBackgroundImage()
        {
            BackgroundImage = Image.FromFile("StartMap.png");
            BackgroundImageLayout = ImageLayout.Stretch;
            DoubleBuffered = true;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
        }

        public void SetPositionEnemy()
        {
            _enemySpawn = new List<AnimationEnimies>
            {
                new AnimationEnimies { X = 2700,},
                new AnimationEnimies { X = 2800,},
                new AnimationEnimies { X = 2900,},

                new AnimationEnimies { X = 5050,},
                new AnimationEnimies { X = 5150,},
                new AnimationEnimies { X = 5250,},
                new AnimationEnimies { X = 5350,},
                new AnimationEnimies { X = 5450,},
                new AnimationEnimies { X = 5550,},


                new AnimationEnimies { X = 6650,},
                new AnimationEnimies { X = 6750,},
                new AnimationEnimies { X = 6850,},
                new AnimationEnimies { X = 6950,},
                new AnimationEnimies { X = 7050,},
                new AnimationEnimies { X = 7150,},
                new AnimationEnimies { X = 7250,},
                new AnimationEnimies { X = 7350,},
                new AnimationEnimies { X = 7450,},
                new AnimationEnimies { X = 7550,}
            };
        }

        public void SetPositionTrade()
        {
            traders = new List<Trader>
            {
                new Trader {X = 1750, Id = 1},
                new Trader{X = 3400, Id = 2},
                new Trader{X = 6000, Id = 3}
            };
        }

        private void KeyIsDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.D)
                _animationPlayer.goRight = true;
            if (e.KeyCode == Keys.A)
                _animationPlayer.goLeft = true;
            if (e.KeyCode == Keys.F && !(_animationPlayer.goLeft || _animationPlayer.goRight) && !Fire && !_animationPlayer.StartPosition)
            {
                Fire = true;
                for (var i = 0; i < _bullets.Length; i++)
                    _bullets[i].Location = new Point(_animationPlayer.X + 100 + i * 50, _animationPlayer.Y + 50);
            }

            if (!_animationPlayer.IsJump)
            {
                if (e.KeyCode == Keys.Space)
                {
                    _animationPlayer.IsJump = true;
                    _animationPlayer.force = 10;
                }
            }


            if (e.KeyCode == Keys.E && _animationPlayer.IsTrader)
                IsTraderButton = true;
        }

        private void KeyIsUp(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.D)
                _animationPlayer.goRight = false;
            if (e.KeyCode == Keys.A)
                _animationPlayer.goLeft = false;

            if (_animationPlayer.PressStart)
                _animationPlayer.AnimatePlayer(_animationPlayer.playerMovementRight, 0, 0);
            else if (_animationPlayer.PressThirdChoice)
                _animationPlayer.AnimatePlayer(_animationPlayer.playerThirdRight, 0, 0);
            else if (_animationPlayer.PressFourthChoice)
                _animationPlayer.AnimatePlayer(_animationPlayer.playerFourthRight, 0, 0);
            else
                _animationPlayer.AnimatePlayer(_animationPlayer.playerStartRight, 0, 0);
        }

        private void SetTraderImage()
        {
            foreach (var trader in traders)
                trader.SetTraderImage();
        }

        private void DrawTraderImage(Graphics g)
        {
            foreach (var trader in traders)
                trader.DrawImage(g);
        }

        private void SetEnemyImage()
        {
            foreach (var enemy in _enemySpawn)
                enemy.SetEnimiesImage();
        }

        private void DrawImage(Graphics g)
        {
            foreach (var enemy in _enemySpawn)
                enemy.DrawImage(g);
        }
    }
}
