using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualizerAudio
{
    public class ReadEventArgs : EventArgs
    {
        public ReadEventArgs(long nextPosition) :  base()
        {
            NextPosition = nextPosition;
        }

        public long NextPosition { get; }
    }
}
