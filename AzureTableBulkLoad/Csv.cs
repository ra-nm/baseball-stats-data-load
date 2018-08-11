using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureTableBulkLoad
{
    public static class Csv
    {
        public static DataTable Load(string filePath)
        {
            DataTable dt = new DataTable();

            int counter = 0;
            string line;

            using (StreamReader file = new System.IO.StreamReader(filePath))
            {
                while ((line = file.ReadLine()) != null)
                {
                    //assumption: first row has column names
                    if (counter == 0)
                    {
                        foreach (var column in line.Split(','))
                        {
                            dt.Columns.Add(column);
                        }
                    }
                    else
                    {
                        if (!string.IsNullOrWhiteSpace(line))
                        {
                            var row = dt.Rows.Add();
                            int columnNumber = 0;
                            foreach (var column in line.Split(','))
                            {
                                row[columnNumber] = column;
                                columnNumber++;
                            }
                        }
                    }
                    counter++;
                }
            }

            return dt;
        }
    }
}
