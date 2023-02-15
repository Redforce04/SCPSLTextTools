using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectMiro.Framework
{
    /// <summary>
    /// The geometry object contains geometrical information about the item, such as dimensions and rotation.
    /// </summary>
    public class Geometry
    {
        /// <summary>
        /// Width of the item, in device-independent pixels (dp).
        /// Default value card item: 320.0
        /// </summary>
        public float width { get; private set; } = 320.0f;

        /// <summary>
        /// Height of the item, in device-independent pixels (dp).
        /// Default value card item: 94.0
        /// </summary>
        public float height { get; private set; } = 94.0f;

        /// <summary>
        /// Rotation angle of an item, in degrees, relative to the board.
        /// You can rotate items clockwise(right) and counterclockwise(left) by specifying positive and negative values, respectively.
        /// Note: this property is not applicable to frame items.
        /// </summary>
        public float rotation { get; private set; }
    }
}
