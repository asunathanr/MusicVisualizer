using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualizerAudio
{
    public class AudioFileSubscriber
    {
        private Action onMaxPeak;


        public AudioFileSubscriber(AudioPlayer player, Action onMaxPeak)
        {
            this.onMaxPeak = onMaxPeak;
            player.RegisterReadSubscriber(ProcessAudioBuffer);
        }

        private void ProcessAudioBuffer(object sender, byte[] buffer)
        {
            onMaxPeak();
        }
    }
}
