using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;

namespace XML_DB
{
    public class XmlToSql
    {
        private string _tableName;

        public XmlToSql(string tableName)
        {
            _tableName = tableName;
        }

        public string MakeSqlCreateDbCommandFrom(IEnumerable<IEnumerable<XElement>> fields)
        {
            if (fields == null) return null;

            var createTable = "Create table " + _tableName + " \n(\n"
                                 + "id int NOT NULL IDENTITY,\n";

            foreach (var field in fields)
            {
                var colName = "";
                var colType = "";
                var comAtr = "";
                try
                {
                    var xElements = field as XElement[] ?? field.ToArray();
                    colName = xElements[0].Value;

                    colType = xElements[1].Value;

                    try
                    {
                        comAtr = xElements[2].Value;
                    }
                    catch (Exception ex)
                    {
                        comAtr = "NULL";
                    }
                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + " | " + ex.StackTrace);
                }

                createTable += colName + " " + colType + " " + comAtr + ",\n";
            }

            createTable += "PRIMARY KEY ( id )\n"
                           + ");\n";

            return createTable;
        }
    }
}
