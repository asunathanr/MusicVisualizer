using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualizerAudio
{
    /// <summary>
    /// From: https://github.com/naudio/NAudio.WaveFormRenderer/tree/master/WaveFormRendererLib
    /// </summary>
    public class RmsPeakProvider : PeakProvider
    {
        private readonly int blockSize;

        public RmsPeakProvider(int blockSize)
        {
            this.blockSize = blockSize;
        }

        public override PeakInfo GetNextPeak()
        {
            var samplesRead = Provider.Read(ReadBuffer, 0, ReadBuffer.Length);

            var max = 0.0f;
            for (int x = 0; x < samplesRead; x += blockSize)
            {
                double total = 0.0;
                for (int y = 0; y < blockSize && x + y < samplesRead; y++)
                {
                    total += ReadBuffer[x + y] * ReadBuffer[x + y];
                }
                var rms = (float)Math.Sqrt(total / blockSize);

                max = Math.Max(max, rms);
            }

            return new PeakInfo(0 - max, max);
        }
    }
}
