using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace GraphicsVisualizer
{
    /// <summary>
    /// An entity that specifies all relevant attributes about a
    /// particular graphical object in a scene.
    /// </summary>
    public class GraphicsComponent
    {
        public readonly int id;
        public Vector3D position;
        public Color color;

        /// <summary>
        /// Chose a color that really stands out in order to let the client know
        /// a color is uninitialized
        /// </summary>
        public readonly static Color DEFAULT_COLOR = Colors.GreenYellow;

        public readonly static Vector3D DEFAULT_POSITION = new Vector3D(0.0f, 0.0f, 0.0f);


        public GraphicsComponent(int id)
        {
            this.id = id;
            position = DEFAULT_POSITION;
            color = DEFAULT_COLOR;
        }

        public GraphicsComponent(int id, Vector3D position, Color color) : this(id)
        {
            this.position = position;
            this.color = color;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || !GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                GraphicsComponent objAsComponent = (GraphicsComponent)obj;
                return objAsComponent.id == id;
            }
        }

        public override int GetHashCode()
        {
            return id.GetHashCode();
        }

    }
}
