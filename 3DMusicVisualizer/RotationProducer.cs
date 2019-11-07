using NAudio.Wave;
using System.Collections.Generic;
using System.Linq;

namespace _3DMusicVisualizer
{
    public class RotationProducer
    {
        private AudioFileReader rotationProvider;
        private List<long> peaks;
        private RotationConsumer consumer;

        public RotationProducer(AudioFileReader rotationProvider, RotationConsumer consumer)
        {
            this.rotationProvider = rotationProvider;
            this.consumer = consumer;
            peaks = new List<long>();
            SampleAudio();
        }

        public void HandleSample(long position)
        {
            if (peaks.Contains(position))
            {
                consumer.Consume(1.5f);
            }
            else
            {
                consumer.Consume(0.0f);
            }
        }

        public bool CanRotate(long currentSample)
        {
            return peaks.Contains(currentSample);
        }

        private void SampleAudio()
        {
            int sampleLength = 100;
            byte[] buffer = new byte[sampleLength];
            for (long i = 0; i < rotationProvider.Length; i += sampleLength)
            {
                rotationProvider.Read(buffer, 0, sampleLength);
                byte largestSample = buffer.Take(sampleLength).Max();
                byte smallestSample = buffer.Take(sampleLength).Min();
                IEnumerable<byte> rotatingSamples = buffer.Where(sample => sample > (largestSample + smallestSample) / 2);
                foreach (var sample in rotatingSamples)
                {
                    peaks.Add(buffer.ToList().FindIndex(obj => obj == sample) * i);
                }
            }
        }
        
    }
}
