using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pain_and_Stealth
{
    public class Boss
    {
        public Image boss; 
        private int steps;
        public int X { get; set; }
        public int Y { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
        public List<string> BossMove;
        public List<string> BossAttack;
        public List<string> BossDead;
        private int slowDowmFrameRate;
        public bool anim = false;

        public Boss()
        {
            X = 9700;
            Y = 290;
            Height = 245;
            Width = 200;
        }

        public void SetEnimiesImage()
        {
            BossMove = Directory.GetFiles("BossMove", "*png").ToList();
            BossAttack = Directory.GetFiles("BossAttack", "*png").ToList();
            BossDead = Directory.GetFiles("BossDead", "*png").ToList();
            boss = Image.FromFile(BossMove[0]);
        }

        public void AnimateBoss(List<string> Move, int start, int end)
        {
            slowDowmFrameRate += 1;
            if (slowDowmFrameRate == 8)
            {
                steps++;
                slowDowmFrameRate = 0;
            }
            if (steps > end)
            {
                anim = false;
                steps = start;
                boss = Image.FromFile(Move[start]);
            }
            else
                boss = Image.FromFile(Move[steps]);
        }

        public void AnimateMove()
        {
            AnimateBoss(BossMove, 0, 3);
        }

        public void AnimateAttack()
        {
            AnimateBoss(BossAttack, 0, 3);
        }

        public void AnimateDead()
        {
            AnimateBoss(BossDead, 0, 17);
        }

        public void DrawImage(Graphics g) => g.DrawImage(boss,
            X, Y, Width, Height);
    }
}
