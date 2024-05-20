using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pain_and_Stealth
{
    public class ExplosionStar
    {
        private Image explosion;
        public int X { get; set; }
        public int Y { get; set; }
        private int Height { get; set; }
        public int Width { get; set; }
        public List<string> AnimationExplosion;
        private int slowDowmFrameRate;
        private int steps;
        public bool anim;

        public ExplosionStar()
        {
            Y = 370;
            Width = 205;
            Height = 189;
            AnimationExplosion = new List<string>();
        }

        public void SetImage()
        {
            AnimationExplosion = Directory.GetFiles("ExplosionStar").ToList();
            explosion = Image.FromFile(AnimationExplosion[0]);
        }

        public void AnimateExplosion(List<string> Move, int start, int end)
        {
            slowDowmFrameRate += 1;
            anim = true;
            if (slowDowmFrameRate == 5)
            {
                steps++;
                slowDowmFrameRate = 0;
            }
            if (steps > end)
            {
                steps = start;
                explosion = Image.FromFile(Move[start]);
                anim = false;
            }
            else
                explosion = Image.FromFile(Move[steps]);
        }

        public void Animate() => AnimateExplosion(AnimationExplosion, 0, 5);

        public void DrawImage(Graphics g) => g.DrawImage(explosion,
            X, Y, Width, Height);
    }
}
