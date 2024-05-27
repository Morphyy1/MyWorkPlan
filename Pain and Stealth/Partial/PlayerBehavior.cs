using System.Windows.Forms;

namespace Pain_and_Stealth
{
    partial class Form1
    {
        private int _heroDamage;
        private HealthyBar _healthyBar;
        private AnimationMap _animationMap;
        private AnimationPlayer _animationPlayer;

        private void SetPlayer()
        {
            _animationPlayer = new AnimationPlayer();
            _animationMap = new AnimationMap();
            _healthyBar = new HealthyBar();
        }

        private void SetIfHeroDead(Button menuButton, Button newGameButton, PictureBox youLose, PictureBox blackFront)
        {
            Controls.Add(menuButton);
            Controls.Add(newGameButton);
            Controls.Add(youLose);
            Controls.Add(blackFront);
        }

        private void PlayerTick(Timer player)
        {
            player.Tick += (sender, args) =>
            {
                if (_animationMap.SecondMap1.X <= 0 && !StarIsActiveted)
                {
                    _darkSpace = true;
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
    }
}
