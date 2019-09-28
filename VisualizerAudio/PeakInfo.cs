namespace VisualizerAudio
{
    /// <summary>
    /// From: https://github.com/naudio/NAudio.WaveFormRenderer/tree/master/WaveFormRendererLib
    /// </summary>
    public class PeakInfo
    {
        public PeakInfo(float min, float max)
        {
            Max = max;
            Min = min;
        }

        public float Min { get; private set; }
        public float Max { get; private set; }
    }
}
