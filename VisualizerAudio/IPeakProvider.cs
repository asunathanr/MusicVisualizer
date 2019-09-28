using NAudio.Wave;

namespace VisualizerAudio
{
    /// <summary>
    /// From: https://github.com/naudio/NAudio.WaveFormRenderer/blob/master/WaveFormRendererLib/
    /// </summary>
    public interface IPeakProvider
    {
        void Init(ISampleProvider reader, int samplesPerPixel);
        PeakInfo GetNextPeak();
    }
}
