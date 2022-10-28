using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectMiro.Framework
{
    /// <summary>
    ///Contains the <seealso cref="permissionsPolicy"/> and <seealso cref="sharingPolicy"/> for the board.
    /// </summary>
    public class Policy
    {
        /// <summary>
        /// Defines the permissions policies for the board. For more information, see <seealso cref="sharingPolicy"/>.
        /// </summary>
        public PermissionsPolicy permissionsPolicy { get; private set; }

        /// <summary>
        /// Defines the public-level, organization-level, and team-level access for the board. The access level that a user gets depends on the highest level of access that results from considering the public-level, team-level, organization-level, and direct sharing access. For more information, see <seealso cref="sharingPolicy"/>.
        /// </summary>
        public SharingPolicy sharingPolicy { get; private set; }
    }
}
