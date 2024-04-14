using Pain_and_Stealth;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pain_and_Stealth
{
    public class AnimationEnimies
    {
        Image enimies;
        List<string> EnemiesDead;
        private int slowDowmFrameRate;
        private int steps;
        private Random random;
        public int X { get; set; }
        public int Y { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }

        public AnimationEnimies()
        {
            Y = 395;
            Height = 148;
            Width = 155;
            EnemiesDead = new List<string>();
        }

        public void SetEnimiesImage()
        {
            EnemiesDead = Directory.GetFiles("EnemiesDead", "*png").ToList();
            enimies = Image.FromFile(EnemiesDead[0]);
        }

        public void Animation()
        {
            AnimateEnimies(EnemiesDead, 0, 3);
        }

        public void AnimateEnimies(List<string> Move, int start, int end)
        {
            slowDowmFrameRate += 1;
            if (slowDowmFrameRate == 5)
            {
                steps++;
                slowDowmFrameRate = 0;
            }
            if (steps > end || steps < start)
                steps = start;
            enimies = Image.FromFile(Move[steps]);
        }

        public void DrawImage(Graphics g) => g.DrawImage(enimies,
            X, Y, Width, Height);
    }
}
