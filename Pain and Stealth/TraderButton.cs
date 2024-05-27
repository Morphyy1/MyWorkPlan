using System.Drawing;

namespace Pain_and_Stealth
{
    public class TraderButton
    {
        public Image Button;
        public int X { get; set; }
        public int Y { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }

        public TraderButton()
        {
            Y = 3375;
            Height = 25;
            Width = 25;
        }

        public void SetButtonImage() => Button = Image.FromFile("TraderButton.png");

        public void DrawButtonImage(Graphics g) => g.DrawImage(Button, X, Y, Width, Height);
    }

}
