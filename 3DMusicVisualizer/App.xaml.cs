using Numpy;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Media;

namespace _3DMusicVisualizer
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static SoundPlayer player = new SoundPlayer("E:\\School\\Graphics Programming\\FinalProject\\3DMusicVisualizer\\media\\rickRolled.wav");

        public static void PauseMusic()
        {
            player.Stop();
        }

        public static void PlayMusic()
        {
            player.Play();
        }
    }
}
