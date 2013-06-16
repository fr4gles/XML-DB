using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XML_DB
{
    public static class ManageMsSqlDb
    {
        public static IEnumerable<string> ReadTablesFrom(DataSet ds)
        {
            foreach (DataTable dt in ds.Tables)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    foreach (DataColumn column in dt.Columns)
                    {
                        yield return dr[column.ToString()].ToString();
                    }
                }
            }
        }

        public static void ReadStructureFrom()
        {
            
        }
    }
}
