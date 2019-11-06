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
            float rotationAmount = sampler.NextAveragePeak.Max;
            Visual3D[] allModels = new Visual3D[children.Count];
            children.CopyTo(allModels, 0);
            foreach (var model in allModels)
            {
                RotateTransform3D modelRotation = new RotateTransform3D();
                AxisAngleRotation3D modelAxisAngle = new AxisAngleRotation3D();
                modelAxisAngle.Angle = Math.PI / 2 + rotationAmount * 100000;
                modelAxisAngle.Axis = new Vector3D(0, 3, 0);
                modelRotation.Rotation = modelAxisAngle;
                model.Transform = modelRotation;
            }
        }
    }
}
