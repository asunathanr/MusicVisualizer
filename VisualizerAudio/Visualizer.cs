using System;
using System.Windows.Controls;
using System.Windows.Media.Media3D;

namespace VisualizerAudio
{
    public class Visualizer
    {
        Visual3DCollection children;
        AudioStatistics sampler;

        public Visualizer(Viewport3D viewport, AudioStatistics sampler)
        {
            children = viewport.Children;
            this.sampler = sampler;
            
        }

        public void PerformRotation()
        {
            
        }
    }
}
