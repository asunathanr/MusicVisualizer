using System.Windows;

namespace _3DMusicVisualizer
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static AudioPlayer audio;

        public static void Play()
        {
            PlayMusic();
        }

        public static void Pause()
        {
            PauseMusic();
        }

        public static void ChangeTrack(string trackName)
        {
            Pause();
            audio = new AudioPlayer(trackName);
            Play();
        }

        public static void PauseMusic()
        {
            audio.Pause();
        }

        public static void PlayMusic()
        {
            audio.Play();
        }
    }
}
