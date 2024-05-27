using System.Windows.Forms;

namespace Pain_and_Stealth
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            SetUp();
            EventsCollection();
        }

        private void SetUp()
        {
            SetPlayer();
            SetSuperStar();
            SetBullet();
            SetTrader();
            SetBoss();
            SetEnemy();
            SetAllImages();
            SetSound();
            SetEnemyLevel();
        }
    }
}