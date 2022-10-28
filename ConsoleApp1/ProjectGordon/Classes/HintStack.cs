using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ProjectGordon
{
    public class HintStack
    {
        public List<StringRow> Hint = new List<StringRow>();

        public int StartRow
        {
            get => _startRow;
            set => _startRow = Mathf.Clamp(value, 0, 18);
        }
        private int _startRow;

        public int Priority = 400;
        //public CoroutineHandle Handle;
    }
}
