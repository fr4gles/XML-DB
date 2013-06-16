using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XML_DB
{
    [Serializable]
    public class Settings
    {
        public string databasePath;
        public string password;

        public Settings(string path, string pass)
        {
            password = pass;
            databasePath = path;
        }
    }
}
