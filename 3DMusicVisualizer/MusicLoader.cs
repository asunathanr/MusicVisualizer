using System;
using System.Windows.Media;

namespace _3DMusicVisualizer
{
    /// <summary>
    /// Loads sound files from a directory
    /// </summary>
    public class MusicLoader
    {

        private MediaPlayer mMusicPlayer;

        public MusicLoader(MediaPlayer musicPlayer, Uri audioLocation)
        {
            mMusicPlayer = musicPlayer;
            mMusicPlayer.Open(audioLocation);
        }

        public MediaPlayer MusicPlayer
        {
            get => mMusicPlayer;
        }
    }
}
