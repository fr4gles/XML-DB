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

            var colName = "";
            var colType = "";
            var comAtr = "";
            foreach (var field in fields)
            {
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

        public string MakeSqlInsertValuesDbCommandFrom(IEnumerable<IEnumerable<XElement>> records)
        {
            if (records == null) return null;

            var insertInto = "Insert into " + _tableName + " values\n";

            var insertValue = new List<string>();

            foreach (var record in records)
            {
                var tmpInsertValue = "(";
                try
                {
                    var xElements = record as XElement[] ?? record.ToArray();

                    tmpInsertValue += string.Join(", ", xElements.Select(d => "'"+d.Value+"'"));
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + " | " + ex.StackTrace);
                }
                tmpInsertValue += ")";

                insertValue.Add(tmpInsertValue);
            }

            insertInto += string.Join(",\n", insertValue) + ";\n";

            return insertInto;
        }
    }
}
