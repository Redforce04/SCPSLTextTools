using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectMiro.Framework
{
    /// <summary>
    /// Defines who can start or stop timer, voting, video chat, screen sharing, attention management.
    /// Others will only be able to join. To change the value for the collaborationToolsStartAccess parameter, contact Miro Customer Support.
    /// </summary>
    public enum CollaborationToolsStartAccess
    {
        all_editors,
        board_owners_and_coowners
    }
}
