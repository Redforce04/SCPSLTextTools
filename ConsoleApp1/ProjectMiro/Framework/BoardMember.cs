using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectMiro.Framework
{
    public class BoardMember
    {
        /// <summary>
        /// Unique identifier of the user.
        /// </summary>
        public string id { get; private set; }
        
        /// <summary>
        /// Name of the user.
        /// </summary>
        public string name { get; private set; }

        /// <summary>
        /// Role of the user on the board.
        /// </summary>
        public Role role { get; private set; }

        /// <summary>
        /// The type of object that is returned. In this case, type returns board_member.
        /// </summary>
        public ModelType type { get; private set; }
    }

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
