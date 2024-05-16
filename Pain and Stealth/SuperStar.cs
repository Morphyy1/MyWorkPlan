using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Pain_and_Stealth
{
    public class SuperStar
    {
        private Image super;
        public int X { get; set; }
        public int Y { get; set; }
        public int Height { get; set; }
        private int Width { get; set; }
        public List<string> AnimationStarDown;
        private int slowDowmFrameRate;
        private int steps;
        public bool anim;

        public SuperStar()
        {
            Width = 35;
            Height = 78;
            AnimationStarDown = new List<string>();
        }

        public void SetImage()
        {
            AnimationStarDown = Directory.GetFiles("SuperStar").ToList();
            super = Image.FromFile(AnimationStarDown[0]);
        }

        public void AnimateStar(List<string> Move, int start, int end)
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
                super = Image.FromFile(Move[start]);
                anim = false;
            }
            else
                super = Image.FromFile(Move[steps]);
        }

        public void SuperStarAnimationDown() => AnimateStar(AnimationStarDown, 4, 6);
        public void SuperStarAnimationDrop() => AnimateStar(AnimationStarDown, 0, 3);

        public void DrawImage(Graphics g) => g.DrawImage(super,
            X, Y, Width, Height);
    }
}
