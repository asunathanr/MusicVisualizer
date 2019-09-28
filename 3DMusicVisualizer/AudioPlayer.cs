using NAudio.Wave;

namespace _3DMusicVisualizer
{
    public class AudioPlayer
    {
        private AudioFileReader player;
        private WaveOutEvent waveOutEvent;

        public AudioPlayer(string fileName)
        {
            player = new AudioFileReader(fileName);
            waveOutEvent = new WaveOutEvent();
            waveOutEvent.Init(player);
        }

        /// <summary>
        /// Begins playing audio from current track position
        /// </summary>
        public void Play()
        {
            waveOutEvent.Play();
        }

        /// <summary>
        /// Pauses the audio while maintaining track position
        /// </summary>
        public void Pause()
        {
            waveOutEvent.Pause();
        }

        /// <summary>
        /// Stops audio and loses track position.
        /// </summary>
        public void Stop()
        {
            waveOutEvent.Stop();
        }
    }
}
