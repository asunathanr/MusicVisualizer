using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace VisualizerAudio
{
    /// <summary>
    /// Defines an operation interface based on incoming audio
    /// that transforms graphic components
    /// </summary>
    interface IVisualizationTransform
    {
        void Act(GeometryModel3D model);
    }
}
