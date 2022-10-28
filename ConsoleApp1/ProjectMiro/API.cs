using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ProjectMiro
{
    public class API
    {
        public static API Api { get; set; }
        public static void Enable()
        {
            Api = new API();
        }

        private string _baseURL = "https://api.miro.com/";
        public void MakeAPICall(string location)
        {

        }
    }

    public Enum
    public class Interface
    {
        private string Name { get; set; }
    }
}
