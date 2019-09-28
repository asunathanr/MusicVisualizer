using NAudio.Wave;
using System.Linq;

namespace VisualizerAudio
{
    /// <summary>
    /// From: https://github.com/naudio/NAudio.WaveFormRenderer/tree/master/WaveFormRendererLib
    /// </summary>
    public class MaxPeakProvider : PeakProvider
    {
        public override PeakInfo GetNextPeak()
        {
            var samplesRead = Provider.Read(ReadBuffer, 0, ReadBuffer.Length);
            var max = (samplesRead == 0) ? 0 : ReadBuffer.Take(samplesRead).Max();
            var min = (samplesRead == 0) ? 0 : ReadBuffer.Take(samplesRead).Min();
            return new PeakInfo(min, max);
        }
    }
}
