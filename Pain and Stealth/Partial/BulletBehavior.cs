using System.Drawing;
using System.Windows.Forms;

namespace Pain_and_Stealth
{
    partial class Form1
    {
        private int BulletSpeed;
        private bool IsMap;
        private Bullets _bullet;
        private PictureBox[] _bullets;

        private void SetBullet()
        {
            _bullet = new Bullets();
            BulletSpeed = 10;
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
    }
}
