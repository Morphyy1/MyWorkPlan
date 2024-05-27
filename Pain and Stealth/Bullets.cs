using System.Drawing;
namespace Pain_and_Stealth
{

    public class Bullets
    {
        private Image bullet;
        public int X { get; set; }
        public int Y { get; set; }
        private int Height { get; set; }
        private int Width { get; set; }
        public bool IsFire { get; set; }


        public Bullets()
        {
            Y = 300;
            Height = 4;
            Width = 15;
        }

        public void SetImage() => bullet = Image.FromFile("Bullet.png");

        public void DrawImage(Graphics g) => g.DrawImage(bullet,
            X, Y, Width, Height);
    }
}
