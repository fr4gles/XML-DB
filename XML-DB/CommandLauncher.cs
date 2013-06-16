using System;
using System.Collections.Generic;
using System.Data.SqlServerCe;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XML_DB
{
    public class CommandLauncher
    {

        public static void LaunchSqlCommand(string sqlCommand)
        {

            var connection = new SqlCeConnection(@"Data Source=" + MainWindow.mainSettings.databasePath + ";password=" + MainWindow.mainSettings.password);
            connection.Open();
            SqlCeCommand cmd = connection.CreateCommand();
            cmd.CommandText = sqlCommand;
            cmd.ExecuteNonQuery();            
        }

        public static string LaunchSqlCommandWithReturn(string sqlCommand)
        {
            string ret = "";
            var connection = new SqlCeConnection(@"Data Source=" + MainWindow.mainSettings.databasePath + ";password=" + MainWindow.mainSettings.password);
            connection.Open();
            SqlCeCommand cmd = connection.CreateCommand();
            cmd.CommandText = sqlCommand;            

            SqlCeDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                ret += "\n";
                for (int i = 0; i < rdr.FieldCount; i++)
                {
                    ret += rdr[i] + "\t";
                }
            }

            return ret;
        }




    }
}
