﻿using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;

namespace Pain_and_Stealth
{
    public class AnimationPlayer
    {
        public bool goRight;
        public bool goLeft;
        public bool IsJump;
        public bool IsEndMap;
        public bool IsStartMap;

        public int Id;

        public bool PressStart;
        public bool PressThirdChoice;
        public bool PressFourthChoice;

        private int steps;
        public int force;
        public bool IsTrader;
        public int slowDowmFrameRate;

        private Image player;

        public List<string> playerMovementRight;
        private List<string> playerMovementLeft;

        public List<string> playerStartRight;
        private List<string> playerStartLeft;

        private List<string> playerThirdLeft;
        public List<string> playerThirdRight;

        private List<string> playerFourthLeft;
        public List<string> playerFourthRight;

        public bool Conflict;
        public bool StartPosition;

        private SizeMap ClientSize;
        public int X { get; set; }
        public int Y { get; set; }
        private int Height { get; set; }
        private int Width { get; set; }
        public int Speed { get; set; }

        public AnimationPlayer()
        {
            X = 350;
            Y = 440;
            Height = 100;
            Width = 105;
            Speed = 3;
            playerFourthLeft = new List<string>();
            playerFourthRight = new List<string>();
            playerStartRight = new List<string>();
            playerStartLeft = new List<string>();
            playerMovementRight = new List<string>();
            playerMovementLeft = new List<string>();
            playerThirdLeft = new List<string>();
            playerThirdRight = new List<string>();
            StartPosition = true;
            IsStartMap = true;
            ClientSize = new SizeMap();
        }

        public void SetPlayerImage()
        {
            playerFourthLeft = Directory.GetFiles("HeroFourthMoveLeft", "*png").ToList();
            playerFourthRight = Directory.GetFiles("HeroFourthMoveRight", "*png").ToList();
            playerThirdRight = Directory.GetFiles("HeroThirdMoveRight", "*png").ToList();
            playerThirdLeft = Directory.GetFiles("HeroThirdMoveLeft", "*png").ToList();
            playerStartRight = Directory.GetFiles("HeroRightStart", "*png").ToList();
            playerStartLeft = Directory.GetFiles("HeroLeftStart", "*png").ToList();
            playerMovementRight = Directory.GetFiles("HeroMoveRight", "*png").ToList();
            playerMovementLeft = Directory.GetFiles("HeroMoveLeft", "*png").ToList();
            player = Image.FromFile(playerStartRight[0]);
        }

        public void Animation(AnimationMap map, List<AnimationEnimies> enimies,
            Bullets bullet, List<Trader> traders, TraderButton traderButton, Boss boss,
            ExplosionStar explosion, SuperStar star)
        {
            if (bullet.IsFire)
            {
                bullet.X += 10;
            }
            if (goLeft && X > 0)
            {
                X -= Speed;

                ChoiceMoveLeft();
            }

            if (goRight && X + Width < ClientSize.Width && !Conflict)
            {
                X += Speed;
                ChoiceMoveRight();
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
                MovingMapRight(map);
                star.X -= Speed;
                explosion.X -= Speed;
                boss.X -= Speed;
                traderButton.X -= Speed;

                foreach (var trader in traders)
                    trader.X -= Speed;

                for (var i = 0; i < enimies.Count; i++)
                    enimies[i].X -= Speed;
                X = 400;
                IsStartMap = false;

            }
            if (X <= 200 && goLeft && !IsStartMap)
            {
                MovingMapLeft(map);
                star.X += Speed;
                explosion.X += Speed;
                boss.X += Speed;
                traderButton.X += Speed;

                foreach (var trader in traders)
                    trader.X += Speed;

                for (var i = 0; i < enimies.Count; i++)
                    enimies[i].X += Speed;
                X = 200;
            }

            if (map.StartMap1.X >= 0)
                IsStartMap = true;
            if (map.FinalMap2.X <= 500)
                IsEndMap = true;

            foreach (var trader in traders)
            {
                if (X + 200 >= trader.X)
                {
                    var coord = trader.X + 45;
                    traderButton.Y = 375;
                    traderButton.X = coord;
                    Id = trader.Id;
                    IsTrader = true;
                }
                if (trader.X + 150 < X)
                {
                    traderButton.Y = 1000;
                    IsTrader = false;
                    continue;
                }
                
            }
        }

        private void ChoiceMoveLeft()
        {
            if (PressThirdChoice)
                AnimatePlayer(playerThirdLeft, 0, 4);
            else if (PressStart)
                AnimatePlayer(playerMovementLeft, 0, 4);
            else if (PressFourthChoice)
                AnimatePlayer(playerFourthLeft, 0, 4);
            else
                AnimatePlayer(playerStartLeft, 0, 4);
        }

        private void ChoiceMoveRight()
        {
            if (PressThirdChoice)
                AnimatePlayer(playerThirdRight, 1, 5);
            else if (PressStart)
                AnimatePlayer(playerMovementRight, 1, 5);
            else if (PressFourthChoice)
                AnimatePlayer(playerFourthRight, 1, 5);
            else
                AnimatePlayer(playerStartRight, 1, 5);
        }


        private void MovingMapLeft(AnimationMap map)
        {
            map.StartMap1.X += Speed;
            map.StartMap2.X += Speed;
            map.StartMap3.X += Speed;
            map.StartMap4.X += Speed;
            map.TransitionMap.X += Speed;
            map.SecondMap1.X += Speed;
            map.SecondMap2.X += Speed;
            map.SecondMap3.X += Speed;
            map.SecondMap4.X += Speed;
            map.SecondMap5.X += Speed;
            map.SecondMap6.X += Speed;
            map.FinalMap1.X += Speed;
            map.FinalMap2.X += Speed;
        }

        private void MovingMapRight(AnimationMap map)
        {
            map.StartMap1.X -= Speed;
            map.StartMap2.X -= Speed;
            map.StartMap3.X -= Speed;
            map.StartMap4.X -= Speed;
            map.TransitionMap.X -= Speed;
            map.SecondMap1.X -= Speed;
            map.SecondMap2.X -= Speed;
            map.SecondMap3.X -= Speed;
            map.SecondMap4.X -= Speed;
            map.SecondMap5.X -= Speed;
            map.SecondMap6.X -= Speed;
            map.FinalMap1.X -= Speed;
            map.FinalMap2.X -= Speed;
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
