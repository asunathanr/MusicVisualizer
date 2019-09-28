using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using VisualizerAudio;

namespace FrequencyAnalysisApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static FileFrequencySampler sampler;

        public static void ReadSamples()
        {
            Console.WriteLine("Reading samples");

            sampler = new FileFrequencySampler(
                "E:\\School\\Graphics Programming\\FinalProject\\3DMusicVisualizer\\media\\rickRolled.wav"
            );
            for (int i = 0; i < 20; ++i)
            {
                Console.WriteLine(sampler.NextAveragePeak.Max);
                System.Threading.Thread.Sleep(20);
            }
            
        }
    }
}
