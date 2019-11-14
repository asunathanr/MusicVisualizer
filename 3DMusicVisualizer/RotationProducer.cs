using NAudio.Wave;
using System.Collections.Generic;
using System.Linq;

namespace _3DMusicVisualizer
{
    public class RotationProducer : IProducer
    {
        private RotationConsumer consumer;

        public RotationProducer(AudioPlayer rotationProvider, RotationConsumer consumer)
        {
            this.consumer = consumer;
        }


        public void HandleSample(long position)
        {

        }

        public float NextValue(long trackPosition)
        {
            return 0.0f;
        }
    }
}
