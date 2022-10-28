using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp1;

namespace ProjectGordon
{
    public class API
    {
        public static API Api;

        public static void Enable()
        {
            Api = new API();
        }

        internal readonly bool Debug = true;
        internal Dictionary<string, List<HintStack>> PlayerHintStack = new Dictionary<string, List<HintStack>>();
        internal Dictionary<int, string> _messageArray = new Dictionary<int, string>();

        internal readonly List<List<string>> StringArray = new List<List<string>>()
        {
            new List<string>() { "L", "1", "R" }, new List<string>() { "L", "2", "R" },
            new List<string>() { "L", "3", "R" },
            new List<string>() { "L", "4", "R" }, new List<string>() { "L", "5", "R" },
            new List<string>() { "L", "6", "R" },
            new List<string>() { "L", "7", "R" }, new List<string>() { "L", "8", "R" },
            new List<string>() { "L", "9", "R" },
            new List<string>() { "L", "10", "R" }, new List<string>() { "L", "11", "R" },
            new List<string>() { "L", "12", "R" },
            new List<string>() { "L", "13", "R" }, new List<string>() { "L", "14", "R" },
            new List<string>() { "L", "15", "R" },
            new List<string>() { "L", "16", "R" }, new List<string>() { "L", "17", "R" },
            new List<string>() { "L", "18", "R" }
        };

        internal readonly Dictionary<char, int> CharacterWidth = new Dictionary<char, int>()
        {
            { 'A', 1279 }, { 'B', 1255 }, { 'C', 1330 }, { 'D', 1341 }, { 'E', 1165 }, { 'F', 1152 },
            { 'G', 1400 }, { 'H', 1449 }, { 'I', 545  }, { 'J', 1127 }, { 'K', 1292 }, { 'L', 1079 },
            { 'M', 1772 }, { 'N', 1454 }, { 'O', 1386 }, { 'P', 1261 }, { 'Q', 1386 }, { 'R', 1300 },
            { 'S', 1213 }, { 'T', 1223 }, { 'U', 1346 }, { 'V', 1263 }, { 'W', 1836 }, { 'X', 1253 },
            { 'Y', 1226 }, { 'Z', 1225 }, { '1', 1135 }, { '2', 1135 }, { '3', 1135 }, { '4', 1135 },
            { '5', 1135 }, { '6', 1135 }, { '7', 1135 }, { '8', 1135 }, { '9', 1135 }, { '0', 1135 },
            { ' ', 498  }, { '|', 452  }, { '[', 491  }, { ']', 491  }, { '{', 676  }, { '}', 676  },
            { '(', 653  }, { ')', 653  }, { '+', 1156 }, { '-', 586  }, { '_', 884  }, { '=', 1133 },
            { '`', 585  }, { '~', 1402 }, { '\\', 807 }, { '/', 813  }, { '.', 489  }, { ',', 392  },
            { '<', 1047 }, { '>', 1061 }, { '!', 462  }, { '@', 1870 }, { '#', 1191 }, { '$', 1135 },
            { '%', 1513 }, { '^', 852  }, { '&', 1260 }, { '*', 869  }, { '?', 930  }, { ':', 430  },
            { ';', 399  }, { '\'', 348 }, { '"', 588  }, { '©', 1637 }, { '®', 1642 }, { ' ', 498  },
            { '░', 1000 }, { '▒', 1000 }, { '▓', 1000 }, { '█', 1000 }
        }; // JJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJCCCCCCCCCCCCCCCCCCCCCCCCIIIIIIIIIII

        internal readonly int MaxLineSize = 70209;
        private readonly float _scaleFactor = 35f;
        internal readonly float Height = 1536;

        public void ShowHintBypass(string ply, string hint, float duration)
        {
            //ply.ShowHint(hint, duration);
        }

        public void ProcessPlayerHints(string ply, int Hint, bool Expired = false)
        {
            List<HintStack> stack = new List<HintStack>();
            if (Expired)
            {
                stack = PlayerHintStack[ply];
                var expired = stack[Hint];

            }
        }

        /*
         * 0  - 
         * 1  - 
         * 2  - 
         * 3  - 
         * 4  - 
         * 5  - 
         * 6  - 
         * 7  - 
         * 8  - 
         * 9  - 
         * 10 - 
         * 11 - 
         * 12 - 
         * 13 - 
         * 14 - 
         * 15 - 
         * 16 - 
         * 17 - 
         */
        public string ProcessHint(List<HintStack> hintStack)
        {
            // Output Array is 18
            Dictionary<int, string> outputArray = new Dictionary<int, string>(18);
            foreach (var hint in hintStack.OrderByDescending(x => x.Priority))
            {
                // Iterated for each hint
                int ActualRow = -1; // what row will actually be the start for this hint
                //Log.Debug($"Hint StartRow: {hint.StartRow}, OutputArrayCapacity: {outputArray.Count}", Debug);
                for (int i = hint.StartRow; i < 18; i++) // check if the set of rows required is empty
                {
                    bool rowisempty = !outputArray.ContainsKey(i);

                    if (!rowisempty) // if row is empty, start checking the remainder of the rows. otherwise move to the next row until we find a row or run out.
                    {
                        Log.Debug($"i: {i}, row is empty", Debug);
                        continue;
                    }

                    bool works = false;
                    int rowsneeded = hint.Hint.Count; // how many empty rows the next for loop is checking for
                    for (int x = 1; x < rowsneeded; x++)
                    {
                        if (outputArray.ContainsKey(i + x)) // if this row is empty, check the next row.
                            continue;
                        // if the row is taken, the whole thing must start later. stop this check, and the whole row will iterate one more and recheck
                        works = true;
                        break;
                    }

                    if (works) // if this is good placement, stop searching for this hint.
                    { 
                        Log.Debug($"i: {i}, is actual row", Debug);
                        ActualRow = i; // set actual start row for this hint
                        break;
                    }
                        Log.Debug($"i: {i}, not working placement", Debug);

                }
                // check to see if this hint has room to be displayed. if not, we wont display it for now
                if (ActualRow != -1)
                {
                    Log.Debug($"found working placement for hint at : {ActualRow}", Debug);
                    for (int z = 0; z < hint.Hint.Count; z++) // set the actual output for this hint
                    {
                        outputArray.Add(ActualRow + z, StringRow.MonoSpace(hint.Hint[z].Text.FirstOrDefault(""))); // add the output
                    }
                }
            }
            

            string result = ""; // format and generate the actual hint.
            for (int str = 0; str < 18; str++)
            {
                if (!outputArray.ContainsKey(str)) 
                {
                    result += "<br>";
                    continue;
                }
                
                result += outputArray[str] + "<br>"; // empty output shouldnt matter
            }

            return result;
        }
    }
}
