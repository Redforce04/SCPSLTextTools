using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectMiro.Framework
{
    public class Items
    {
        public enum Actions
        {
            GetItemsOnBoard,
            GetSpecificItemOnBoard,
            UpdateItemPositionOrParent,
            DeleteItem
        }
    }
}
