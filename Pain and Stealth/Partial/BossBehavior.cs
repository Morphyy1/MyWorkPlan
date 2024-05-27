using System.Windows.Forms;

namespace Pain_and_Stealth
{
    partial class Form1
    {
        private Boss _boss;
        private BossHealthyBar _bossHealthyBar;
        private int _bossDamage;
        private int _bossSpeed;
        private int _bossSteps;


        private void SetBoss()
        {
            _boss = new Boss();
            _bossHealthyBar = new BossHealthyBar();
            _bossSpeed = 4;
        }
        private void BossTick(Timer boss, Timer bossAttack)
        {
            boss.Tick += (sender, args) =>
            {
                if (_boss.X <= 900)
                {
                    IsFinalRound = true;
                    _boss.AnimateMove();
                    _boss.X -= _bossSpeed;
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
                _bossSteps++;
                if (_boss.X > _animationPlayer.X + 55)
                {
                    _bossSteps = 0;
                    boss.Stop();
                    bossTimer.Start();
                }
                if (_bossSteps == 7)
                    _heroDamage += 2;
                if (_bossSteps > 10)
                    _bossSteps = 0;
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

        private void BossDead(Timer boss) => boss.Tick += (sender, args) => _boss.AnimateDead();
    }
}
