using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectMiro.Framework
{
    /// <summary>
    /// Defines the user role when inviting a user via the invite to team and board link. For Enterprise users, the inviteToAccountAndBoardLinkAccess parameter is always set to no_access regardless of the value that you assign for this parameter.
    /// </summary>
    public enum InviteToAccountAndBoardLinkAccess
    {
        viewer,
        commenter,
        editor,
        no_access
    }
}
