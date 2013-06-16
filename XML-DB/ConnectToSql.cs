using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlServerCe;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XML_DB
{
    public class ConnectToSql
    {
        public List<string> TableList;
        

        public ConnectToSql(string pathToFile)
        {
            var connection = new SqlCeConnection(@"Data Source=" + pathToFile + ";password=xml-db123");

            connection.Open();

            ReadTablesFrom(connection);


        }

        private void ReadTablesFrom(SqlCeConnection connection)
        {
            var tablesQuery = new SqlCeCommand("SELECT TABLE_NAME FROM information_schema.tables", connection);

            var dataAdapter = new SqlCeDataAdapter(tablesQuery);

            var ds = new DataSet();
            dataAdapter.Fill(ds);

            TableList = ManageMsSqlDb.ReadTablesFrom(ds).ToList();
        }

        public void GetDataFromTable(string tableName)
        {
            
        }
    }
}
