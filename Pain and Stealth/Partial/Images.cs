using System.Drawing;
using System.Windows.Forms;

namespace Pain_and_Stealth
{
    partial class Form1
    {
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
    }
}
