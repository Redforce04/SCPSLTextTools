using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectMiro.Framework
{
    public static class AccessEnum
    {
        public static Dictionary<string, Access> AccessDictionary { get; set; } = new Dictionary<string, Access>()
        {
            { "private", Access._private },
            { "_private", Access._private },
            { "view", Access.view },
            { "comment", Access.comment },
            { "edit", Access.edit }
        };

        public static Access GetAccessEnum(string input)
        {
            if (!AccessDictionary.ContainsKey(input.ToLower()))
                throw new ArgumentNullException(input, "Access does not contain this enum.");
            return AccessDictionary[input.ToLower()];
        }
        /// <summary>
        /// Defines the public-level access to the board.
        /// </summary>
        public enum Access
        {
            _private,
            view,
            comment,
            edit
        }
    }
    
}
