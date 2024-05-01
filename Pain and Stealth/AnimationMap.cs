using Pain_and_Stealth;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pain_and_Stealth
{
    public class AnimationMap
    {
        public Map StartMap1;
        public Map StartMap2;
        public Map StartMap3;
        public Map TransitionMap;
        public Map SecondMap1;
        public Map SecondMap2;
        public Map SecondMap3;
        public Map SecondMap4;
        public Map SecondMap5;
        public Map SecondMap6;
        public SizeMap ClientSize;

        public AnimationMap()
        {
            StartMap1 = new Map { X = 0, Y = 0 };
            StartMap2 = new Map { X = 830, Y = 0 };
            StartMap3 = new Map { X = 1660, Y = 0 };
            TransitionMap = new Map { X = 2490, Y = 0 };
            SecondMap1 = new Map { X = 3320, Y = 0 };
            SecondMap2 = new Map { X = 4150, Y = 0 };
            SecondMap3 = new Map { X = 4980, Y = 0 };
            SecondMap4 = new Map { X = 5810, Y = 0 };
            SecondMap5 = new Map { X = 6640, Y = 0 };
            SecondMap6 = new Map { X = 7470, Y = 0 };
            ClientSize = new SizeMap();
        }

        public void SetImagesMaps()
        {
            StartMap1.map = Image.FromFile("StartMap.png");
            StartMap2.map = Image.FromFile("StartMap.png");
            StartMap3.map = Image.FromFile("StartMap.png");
            TransitionMap.map = Image.FromFile("TransitionsSecondMap.png");
            SecondMap1.map = Image.FromFile("SecondMap.png");
            SecondMap2.map = Image.FromFile("SecondMap.png");
            SecondMap3.map = Image.FromFile("SecondMap.png");
            SecondMap4.map = Image.FromFile("SecondMap.png");
            SecondMap5.map = Image.FromFile("SecondMap.png");
            SecondMap6.map = Image.FromFile("SecondMap.png");
        }

        public void DrawImage(Graphics g)
        {
            g.DrawImage(SecondMap2.map, SecondMap2.X, SecondMap2.Y, ClientSize.Width, ClientSize.Height);
            g.DrawImage(SecondMap1.map, SecondMap1.X, SecondMap1.Y, ClientSize.Width, ClientSize.Height);
            g.DrawImage(SecondMap3.map, SecondMap3.X, SecondMap3.Y, ClientSize.Width, ClientSize.Height);
            g.DrawImage(SecondMap4.map, SecondMap4.X, SecondMap4.Y, ClientSize.Width, ClientSize.Height);
            g.DrawImage(SecondMap5.map, SecondMap5.X, SecondMap5.Y, ClientSize.Width, ClientSize.Height);
            g.DrawImage(SecondMap6.map, SecondMap6.X, SecondMap6.Y, ClientSize.Width, ClientSize.Height);
            g.DrawImage(TransitionMap.map, TransitionMap.X, TransitionMap.Y, ClientSize.Width, ClientSize.Height);
            g.DrawImage(StartMap1.map, StartMap1.X, StartMap1.Y, ClientSize.Width, ClientSize.Height);
            g.DrawImage(StartMap2.map, StartMap2.X, StartMap2.Y, ClientSize.Width, ClientSize.Height);
            g.DrawImage(StartMap3.map, StartMap3.X, StartMap3.Y, ClientSize.Width, ClientSize.Height);
        }
    }
}
