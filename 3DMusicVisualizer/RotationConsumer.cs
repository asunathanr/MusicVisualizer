using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
