using NAudio.Wave;

namespace VisualizerAudio
{
    /// <summary>
    /// Reads an audio file and processes its sample.
    /// </summary>
    public class FileFrequencySampler
    {

        private AudioFileReader audioReader;
        private MaxPeakProvider maxPeakProvider;
        private AveragePeakProvider averagePeak;
        private RmsPeakProvider rmsPeak;
        private DecibelPeakProvider decibelPeak;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileName">The file to analyze</param>
        /// <param name="reader">The signal reader to use for analsis</param>
        public FileFrequencySampler(string fileName)
        {
            audioReader = new AudioFileReader(fileName);

            maxPeakProvider = new MaxPeakProvider();
            maxPeakProvider.Init(audioReader, 20);

            averagePeak = new AveragePeakProvider(500.0f);
            averagePeak.Init(audioReader, 20);

            rmsPeak = new RmsPeakProvider(2);
            rmsPeak.Init(audioReader, 20);

            decibelPeak = new DecibelPeakProvider(rmsPeak, 0.02f);

        }


        public PeakInfo NextMaxPeak
        {
            get
            {
                return maxPeakProvider.GetNextPeak();
            }
        }

        public PeakInfo NextRMSPeak
        {
            get
            {
                return rmsPeak.GetNextPeak();
            }
        }

        public PeakInfo NextAveragePeak
        {
            get
            {
                return averagePeak.GetNextPeak();
            }
        }

        public PeakInfo NextDecibalPeak
        {
            get
            {
                return decibelPeak.GetNextPeak();
            }
        }
    }
}
