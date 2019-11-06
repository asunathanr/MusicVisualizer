using NAudio.Wave;

namespace VisualizerAudio
{
    /// <summary>
    /// Provides statistics gathered from an audio file.
    /// </summary>
    public class AudioStatistics
    {

        public AudioStatistics()
        {
            
        }

        public PeakInfo OverallMaxPeak(AudioFileReader fileReader)
        {
            long originalPosition = fileReader.Position;
            MaxPeakProvider maxPeak = new MaxPeakProvider();
            PeakInfo overallMaxPeak = new PeakInfo(float.MaxValue, float.MinValue);
            PeakInfo nextPeak;
            maxPeak.Init(fileReader, 20);

            return maxPeak.GetNextPeak();
        }
    }
}
