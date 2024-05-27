

using System.Windows.Forms;

namespace Pain_and_Stealth
{
    partial class Form1
    {
        private bool IsSuper;
        private bool StarIsActiveted;
        private bool _darkSpace;
        private bool _darkHit;
        private SuperStar _superStar;
        private SuperButton _superButton;
        private ExplosionStar _explosionStar;

        private void SetSuperStar()
        {
            _superStar = new SuperStar();
            _explosionStar = new ExplosionStar();
            _superButton = new SuperButton();
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
                _darkSpace = true;
                darkTimer.Stop();
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
                || (_explosionStar.X >= _boss.X && _explosionStar.X <= _boss.X + _boss.Width)) && !_darkHit)
                {
                    _darkHit = true;
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

        private void SetStarEvent(Timer darkTimer, Timer starDown, Timer starDrop, Timer explosionStar)
        {
            DarkStarTick(darkTimer);
            StarDropingTick(starDrop, starDown);
            StarDownTick(starDown, explosionStar);
        }
    }
}
