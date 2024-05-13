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
        private bool firstButtonClick;
        private bool secondhButtonClick;
        private bool thirdButtonClick;
        private bool fourthButtonClick;
        private bool fifthButtonClick;

        private int BulletSpeed;
        private bool Fire;
        private bool IsTraderButton;
        private bool IsFining;
        private int EnemySpeed;
        private int BossSpeed;
        private int damage;
        private int Enemysteps;
        private int Bosssteps;
        private int EnemyLevelTwo;
        private int EnemyLevelThree;

        private List<Trader> _traders;
        private Boss _boss;
        private TraderButton _traderButton;
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
            _traderButton = new TraderButton();
            _bullet = new Bullets();
            _boss = new Boss();
            SetAllImages();
            EnemyLevelTwo = _enemySpawn.Count - 3;
            EnemyLevelThree = EnemyLevelTwo - 6;
            BulletSpeed = 10;
            EnemySpeed = 2;
            BossSpeed = 4;
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
                firstButtonClick = true;
                _animationPlayer.PressStart = true;
                _animationPlayer.StartPosition = false;
                IsTraderButton = false;
            };

            second.Click += (sender, args) =>
            {
                Controls.Remove(second);
                Controls.Remove(third);
                secondhButtonClick = true;
                _animationPlayer.slowDowmFrameRate -= 1;
                _animationPlayer.Speed += 1;
                IsTraderButton = false;
            };

            third.Click += (sender, args) =>
            {
                Controls.Remove(second);
                Controls.Remove(third);
                thirdButtonClick = true;
                _animationPlayer.PressThirdChoice = true;
                _animationPlayer.PressFourthChoice = false;
                _animationPlayer.PressStart = false;
                BulletSpeed += 1;
                IsTraderButton = false;
            };

            fourth.Click += (sender, args) =>
            {
                Controls.Remove(fifth);
                Controls.Remove(fourth);
                fourthButtonClick = true;
                _animationPlayer.PressFourthChoice = true;
                _animationPlayer.PressThirdChoice = false;
                _animationPlayer.PressStart = false;
                _animationPlayer.slowDowmFrameRate -= 1;
                _animationPlayer.Speed += 1;
                BulletSpeed += 5;
                IsTraderButton = false;
            };

            fifth.Click += (sender, args) =>
            {
                Controls.Remove(fifth);
                Controls.Remove(fourth);
                fifthButtonClick = true;
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
                            if (!firstButtonClick)
                                Controls.Add(first);
                            else
                                IsTraderButton = false;
                            break;
                        case 2:
                            if (!secondhButtonClick && !thirdButtonClick)
                            {
                                Controls.Add(second);
                                Controls.Add(third);
                            }
                            else
                                IsTraderButton = false;
                            break;
                        case 3:
                            if (!fourthButtonClick && !fifthButtonClick)
                            {
                                Controls.Add(fourth);
                                Controls.Add(fifth);
                            }
                            else
                                IsTraderButton = false;
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
                Enemysteps++;
                if (Enemysteps == 18)
                    damage++;
                if (Enemysteps > 18)
                    Enemysteps = 0;
                else if (damage == 4)
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
                    if (_enemySpawn.Count == EnemyLevelTwo)
                        EnemySpeed = 4;
                    if (_enemySpawn.Count == EnemyLevelThree)
                        EnemySpeed = 5;

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
                        Enemysteps = 0;
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
                if (_animationPlayer.X + 50 > _boss.X)
                    _animationPlayer.Conflict = true;
                else
                    _animationPlayer.Conflict = false;
                _animationPlayer.Animation(_animationMap, _enemySpawn,
                    _bullet, _traders, _traderButton, _boss);
                Invalidate();
            };
        }

        public void BossTick(Timer boss, Timer bossAttack)
        {
            boss.Tick += (sender, args) =>
            {
                if (_boss.X <= 900)
                {
                    _boss.AnimateMove();
                    _boss.X -= BossSpeed;
                }
                if (_boss.X <= _animationPlayer.X + 55)
                {
                    boss.Stop();
                    bossAttack.Start();
                }
            };
        }

        public void BossAttack(Timer boss, Timer bossTimer, Timer player, Timer bullets, Timer enemy)
        {
            boss.Tick += (sender, args) =>
            {
                _boss.AnimateAttack();
                Bosssteps++;
                if (_boss.X > _animationPlayer.X + 55)
                {
                    Bosssteps = 0;
                    boss.Stop();
                    bossTimer.Start();
                }
                if (Bosssteps == 10)
                    damage++;
                if (Bosssteps > 10)
                    Bosssteps = 0;
                else if (damage == 4)
                {
                    player.Stop();
                    bullets.Stop();
                    enemy.Stop();
                    Fire = false;
                }
                _healthyBar.AnimationHealthy(damage);
            };
        }

        public void BossDead(Timer boss) => boss.Tick += (sender, args) =>
        {
            _boss.AnimateDead();
        };

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

            SetChoice();
            EnemyAttackTick(enemyAttack, playerTimer, moveBulletsTimer, enemyRun);
            EnemyRunTick(enemyRun, enemyAttack);
            EnemyDeadTick(enemyDead);
            PlayerTick(playerTimer);
            BossTick(bossTimer, bossAttack);
            BossAttack(bossAttack, bossTimer, playerTimer, moveBulletsTimer, enemyRun);
            BossDead(bossDead);
            FormPaint();

            FormClosing += (sender, args) => Application.Exit();
            moveBulletsTimer.Tick += (sender, args) => BulletBehavior(enemyRun, enemyDead, bossTimer, bossAttack, bossDead);
            KeyDown += (sender, args) => KeyIsDown(args);
            KeyUp += (sender, args) => KeyIsUp(args);
        }

        public void FormPaint()
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
                if (_enemySpawn.Count != 0)
                    DrawImage(g);
            };
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

        public void BulletBehavior(Timer enemyRun, Timer enemyDead, Timer bossMove, Timer bossAttack, Timer bossDead) // Еще не трогал
        {
            for (var i = 0; i < _bullets.Length; i++)
            {
                if (_boss.Healthy == 0) 
                {
                    bossMove.Stop();
                    bossAttack.Stop();
                    bossDead.Start();
                    if (!_boss.anim)
                        bossDead.Stop();
                }
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
                else if (_bullets[i].Left > _boss.X + 90 && Fire 
                    && _bullets[0].Location.Y != _animationPlayer.Y + 50)
                {
                    _bullets[i].Left = -10000;
                    _boss.Healthy--;
                    Fire = false;
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
            _boss.SetEnimiesImage();
            _animationPlayer.SetPlayerImage();
            _animationMap.SetImagesMaps();
            SetTraderImage();
            _traderButton.SetButtonImage();
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
                new AnimationEnimies { X = 2700},
                new AnimationEnimies { X = 2800},
                new AnimationEnimies { X = 2900},

                new AnimationEnimies { X = 5050, Healthy = 5},
                new AnimationEnimies { X = 5150, Healthy = 5},
                new AnimationEnimies { X = 5250, Healthy = 5},
                new AnimationEnimies { X = 5350, Healthy = 5},
                new AnimationEnimies { X = 5450, Healthy = 5},
                new AnimationEnimies { X = 5550, Healthy = 5},


                new AnimationEnimies { X = 6650, Healthy = 6},
                new AnimationEnimies { X = 6750, Healthy = 6},
                new AnimationEnimies { X = 6850, Healthy = 6},
                new AnimationEnimies { X = 6950, Healthy = 6},
                new AnimationEnimies { X = 7050, Healthy = 6},
                new AnimationEnimies { X = 7150, Healthy = 6},
                new AnimationEnimies { X = 7250, Healthy = 6},
                new AnimationEnimies { X = 7350, Healthy = 6},
                new AnimationEnimies { X = 7450, Healthy = 6},
                new AnimationEnimies { X = 7550, Healthy = 6}
            };
        }

        public void SetPositionTrade()
        {
            _traders = new List<Trader>
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
            if (e.KeyCode == Keys.F && !(_animationPlayer.goLeft || _animationPlayer.goRight) && !Fire && !_animationPlayer.StartPosition && !IsFining)
            {
                IsFining = true;
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
            if (e.KeyCode == Keys.F)
                IsFining = false;
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
            foreach (var trader in _traders)
                trader.SetTraderImage();
        }

        private void DrawTraderImage(Graphics g)
        {
            foreach (var trader in _traders)
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

        private void TraderAnimation()
        {
            foreach (var tard in _traders)
                tard.Animate();
        }

        private void AnimationRun() => _enemySpawn[0].AnimationRun();

        private void AnimationDead() => _enemySpawn[0].AnimationDead();

        private void AnimationAttack() => _enemySpawn[0].AnimationAttack();

    }
}
