using System;
using VisualizerAudio;

namespace FrequencyAnalysis
{
    /// <summary>
    /// Displays information on frequency samples obtained from an audio file.
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            FileFrequencySampler sampler = new FileFrequencySampler(
                "E:\\School\\Graphics Programming\\FinalProject\\3DMusicVisualizer\\media\\rickRolled.wav"
            );
            Action action = () =>
            {
                double[][] samples = sampler.Samples;
                Console.WriteLine(samples[0][0]);
                Console.WriteLine("Press any key to continue");
                Console.ReadKey();
            };
            sampler.InvokeEventWhenSamplesAreReady = action;
        }
    }
}
