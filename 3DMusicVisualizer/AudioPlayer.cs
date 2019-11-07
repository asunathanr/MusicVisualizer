using NAudio.Wave;
using System.Collections.Generic;

namespace _3DMusicVisualizer
{
    public class AudioPlayer
    {
        private AudioFileReader player;
        private WaveOutEvent waveOutEvent;
        private List<RotationProducer> producers;

        public AudioPlayer(string fileName)
        {
            player = new AudioFileReader(fileName);
            producers = new List<RotationProducer>();
            waveOutEvent = new WaveOutEvent();
            waveOutEvent.Init(player);
        }

        public void RegisterProducer(RotationProducer producer)
        {
            producers.Add(producer);
        }

        public long CurrentSample => player.Position;

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
