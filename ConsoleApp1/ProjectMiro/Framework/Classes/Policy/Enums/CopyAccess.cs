using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectMiro.Framework
{
    /// <summary>
    /// Defines who can copy the board, copy objects, download images, and save the board as a template or PDF.
    /// </summary>
    public enum CopyAccess
    {
        anyone,
        team_members,
        team_editors,
        board_owner
    }
}
