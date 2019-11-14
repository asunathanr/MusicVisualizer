using System;

namespace _3DMusicVisualizer
{
    public class RotationConsumer
    { 
        private Action<float> consumerAction;

        public RotationConsumer(Action<float> consumerAction)
        {
            this.consumerAction = consumerAction ?? throw new NullReferenceException();
        }

        public void Consume(float value)
        {
            consumerAction(value);
        }
    }
}
