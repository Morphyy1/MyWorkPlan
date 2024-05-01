using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pain_and_Stealth
{
    public class Trader
    {
        public Image trader;
        private int steps;
        public int X { get; set; }
        public int Y { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
        private int slowDowmFrameRate;
        public List<string> traderAniamtions;

        public Trader()
        {
            X = 1750;
            Y = 405;
            Height = 126;
            Width = 105;
            traderAniamtions = new List<string>();
        }

        public void SetTraderImage()
        {
            traderAniamtions = Directory.GetFiles("Trader", "*png").ToList();
            trader = Image.FromFile(traderAniamtions[0]);
        }

        public void AnimateTrader(List<string> Move, int start, int end)
        {
            slowDowmFrameRate += 1;
            if (slowDowmFrameRate == 4)
            {
                steps++;
                slowDowmFrameRate = 0;
            }
            if (steps > end || steps < start)
                steps = start;
            trader = Image.FromFile(Move[steps]);
        }

        public void Animate()
        {
            AnimateTrader(traderAniamtions, 0, 8);
        }

        public void DrawImage(Graphics g) => g.DrawImage(trader,
            X, Y, Width, Height);
    }
}
