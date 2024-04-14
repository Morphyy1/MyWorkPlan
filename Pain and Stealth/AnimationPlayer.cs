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
        private List<string> playerMovementLeft;
        private Player Player;
        private SizeMap ClientSize;

        public AnimationPlayer()
        {
            playerMovementRight = new List<string>();
            playerMovementLeft = new List<string>();
            IsStartMap = true;
            Player = new Player();
            ClientSize = new SizeMap();
        }

        public void SetPlayerImage()
        {
            playerMovementRight = Directory.GetFiles("HeroMoveRight", "*png").ToList();
            playerMovementLeft = Directory.GetFiles("HeroMoveLeft", "*png").ToList();
            player = Image.FromFile(playerMovementRight[0]);
        }

        public void Animation(AnimationMap map, List<AnimationEnimies> enimies)
        {
            if (goLeft && Player.X > 0)
            {
                Player.X -= Player.Speed;
                AnimatePlayer(playerMovementLeft, 0, 4);
            }

            if (goRight && Player.X + Player.Width < ClientSize.Width)
            {
                Player.X += Player.Speed;
                AnimatePlayer(playerMovementRight, 1, 5);
            }

            if (IsJump)
            {
                Player.Y -= force;
                force -= 1;
            }

            if (Player.Y + Player.Height >= 550)
            {
                Player.Y = 540 - Player.Height;
                IsJump = false;
            }

            if (Player.X >= 400 && goRight && !IsEndMap)
            {
                map.StartMap1.X -= Player.Speed;
                map.StartMap2.X -= Player.Speed;
                map.TransitionMap.X -= Player.Speed;
                map.SecondMap1.X -= Player.Speed;
                map.SecondMap2.X -= Player.Speed;
                for (var i = 0; i < enimies.Count; i++)
                    enimies[i].X -= Player.Speed;
                Player.X = 400;
                IsStartMap = false;

            }
            if (Player.X <= 200 && goLeft && !IsStartMap)
            {
                map.StartMap1.X += Player.Speed;
                map.StartMap2.X += Player.Speed;
                map.TransitionMap.X += Player.Speed;
                map.SecondMap1.X += Player.Speed;
                map.SecondMap2.X += Player.Speed;
                for (var i = 0; i < enimies.Count; i++)
                    enimies[i].X += Player.Speed;
                Player.X = 200;
            }
            if (map.StartMap1.X >= 0)
                IsStartMap = true;
            if (map.SecondMap2.X <= 500)
                IsEndMap = true;
        }

        public void AnimatePlayer(List<string> Move, int start, int end)
        {
            slowDowmFrameRate += 1;
            if (slowDowmFrameRate == 8)
            {
                steps++;
                slowDowmFrameRate = 0;
            }
            if (steps > end || steps < start)
                steps = start;
            player = Image.FromFile(Move[steps]);
        }

        public void DrawImage(Graphics g) => g.DrawImage(player,
            Player.X, Player.Y, Player.Width, Player.Height);
    }
}
