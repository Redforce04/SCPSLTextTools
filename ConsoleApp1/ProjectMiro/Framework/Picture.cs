using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectMiro.Framework
{
    public class Picture
    {
        /// <summary>
        /// Unique identifier (ID) of the cover picture for the board.
        /// </summary>
        public Int64 id { get; private set; }

        /// <summary>
        /// URL of the cover picture of the board.
        /// </summary>
        public string imageUrl { get; private set; }
    }
}
