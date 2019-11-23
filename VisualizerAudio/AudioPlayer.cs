using NAudio.Wave;
using System;
using VisualizerAudio;

namespace VisualizerAudio
{
    public class AudioPlayer
    {
        private AudioFileObserverStream player;
        private WaveOutEvent waveOutEvent;

        public AudioPlayer(string fileName)
        {
            player = new AudioFileObserverStream(fileName);
            waveOutEvent = new WaveOutEvent();
            waveOutEvent.Init(player);
        }

        public void RegisterReadSubscriber(EventHandler<byte[]> subscriber)
        {
            player.OnBytesRead += subscriber;
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
