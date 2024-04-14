using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pain_and_Stealth
{
    public class Player
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
        public int Speed { get; set; }

        public Player()
        {
            X = 350;
            Y = 440;
            Height = 100;
            Width = 105;
            Speed = 3;
        }
    }

}
