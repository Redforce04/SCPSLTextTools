using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectMiro.Framework
{
    /// <summary>
    /// Contains information about a user.
    /// </summary>
    public class User
    {
        /// <summary>
        /// Unique identifier (ID) of the user.
        /// </summary>
        public Int64 id { get; private set; }
        /// <summary>
        /// Name of the user.
        /// </summary>
        public string name { get; private set; }
        /// <summary>
        /// Indicates the type of object returned. In this case, type returns User.
        /// </summary>
        public ModelType type { get; private set; } = ModelType.user;
    }
}
