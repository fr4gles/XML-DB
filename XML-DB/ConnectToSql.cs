using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlServerCe;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace XML_DB
{
    public class ConnectToSql
    {
        private IEnumerable<string> TableList;

        public bool isEmpty = true;

        private SqlCeConnection connection;

        public ConnectToSql(string pathToFile)
        {
            connection = new SqlCeConnection(@"Data Source=" + pathToFile + ";password=xml-db123");

            connection.Open();

            ReadTablesFrom(connection);


        }

        private void ReadTablesFrom(SqlCeConnection connection)
        {
            var tablesQuery = new SqlCeCommand("SELECT TABLE_NAME FROM information_schema.tables", connection);

            var dataAdapter = new SqlCeDataAdapter(tablesQuery);

            var ds = new DataSet();
            dataAdapter.Fill(ds);

            TableList = ManageMsSqlDb.ReadTablesFrom(ds);

            if (TableList.Any())
                isEmpty = false;
        }

        public IEnumerable<string> GetTablesNames()
        {
            return TableList;
        }

        public DataView GetDataFromTable(string tableName)
        {
            var tmp = new DataTable(tableName);

            var tableQuery = new SqlCeCommand("SELECT * FROM " + tableName, connection);

            var dataAdapter = new SqlCeDataAdapter(tableQuery);

            dataAdapter.Fill(tmp);

            return tmp.DefaultView;
        }
    }
}
