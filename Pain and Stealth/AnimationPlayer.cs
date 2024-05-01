using Pain_and_Stealth;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pain_and_Stealth
{
    public class AnimationPlayer
    {
        public bool goRight;
        public bool goLeft;
        public bool IsJump;
        public bool IsEndMap;
        public bool IsStartMap;
        private int steps;
        public int force;
        private int slowDowmFrameRate;

        private Image player;
        public List<string> playerMovementRight;
        public List<string> playerMovementLeft;
        public List<string> playerStartRight;
        public List<string> playerStartLeft;
        public bool Conflict = false;
        public bool StartPosition = true;

        private SizeMap ClientSize;
        public int X { get; set; }
        public int Y { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
        public int Speed { get; set; }
        public int Healthy { get; set; }

        public AnimationPlayer()
        {
            X = 350;
            Y = 440;
            Height = 100;
            Width = 105;
            Speed = 3;
            Healthy = 4;
            playerStartRight = new List<string>();
            playerStartLeft = new List<string>();
            playerMovementRight = new List<string>();
            playerMovementLeft = new List<string>();
            IsStartMap = true;
            ClientSize = new SizeMap();
        }

        public void SetPlayerImage()
        {
            playerStartRight = Directory.GetFiles("HeroRightStart", "*png").ToList();
            playerStartLeft = Directory.GetFiles("HeroLeftStart", "*png").ToList();
            playerMovementRight = Directory.GetFiles("HeroMoveRight", "*png").ToList();
            playerMovementLeft = Directory.GetFiles("HeroMoveLeft", "*png").ToList();
            player = Image.FromFile(playerStartRight[0]);
        }

        public void Animation(AnimationMap map, List<AnimationEnimies> enimies,
            Bullets bullet, Trader trader)
        {
            if (bullet.IsFire)
            {
                bullet.X += 10;
            }
            if (goLeft && X > 0)
            {
                X -= Speed;
                AnimatePlayer(playerStartLeft, 0, 4);
            }

            if (goRight && X + Width < ClientSize.Width && !Conflict)
            {
                X += Speed;
                AnimatePlayer(playerStartRight, 1, 5);
            }

            if (IsJump)
            {
                Y -= force;
                force -= 1;
            }

            if (Y + Height >= 550)
            {
                Y = 540 - Height;
                IsJump = false;
            }

            if (X >= 400 && goRight && !IsEndMap && !Conflict)
            {
                map.StartMap1.X -= Speed;
                map.StartMap2.X -= Speed;
                map.StartMap3.X -= Speed;
                map.TransitionMap.X -= Speed;
                map.SecondMap1.X -= Speed;
                map.SecondMap2.X -= Speed;
                map.SecondMap3.X -= Speed;
                map.SecondMap4.X -= Speed;
                map.SecondMap5.X -= Speed;
                map.SecondMap6.X -= Speed;

                trader.X -= Speed;

                for (var i = 0; i < enimies.Count; i++)
                    enimies[i].X -= Speed;
                X = 400;
                IsStartMap = false;

            }
            if (X <= 200 && goLeft && !IsStartMap)
            {
                map.StartMap1.X += Speed;
                map.StartMap2.X += Speed;
                map.StartMap3.X += Speed;
                map.TransitionMap.X += Speed;
                map.SecondMap1.X += Speed;
                map.SecondMap2.X += Speed;
                map.SecondMap3.X += Speed;
                map.SecondMap4.X += Speed;
                map.SecondMap5.X += Speed;
                map.SecondMap6.X += Speed;
                
                trader.X += Speed;

                for (var i = 0; i < enimies.Count; i++)
                    enimies[i].X += Speed;
                X = 200;
            }
            if (map.StartMap1.X >= 0)
                IsStartMap = true;
            if (map.SecondMap6.X <= 500)
                IsEndMap = true;
        }

        public void AnimatePlayer(List<string> Move, int start, int end)
        {
            slowDowmFrameRate += 1;
            if (slowDowmFrameRate == 5)
            {
                steps++;
                slowDowmFrameRate = 0;
            }
            if (steps > end || steps < start)
                steps = start;
            player = Image.FromFile(Move[steps]);
        }

        public void DrawImage(Graphics g) => g.DrawImage(player,
            X, Y, Width, Height);
    }
}
