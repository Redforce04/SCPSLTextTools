using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectMiro.Framework
{
    /// <summary>
    /// The position object contains location information about the item, such as the x and y coordinates.
    /// </summary>
    public class Position
    {
        /// <summary>
        /// X-axis coordinate of the location of the item on the board.
        /// By default, all items have absolute positioning to the board, not the current viewport.
        /// The center point of the board has x: 0 and y: 0 coordinates.
        /// </summary>
        public float x { get; private set; } = 0.0f;

        /// <summary>
        /// Y-axis coordinate of the center of the item on the board.
        /// By default, all items have absolute positioning to the board, not the current viewport.
        /// The center point of the board has x: 0 and y: 0 coordinates.
        /// </summary>
        public float y { get; private set; } = 0.0f;

        /// <summary>
        /// Point of the item that is referenced by its x and y coordinates.
        /// For example, if an item has a center origin, the corresponding x and y coordinates point to the center of the item.
        /// Currently, we support only center.
        /// </summary>
        public Origin origin { get; private set; } = Origin.center;
    }

    
}
