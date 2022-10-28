using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectMiro.Framework
{
    /// <summary>
    /// Contains information about the public-level, organization-level, and team-level access for the board. The access level that a user gets depends on the highest level of access that results from considering the public-level, team-level, organization-level, and direct sharing access.
    /// </summary>
    public class SharingPolicy
    {
        /// <summary>
        /// Defines the public-level access to the board.
        /// </summary>
        public Access access { get; private set; } = Access._private;
        /// <summary>
        /// Defines the user role when inviting a user via the invite to team and board link. For Enterprise users, the inviteToAccountAndBoardLinkAccess parameter is always set to no_access regardless of the value that you assign for this parameter.
        /// </summary>
        public InviteToAccountAndBoardLinkAccess inviteToAccountAndBoardLinkAccess { get; private set; } = InviteToAccountAndBoardLinkAccess.viewer;

        /// <summary>
        /// Defines the organization-level access to the board. If the board is created for a team that does not belong to an organization, the organizationAccess parameter is always set to the default value.
        /// </summary>
        public OrganizationAccess orginzationAccess { get; private set; } = OrganizationAccess._private;
        /// <summary>
        /// Defines the team-level access to the board.
        /// </summary>
        public TeamAccess teamAccess { get; private set; } = TeamAccess._private;
    }
}
