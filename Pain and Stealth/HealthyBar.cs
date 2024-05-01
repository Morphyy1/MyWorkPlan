using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pain_and_Stealth
{
    public class HealthyBar
    {
        private Image healthy;
        private List<string> healthyAnimation;
        private int x { get; set; }
        private int y { get; set; }
        private int height { get; set; }
        private int width { get; set; }

        public HealthyBar()
        {
            x = 10;
            y = 10;
            width = 148;
            height = 37;
            healthyAnimation = new List<string>();
        }

        public void SetIamage()
        {
            healthyAnimation = Directory.GetFiles("HealthyBar", "*png").ToList();
            healthy = Image.FromFile(healthyAnimation[0]);
        }

        public void AnimationHealthy(int damage)
        {
            if (damage <= 4)
                healthy = Image.FromFile(healthyAnimation[damage]);
        }

        public void DrawImage(Graphics g) => g.DrawImage(healthy,
           x, y, width, height);
    }
}
