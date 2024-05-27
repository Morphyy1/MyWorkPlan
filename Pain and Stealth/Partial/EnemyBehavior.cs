using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Pain_and_Stealth
{
    partial class Form1
    {
        private int EnemySpeed;
        private int Enemysteps;
        private int EnemyLevelTwo;
        private int EnemyLevelThree;
        private AddEnemy _addEnemy;
        private List<AnimationEnimies> _enemySpawn;

        private void SetEnemy()
        {
            _addEnemy = new AddEnemy();
            EnemySpeed = 2;
        }

        private void SetEnemyLevel()
        {
            EnemyLevelTwo = _enemySpawn.Count - 3;
            EnemyLevelThree = EnemyLevelTwo - 6;
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

        private void SetPositionEnemy() => _enemySpawn = _addEnemy.SetPositionEnemy(_enemySpawn);

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

        private void AnimationRun() => _enemySpawn[0].AnimationRun();

        private void AnimationDead() => _enemySpawn[0].AnimationDead();

        private void AnimationAttack() => _enemySpawn[0].AnimationAttack();
    }
}
