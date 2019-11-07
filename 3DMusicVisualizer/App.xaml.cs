using System.Windows;
using System.Windows.Controls;
using VisualizerAudio;

namespace _3DMusicVisualizer
{
    /// <summary>
    /// Gatekeeper to all visualization logic.
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

        public static long CurrentSample()
        {
            return audio?.CurrentSample ?? 0;
        }

        public static void ChangeTrack(string trackName, Viewport3D model)
        {
            Pause();
            audio = new AudioPlayer(trackName);
            Play();
        }

        public static void PauseMusic()
        {
            if (audio != null)
            {
                audio.Pause();
            }
        }

        public static void PlayMusic()
        {
            if (audio != null)
            {
                audio.Play();
            }
            
        }
    }
}
