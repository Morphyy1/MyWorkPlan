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
        public int BulletSpeed;
        public bool Fire;
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
            var traderAnimation = new Timer
            {
                Enabled = true,
                Interval = 1
            };
            var enemyAttack = new Timer
            {
                Enabled = false,
                Interval = 1
            };


            traderAnimation.Tick += (sender, args) =>
            {
                TraderAnimation();
            };
            enemyAttack.Tick += (sender, args) =>
            {
                AnimationAttack();
                steps++;
                if (steps == 18)
                    damage++;
                if (steps > 18)
                    steps = 0;
                if (damage == 5)
                {
                    _healthyBar.AnimationHealthy(damage);
                    playerTimer.Stop();
                    moveBulletsTimer.Stop();
                    enemyRun.Stop();
                    Fire = false;
                }
                _healthyBar.AnimationHealthy(damage);
            };
            enemyRun.Tick += (sender, args) =>
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
                        enemyRun.Stop();
                        enemyAttack.Start();
                    }
                    else
                    {
                        steps = 0;
                        enemyAttack.Stop();
                        enemyRun.Start();
                    }
                }
            };

            enemyDead.Tick += (sender, args) =>
            {
                if (_enemySpawn.Count != 0)
                    AnimationDead();
            };

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

            playerTimer.Tick += (sender, args) =>
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
                /*new AnimationEnimies { X = 1200,},
                new AnimationEnimies {X = 1350, },
                new AnimationEnimies {X = 1500,},*/
            };
        }

        public void SetPositionTrade()
        {
            traders = new List<Trader>
            {
                new Trader {X = 1000},
                new Trader {X = 1750},
                new Trader{X = 2750},
                new Trader{X = 5150}
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

        }

        private void KeyIsUp(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.D)
                _animationPlayer.goRight = false;
            if (e.KeyCode == Keys.A)
                _animationPlayer.goLeft = false;

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
