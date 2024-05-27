using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Pain_and_Stealth
{
    partial class Form1
    {
        private List<Trader> _traders;
        private TraderButton _traderButton;
        private bool firstButtonClick;
        private bool secondhButtonClick;
        private bool thirdButtonClick;
        private bool fourthButtonClick;
        private bool fifthButtonClick;
        private bool IsTraderButton;

        private bool Fire;
        private bool IsFining;
        private bool IsFinalRound;


        private void SetTrader()
        {
            _traderButton = new TraderButton();
        }

        private void SetPositionTrade()
        {
            _traders = new List<Trader>
            {
                new Trader {X = 1750, Id = 1},
                new Trader{X = 3400, Id = 2},
                new Trader{X = 6000, Id = 3}
            };
        }

        private void SetTrederChoice(Button first, Button second, Button third, Button fourth, Button fifth, Timer trader)
        {
            trader.Tick += (sender, args) =>
            {
                TraderAnimation();
                if (IsTraderButton)
                {
                    switch (_animationPlayer.Id)
                    {
                        case 1:
                            if (!firstButtonClick)
                                Controls.Add(first);
                            else
                                IsTraderButton = false;
                            break;
                        case 2:
                            if (!secondhButtonClick && !thirdButtonClick)
                            {
                                Controls.Add(second);
                                Controls.Add(third);
                            }
                            else
                                IsTraderButton = false;
                            break;
                        case 3:
                            if (!fourthButtonClick && !fifthButtonClick)
                            {
                                Controls.Add(fourth);
                                Controls.Add(fifth);
                            }
                            else
                                IsTraderButton = false;
                            break;
                    }
                }
            };
        }

        private void SetChoice()
        {
            var traderAnimation = new Timer
            {
                Enabled = true,
                Interval = 1
            };
            var firstChoiceButton = new Button
            {
                BackgroundImage = Image.FromFile("FirstChoice.png"),
                FlatStyle = FlatStyle.Popup,
                Size = new Size(206, 223),
                Location = new Point(322, 27),
                BackColor = Color.Transparent
            };
            var secondChoiceButton = new Button
            {
                BackgroundImage = Image.FromFile("SecondChoice.png"),
                FlatStyle = FlatStyle.Popup,
                Size = new Size(206, 223),
                Location = new Point(220, 27),
                BackColor = Color.Transparent
            };
            var thirdChoiceButton = new Button
            {
                BackgroundImage = Image.FromFile("ThirdChoice.png"),
                FlatStyle = FlatStyle.Popup,
                Size = new Size(206, 223),
                Location = new Point(440, 27),
                BackColor = Color.Transparent
            };
            var fourthChoiceButton = new Button
            {
                BackgroundImage = Image.FromFile("FourthChoice.png"),
                FlatStyle = FlatStyle.Popup,
                Size = new Size(206, 223),
                Location = new Point(220, 27),
                BackColor = Color.Transparent
            };
            var fifthChoiceButton = new Button
            {
                BackgroundImage = Image.FromFile("FifthChoice.png"),
                FlatStyle = FlatStyle.Popup,
                Size = new Size(206, 223),
                Location = new Point(440, 27),
                BackColor = Color.Transparent
            };

            ButtonClicked(firstChoiceButton, secondChoiceButton, thirdChoiceButton, fourthChoiceButton, fifthChoiceButton);
            SetTrederChoice(firstChoiceButton, secondChoiceButton, thirdChoiceButton, fourthChoiceButton, fifthChoiceButton, traderAnimation);
        }

        private void ButtonClicked(Button first, Button second, Button third, Button fourth, Button fifth)
        {
            first.Click += (sender, args) =>
            {
                Controls.Remove(first);
                firstButtonClick = true;
                _animationPlayer.PressStart = true;
                _animationPlayer.StartPosition = false;
                IsTraderButton = false;
            };

            second.Click += (sender, args) =>
            {
                Controls.Remove(second);
                Controls.Remove(third);
                secondhButtonClick = true;
                _animationPlayer.slowDowmFrameRate -= 1;
                _animationPlayer.Speed += 1;
                IsTraderButton = false;
            };

            third.Click += (sender, args) =>
            {
                Controls.Remove(second);
                Controls.Remove(third);
                thirdButtonClick = true;
                _animationPlayer.PressThirdChoice = true;
                _animationPlayer.PressFourthChoice = false;
                _animationPlayer.PressStart = false;
                BulletSpeed += 1;
                IsTraderButton = false;
            };

            fourth.Click += (sender, args) =>
            {
                Controls.Remove(fifth);
                Controls.Remove(fourth);
                fourthButtonClick = true;
                _animationPlayer.PressFourthChoice = true;
                _animationPlayer.PressThirdChoice = false;
                _animationPlayer.PressStart = false;
                _animationPlayer.slowDowmFrameRate -= 1;
                _animationPlayer.Speed += 1;
                BulletSpeed += 5;
                IsTraderButton = false;
            };

            fifth.Click += (sender, args) =>
            {
                Controls.Remove(fifth);
                Controls.Remove(fourth);
                fifthButtonClick = true;
                BulletSpeed += 3;
                IsTraderButton = false;
            };
        }

        private void SetTraderImage()
        {
            foreach (var trader in _traders)
                trader.SetTraderImage();
        }

        private void DrawTraderImage(Graphics g)
        {
            foreach (var trader in _traders)
                trader.DrawImage(g);
        }

        private void TraderAnimation()
        {
            foreach (var tard in _traders)
                tard.Animate();
        }
    }
}
