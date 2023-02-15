using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectMiro.Framework
{
    /// <summary>
    /// Role of the user on the board.
    /// </summary>
    public enum Role
    {
        viewer,
        commenter,
        editor,
        owner,
        coowner
    }
}
