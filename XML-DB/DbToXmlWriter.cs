﻿using System;
using System.Collections.Generic;
using System.Data.SqlServerCe;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XML_DB
{
    public class DbToXmlWriter
    {
        private static int size;

        public static string CreateXmlBody(string tableName)
        {
            return "<?xml version=\"1.0\" encoding=\"utf-8\" ?>\n<database>\n<table>\n" + CreateStructure(tableName) + CreateRecords(tableName) + " </table>\n</database>\n";
        }

        public static string CreateStructure(string tableName)
        {
            var structure = "<Name>" + tableName + "</Name>\n<Structure>\n";

            using (
                var connection =
                    new SqlCeConnection(@"Data Source=" + MainWindow.mainSettings.databasePath + ";password=" +
                                        MainWindow.mainSettings.password))
            {
                connection.Open();

                var cmd = connection.CreateCommand();
                cmd.CommandText = "select * from " + tableName;

                var reader = cmd.ExecuteReader();

                size = reader.FieldCount;

                for (int i = 0; i < reader.FieldCount; i++)
                {
                    var Field_Name = reader.GetName(i);
                    var Field_Type = reader.GetDataTypeName(i);


                    var cmdIsNullable = connection.CreateCommand();
                    cmdIsNullable.CommandText = @"SELECT IS_NULLABLE FROM INFORMATION_SCHEMA.COLUMNS WHERE (TABLE_Name = '" + tableName +
                                  "' and COLUMN_NAME = '" + Field_Name + "') ORDER BY ORDINAL_POSITION";
                    var isNullable = cmdIsNullable.ExecuteReader();

                    var Field_Atr = "NOT NULL";
                    if (isNullable.GetName(0).Equals("NO"))
                        Field_Atr = null;

                    structure += AddField(Field_Name, Field_Type, Field_Atr);
                }
            }

            structure += "</Structure>\n";

            return structure;
        }

        public static string CreateRecords(string tableName)
        {
            var records = "<Records>\n";

            using (
                var connection =
                    new SqlCeConnection(@"Data Source=" + MainWindow.mainSettings.databasePath + ";password=" +
                                        MainWindow.mainSettings.password))
            {
                connection.Open();
                var cmd = connection.CreateCommand();
                cmd.CommandText = "select * from " + tableName;

                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    records += AddRecord(reader);
                }
            }

            records += "</Records>\n";

            return records;
        }

        private static string AddRecord(SqlCeDataReader reader)
        {
            var record = "<Record>\n";
            for (int i = 0; i < size; i++)
            {
                record += AddRecord(reader, i);
            }
            record += "</Record>\n";
            return record;
        }

        private static string AddRecord(SqlCeDataReader reader, int i)
        {
            var Field_Name = reader.GetName(i);
            return OpenXml(Field_Name) + reader.GetValue(i) + CloseXml(Field_Name) + "\n";
        }

        private static string AddField(string Field_Name, string Field_Type, string Field_Atr)
        {
            var result = "<Field>\n<Field_Name>" + Field_Name + "</Field_Name>\n<Field_Type>" + Field_Type + "</Field_Type>\n";
            if (Field_Atr != null)
                result += "<Field_Atr>" + Field_Atr + "</Field_Atr>\n";
            return result + "</Field>\n";
        }

        private static string OpenXml(string field)
        {
            return "<" + field + ">";
        }

        private static string CloseXml(string field)
        {
            return "</" + field + ">";
        }

    }
}
