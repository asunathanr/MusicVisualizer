using System.Windows.Media.Media3D;

namespace GraphicsVisualizer
{
    /// <summary>
    /// Represents an epoch event in the visualizer which instructs the application to
    /// create certain graphics objects at far clipping plane
    /// </summary>
    class Epoch
    {
        private Vector3D spawnPoint;

        public Epoch(Vector3D spawnPoint)
        {
            this.spawnPoint = spawnPoint;
        }
    }
}
