using WMPLib;
using System;
using System.Windows.Forms;


namespace Pain_and_Stealth
{
    partial class Form1
    {
        private Timer MainSoundTimer;
        private static WindowsMediaPlayer MainSound;
        private static  WindowsMediaPlayer ShootSound;
        private static int TransitionsCount;


        private void SetSound()
        {
            if (TransitionsCount == 0)
            {
                SetMainSound();
                ShootSound = new WindowsMediaPlayer { URL = "Songs\\Sfx.mp3" };
                ShootSound.settings.volume = 30;
            }
        }

        private void PlayShootSound() => ShootSound.controls.play();

        private void SetMainSound()
        {
            MainSoundTimer = new Timer();
            MainSoundTimer.Interval = 10;
            MainSoundTimer.Tick += new EventHandler(MainSoundRepeat);
            MainSound = new WindowsMediaPlayer { URL = "Songs\\MainSound.mp3" };
            MainSound.settings.volume = 30;
            MainSound.PlayStateChange += new _WMPOCXEvents_PlayStateChangeEventHandler(CheckMainMediaState);
            MainSound.controls.play();
        }

        private void MainSoundRepeat(object s, EventArgs e)
        {
            MainSoundTimer.Stop();
            MainSound.controls.play();
        }

        private void CheckMainMediaState(int state)
        {
            if (state == (int)WMPLib.WMPPlayState.wmppsMediaEnded)
                MainSoundTimer.Start();
        }
    }
}
