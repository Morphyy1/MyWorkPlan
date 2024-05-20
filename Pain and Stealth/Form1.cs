using System.Collections.Generic;
using System.Drawing;
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
        private bool IsFinalRound;

        private bool IsSuper;
        private bool StarIsActiveted;
        private bool IsMap;

        private bool dark;
        private bool hit;

        private int EnemySpeed;
        private int BossSpeed;
        private int _heroDamage;
        private int _bossDamage;
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
        private BossHealthyBar _bossHealthyBar;
        private AnimationMap _animationMap;
        private AnimationPlayer _animationPlayer;
        private List<AnimationEnimies> _enemySpawn;
        private AddEnemy _addEnemy;
        private SuperStar _superStar;
        private SuperButton _superButton;
        private ExplosionStar _explosionStar;

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
            _bossHealthyBar = new BossHealthyBar();
            _traderButton = new TraderButton();
            _superStar = new SuperStar();
            _explosionStar = new ExplosionStar();
            _addEnemy = new AddEnemy();
            _bullet = new Bullets();
            _boss = new Boss();
            _superButton = new SuperButton();

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

        private void SetTrederChoice(Button first, Button second, Button third, Button fourth, Button fifth, Timer trader)
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

        private void EnemyAttackTick(Timer enemy, Timer playerTimer, Timer moveBulletsTimer, Timer enemyRun,
            Button menuButton, Button newGameButton, PictureBox youLose, PictureBox blackFront)
        {
            enemy.Tick += (sender, args) =>
            {
                if (_enemySpawn.Count != 0)
                {
                    AnimationAttack();
                    Enemysteps++;
                    if (Enemysteps == 18)
                        _heroDamage++;
                    if (Enemysteps > 18)
                        Enemysteps = 0;
                    else if (_heroDamage == 4)
                    {
                        playerTimer.Stop();
                        moveBulletsTimer.Stop();
                        enemyRun.Stop();
                        _bullets[0].BringToFront();
                        SetIfHeroDead(menuButton, newGameButton, youLose, blackFront);
                        Fire = true;
                    }
                    _healthyBar.AnimationHealthy(_heroDamage);
                }
            };
        }

        private void SetIfHeroDead(Button menuButton, Button newGameButton, PictureBox youLose, PictureBox blackFront)
        {
            Controls.Add(menuButton);
            Controls.Add(newGameButton);
            Controls.Add(youLose);
            Controls.Add(blackFront);
        }

        private void EnemyRunTick(Timer enemy, Timer enemyAttack)
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

        private void EnemyDeadTick(Timer enemy)
        {
            enemy.Tick += (sender, args) =>
            {
                if (_enemySpawn.Count != 0)
                    AnimationDead();
            };
        }

        private void PlayerTick(Timer player)
        {
            player.Tick += (sender, args) =>
            {
                if (_animationMap.SecondMap1.X <= 0 && !StarIsActiveted)
                {
                    dark = true;
                    StarIsActiveted = true;
                }
                if (_enemySpawn.Count != 0)
                {
                    if (_animationPlayer.X + 50 > _enemySpawn[0].X)
                        _animationPlayer.Conflict = true;
                    else
                        _animationPlayer.Conflict = false;
                }
                else if (_animationPlayer.X + 50 > _boss.X)
                    _animationPlayer.Conflict = true;
                else
                    _animationPlayer.Conflict = false;
                _animationPlayer.Animation(_animationMap, _enemySpawn,
                    _bullet, _traders, _traderButton, _boss, _explosionStar, _superStar);
                Invalidate();
            };
        }

        private void BossTick(Timer boss, Timer bossAttack)
        {
            boss.Tick += (sender, args) =>
            {
                if (_boss.X <= 900)
                {
                    IsFinalRound = true;
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

        private void BossAttack(Timer boss, Timer bossTimer, Timer player, Timer bullets, Timer enemy,
            Button menuButton, Button newGameButton, PictureBox youLose, PictureBox blackFront)
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
                if (Bosssteps == 7)
                    _heroDamage += 2;
                if (Bosssteps > 10)
                    Bosssteps = 0;
                else if (_heroDamage == 4)
                {
                    player.Stop();
                    bullets.Stop();
                    enemy.Stop();
                    _bullets[0].BringToFront();
                    SetIfHeroDead(menuButton, newGameButton, youLose, blackFront);
                    Fire = true;
                }
                _healthyBar.AnimationHealthy(_heroDamage);
            };
        }

        private void BossDead(Timer boss) => boss.Tick += (sender, args) => _boss.AnimateDead();

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
            var starDrop = new Timer { Interval= 1 };
            var explosionStar = new Timer { Interval = 1 };
            var darkTimer = new Timer { Interval = 10000};;

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

        private void StarDownTick(Timer starDown, Timer explosionStar)
        {
            starDown.Tick += (sender, args) =>
            {
                _superStar.SuperStarAnimationDown();
                if (!_superStar.anim)
                {
                    starDown.Stop();
                    _superStar.Y = 0;
                    IsSuper = false;
                    explosionStar.Start();
                }
            };
        }

        private void StarDropingTick(Timer starDrop, Timer starDown)
        {
            starDrop.Tick += (sender, args) =>
            {
                _superStar.SuperStarAnimationDrop();
                _superStar.Y += 25;
                if (_superStar.Y >= 470)
                {
                    starDrop.Stop();
                    starDown.Start();
                }
            };
        }

        private void DarkStarTick(Timer darkTimer)
        {
            darkTimer.Tick += (sender, args) =>
            {
                dark = true;
                darkTimer.Stop();
            };
        }
        
        private void MenuButtonClicked(Button menuButton, Button newGameButton)
        {
            menuButton.Click += (s, e) =>
            {
                var menu = new Menu();
                menu.Show();
                Hide();
            };
            newGameButton.Click += (s, e) =>
            {
                var newGame = new Form1();
                newGame.Show();
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
                    Hide();
                }
            };
        }

        private void ExplosionEventTick(Timer explosionStar, Timer final, Timer bossTimer, Timer bossAttack,
            Timer bossDead, PictureBox finalText, PictureBox blackFront)
        {
            explosionStar.Tick += (sender, args) =>
            {
                IsMap = true;
                _explosionStar.Animate();
                if (_enemySpawn.Count != 0)
                    if ((_explosionStar.X + _explosionStar.Width >= _enemySpawn[0].X && _explosionStar.X + _explosionStar.Width <= _enemySpawn[0].X + _enemySpawn[0].Width)
                        || (_explosionStar.X >= _enemySpawn[0].X && _explosionStar.X <= _enemySpawn[0].X + _enemySpawn[0].Width))
                        _enemySpawn.Remove(_enemySpawn[0]);
                if ((_explosionStar.X + _explosionStar.Width >= _boss.X && _explosionStar.X + _explosionStar.Width <= _boss.X + _boss.Width
                || (_explosionStar.X >= _boss.X && _explosionStar.X <= _boss.X + _boss.Width)) && !hit)
                {
                    hit = true;
                    _boss.Healthy -= 3;
                    _bossDamage += 3;
                    IsBossDead(finalText, blackFront, final, bossTimer, bossAttack, bossDead);
                    _bossHealthyBar.AnimationHealthy(_bossDamage);
                }
                if (!_explosionStar.anim)
                {
                    IsMap = false;
                    explosionStar.Stop();
                }
            };
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

        private void SetStarEvent(Timer darkTimer, Timer starDown, Timer starDrop, Timer explosionStar)
        {
            DarkStarTick(darkTimer);
            StarDropingTick(starDrop, starDown);
            StarDownTick(starDown, explosionStar);
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
                if (dark)
                    _superButton.DrawButtonImage(g);
            };
        }

        private void SetUpForBullets()
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

        private void IsBossDead(PictureBox text, PictureBox blackFront, Timer final,
            Timer bossMove, Timer bossAttack, Timer bossDead)
        {
            if (_boss.Healthy < 0)
            {
                bossMove.Stop();
                bossAttack.Stop();
                bossDead.Start();
                if (!_boss.anim)
                {
                    Fire = true;
                    _bullets[0].BringToFront();
                    final.Start();
                    Controls.Add(text);
                    Controls.Add(blackFront);
                }
            }
        }

        private void IsBulletHitTarget(Timer enemyRun, Timer enemyDead, int i)
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
        }

        private void IsEnemy(Timer enemyRun, Timer enemyDead)
        {
            if (!_enemySpawn[0].anim)
            {
                _enemySpawn[0].anim = true;
                enemyDead.Stop();
                enemyRun.Start();
            }
            if (_enemySpawn[0].Healthy == 0)
                _enemySpawn.Remove(_enemySpawn[0]);
        }

        private void BulletBehavior(Timer enemyRun, Timer enemyDead, Timer bossMove, Timer bossAttack, Timer bossDead,
            PictureBox text, PictureBox blackFront, Timer final)
        {
            for (var i = 0; i < _bullets.Length; i++)
            {
                IsBossDead(text, blackFront, final, bossMove, bossAttack, bossDead);
                if (_enemySpawn.Count != 0)
                {
                    IsBulletHitTarget(enemyRun, enemyDead, i);
                    IsEnemy(enemyRun, enemyDead);
                }
                else if (_bullets[i].Left > _boss.X + 90 && Fire
                    && _bullets[0].Location.Y != _animationPlayer.Y + 50)
                {
                    _bullets[i].Left = -10000;
                    _boss.Healthy--;
                    _bossDamage++;
                    _bossHealthyBar.AnimationHealthy(_bossDamage);
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

        private void SetAllImages()
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
            _superStar.SetImage();
            _bossHealthyBar.SetIamage();
            _explosionStar.SetImage();
            _superButton.SetButtonImage();
        }

        private void SetBackgroundImage()
        {
            BackgroundImage = Image.FromFile("StartMap.png");
            BackgroundImageLayout = ImageLayout.Stretch;
            DoubleBuffered = true;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
        }

        private void SetPositionEnemy() => _enemySpawn = _addEnemy.SetPositionEnemy(_enemySpawn);

        private void SetPositionTrade()
        {
            _traders = new List<Trader>
            {
                new Trader {X = 1750, Id = 1},
                new Trader{X = 3400, Id = 2},
                new Trader{X = 6000, Id = 3}
            };
        }

        private void KeyIsDown(KeyEventArgs e, Timer star, Timer darkTimer)
        {
            if (e.KeyCode == Keys.D)
                _animationPlayer.goRight = true;
            if (e.KeyCode == Keys.A)
                _animationPlayer.goLeft = true;
            if (e.KeyCode == Keys.Q && dark)
            {
                IsSuper = true;
                dark = false;
                darkTimer.Start();
                _superStar.X = _animationPlayer.X + 250;
                _explosionStar.X = _superStar.X - 80;
                star.Start();
            }
            if (e.KeyCode == Keys.F && !(_animationPlayer.goLeft || _animationPlayer.goRight) &&
                !Fire && !_animationPlayer.StartPosition && !IsFining)
            {
                IsFining = true;
                Fire = true;
                hit = false;
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