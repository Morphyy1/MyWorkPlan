using System.Drawing;


namespace Pain_and_Stealth
{
    public class AnimationMap
    {
        public Map StartMap1;
        public Map StartMap2;
        public Map StartMap3;
        public Map StartMap4;
        public Map TransitionMap;
        public Map SecondMap1;
        public Map SecondMap2;
        public Map SecondMap3;
        public Map SecondMap4;
        public Map SecondMap5;
        public Map SecondMap6;
        public Map FinalMap1;
        public Map FinalMap2;
        public SizeMap ClientSize;

        private const string startMap = "StartMap.png";
        private const string secondMap = "SecondMap.png";
        private const string transitionMap = "TransitionsSecondMap.png";
        private const string finalMap = "StartMenu.png";

        public AnimationMap()
        {
            StartMap1 = new Map { X = 0, Y = 0 };
            StartMap2 = new Map { X = 830, Y = 0 };
            StartMap3 = new Map { X = 1660, Y = 0 };
            StartMap4 = new Map { X = 2490, Y = 0 };
            TransitionMap = new Map { X = 3320, Y = 0 };
            SecondMap1 = new Map { X = 4150, Y = 0 };
            SecondMap2 = new Map { X = 4980, Y = 0 };
            SecondMap3 = new Map { X = 5810, Y = 0 };
            SecondMap4 = new Map { X = 6640, Y = 0 };
            SecondMap5 = new Map { X = 7470, Y = 0 };
            SecondMap6 = new Map { X = 8300, Y = 0 };
            FinalMap1 = new Map { X = 9130, Y = 0 };
            FinalMap2 = new Map { X = 9960, Y = 0 };
            ClientSize = new SizeMap();
        }

        public void SetImagesMaps()
        {
            StartMap1.map = Image.FromFile(startMap);
            StartMap2.map = Image.FromFile(startMap);
            StartMap3.map = Image.FromFile(startMap);
            StartMap4.map = Image.FromFile(startMap);
            TransitionMap.map = Image.FromFile(transitionMap);
            SecondMap1.map = Image.FromFile(secondMap);
            SecondMap2.map = Image.FromFile(secondMap);
            SecondMap3.map = Image.FromFile(secondMap);
            SecondMap4.map = Image.FromFile(secondMap);
            SecondMap5.map = Image.FromFile(secondMap);
            SecondMap6.map = Image.FromFile(secondMap);
            FinalMap1.map = Image.FromFile(finalMap);
            FinalMap2.map = Image.FromFile(finalMap);
        }

        public void DrawImage(Graphics g)
        {
            g.DrawImage(FinalMap1.map, FinalMap1.X, FinalMap1.Y, ClientSize.Width, ClientSize.Height);
            g.DrawImage(FinalMap2.map, FinalMap2.X, FinalMap2.Y, ClientSize.Width, ClientSize.Height);
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
            g.DrawImage(StartMap4.map, StartMap4.X, StartMap4.Y, ClientSize.Width, ClientSize.Height);
        }
    }
}
