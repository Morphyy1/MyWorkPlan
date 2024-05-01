using Pain_and_Stealth;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pain_and_Stealth
{
    public class AnimationEnimies
    {
        public Image enimies;
        List<string> EnemiesDead;
        List<string> EnemiesRun;
        List<string> EnemiesAttack;
        private int slowDowmFrameRate;
        public int slowSteps;
        private int steps;
        public bool anim = true;
        public int Healthy;

        public int X { get; set; }
        public int Y { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }

        public AnimationEnimies()
        {
            Healthy = 3;
            Y = 395;
            Height = 148;
            Width = 155;
            slowSteps = 4;
            EnemiesDead = new List<string>();
            EnemiesRun = new List<string>();
        }

        public void SetEnimiesImage()
        {
            EnemiesDead = Directory.GetFiles("EnemiesDead", "*png").ToList();
            EnemiesAttack = Directory.GetFiles("EnemiesAttack", "*png").ToList();
            EnemiesRun = Directory.GetFiles("EnemiesRun", "*png").ToList();
            enimies = Image.FromFile(EnemiesDead[0]);
        }

        public void StateAnimation()
        {
            AnimateEnimies(EnemiesDead, 0,0);
        }

        public void AnimationDead()
        {
            AnimateEnimies(EnemiesDead, 0, 4);
        }

        public void AnimationRun()
        {
            AnimateEnimies(EnemiesRun, 0, 5);
        }
        public void AnimationAttack()
        {
            AnimateEnimies(EnemiesAttack, 0, 8);
        }

        public void AnimateEnimies(List<string> Move, int start, int end)
        {
            slowDowmFrameRate += 1;
            if (slowDowmFrameRate == slowSteps)
            {
                steps++;
                slowDowmFrameRate = 0;
            }
            if (steps > end)
            {
                steps = start;
                enimies = Image.FromFile(Move[start]);
                anim = false;
            }    
            else 
                enimies = Image.FromFile(Move[steps]);
        }

        public void DrawImage(Graphics g) => g.DrawImage(enimies,
            X, Y, Width, Height);
    }
}
