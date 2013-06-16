using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
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

        public static Settings LoadSettingsFromFile()
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream("settingsFile.obj", FileMode.Open, FileAccess.Read, FileShare.Read);
            Settings obj = (Settings)formatter.Deserialize(stream);
            stream.Close();
            return obj;

        }

        public static void SaveSettingsToFile(Settings tempSettings)
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream("settingsFile.obj", FileMode.Create, FileAccess.Write, FileShare.None);
            formatter.Serialize(stream, tempSettings);
            stream.Close();
        }
    }
}
