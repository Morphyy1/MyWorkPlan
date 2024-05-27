using System.Drawing;
using System.Windows.Forms;

namespace Pain_and_Stealth
{
    partial class Form1
    {
        private void KeyIsDown(KeyEventArgs e, Timer star, Timer darkTimer)
        {
            if (e.KeyCode == Keys.D)
                _animationPlayer.goRight = true;
            if (e.KeyCode == Keys.A)
                _animationPlayer.goLeft = true;
            if (e.KeyCode == Keys.Q && _darkSpace)
            {
                IsSuper = true;
                _darkSpace = false;
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
                _darkHit = false;
                PlayShootSound();
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
    }
}
