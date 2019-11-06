using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Media3D;

namespace GraphicsVisualizer
{
    /// <summary>
    /// Interface which instantiates and updates 3DModels
    /// </summary>
    interface IVisualizer
    {
        /// <summary>
        /// Creates initial 3D graphics objects.
        /// </summary>
        /// <returns>Top-level Model3DGroup</returns>
        Model3DGroup Initialize(Viewport3D viewPort);

        /// <summary>
        /// Modifies model transformations, adds new models, and removes old models that are no longer needed.
        /// </summary>
        /// <param name="root">Root level model group</param>
        void Update3DModels(Model3DGroup root);
    }
}
