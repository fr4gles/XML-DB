using System;
using System.Collections.Generic;
using System.Data.SqlServerCe;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace XML_DB
{
    public static class CommandLauncher
    {
        public static string LaunchSqlCommand(string sqlCommand, SqlCeConnection connection = null)
        {
            string ret = "";

            try
            {
                if (connection == null)
                {
                    connection = new SqlCeConnection(@"Data Source=" + MainWindow.mainSettings.databasePath + ";password=" + MainWindow.mainSettings.password);
                    connection.Open();
                }

                var cmd = connection.CreateCommand();
                cmd.CommandText = sqlCommand;
                var rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    ret += "\n";
                    for (int i = 0; i < rdr.FieldCount; i++)
                    {
                        ret += rdr[i] + "\t";
                    }
                }
            }
            catch (SqlCeException e)
            {
                MessageBox.Show(e.ToString());
            }

            return ret;
        }
    }
}
