using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectMiro.Framework
{
    public class Team
    {
        /// <summary>
        /// Unique identifier (ID) of the team.
        /// </summary>
        public Int64 id { get; private set; }
        /// <summary>
        /// Name of the team.
        /// </summary>
        public string name { get; private set; } = "";

        /// <summary>
        /// Indicates the type of object returned. In this case, type returns team.
        /// </summary>
        public ModelType type { get; private set; } = ModelType.team;
    }
}
