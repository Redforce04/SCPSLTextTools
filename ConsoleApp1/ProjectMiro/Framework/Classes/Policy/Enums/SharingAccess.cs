using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectMiro.Framework
{
    /// <summary>
    /// Defines who can change access and invite users to this board. To change the value for the sharingAccess parameter, contact Miro Customer Support.
    /// </summary>
    public enum SharingAccess
    {
        team_members_with_editing_rights,
        owner_and_coowners
    }
}
