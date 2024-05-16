using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pain_and_Stealth
{
    public class SuperButton
    {
        public Image Button;
        public int X { get; set; }
        public int Y { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }

        public SuperButton()
        {
            X = 160;
            Y = 17;
            Height = 25;
            Width = 25;
        }

        public void SetButtonImage() => Button = Image.FromFile("SuperButton.png");

        public void DrawButtonImage(Graphics g) => g.DrawImage(Button, X, Y, Width, Height);
    }
}
