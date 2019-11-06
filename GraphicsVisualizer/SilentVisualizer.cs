using System.Windows.Controls;
using System.Windows.Media.Media3D;

namespace GraphicsVisualizer
{
    /// <summary>
    /// Given the root level 3DModel group, the SilentVisualizer
    /// will perform graphics updates without considering any audio
    /// that might be playing.
    /// </summary>
    public class SilentVisualizer : IVisualizer
    {
        public static SilentVisualizer MakeSilentVisualizer(Viewport3D viewport)
        {
            var visualizer = new SilentVisualizer();
            PerspectiveCamera camera = new PerspectiveCamera();
            camera.Position = new Point3D(0, 0, 0);
            camera.LookDirection = new Vector3D(0, 0, 1);
            camera.FieldOfView = 70;
            viewport.Camera = camera;
            visualizer.Initialize(viewport);
            return visualizer;
        }

        public Model3DGroup Initialize(Viewport3D viewPort)
        {
            Model3DGroup initialRoot = new Model3DGroup();
            return initialRoot;
        }

        public void Update3DModels(Model3DGroup root)
        {
            
        }
    }
}
