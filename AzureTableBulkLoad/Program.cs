using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System.Configuration;
using Newtonsoft.Json;
using System.Data;

namespace AzureTableBulkLoad
{
    class Program
    {
        static void Main(string[] args)
        {
            var data = Csv.Load(@"Data\Salaries.csv");

            var storageAccountName = ConfigurationManager.AppSettings.Get("storageAccountName");
            var storageAccountKey = ConfigurationManager.AppSettings.Get("storageAccountKey");

            CloudStorageAccount storageAccount = new CloudStorageAccount(
                new Microsoft.WindowsAzure.Storage.Auth.StorageCredentials(
                storageAccountName, storageAccountKey), true);

            // Create the table client.
            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();

            // Get a reference to a table named "Salary"
            CloudTable salaryTable = tableClient.GetTableReference("Salary");
            salaryTable.CreateIfNotExists();

            // Create the batch operation.
            TableBatchOperation batchOperation = new TableBatchOperation();

            int maxOperations = Microsoft.WindowsAzure.Storage.Table.Protocol.TableConstants.TableServiceBatchMaximumOperations;
            int rowsInBatch = 0;

            foreach (DataRow row in data.Rows)
            {
                var salary = new SalaryEntity()
                {
                    LeagueId = row["lgID"].ToString(),
                    YearId = int.Parse(row["yearID"].ToString()),
                    PlayerId = row["playerID"].ToString(),
                    TeamId = row["teamID"].ToString(),
                    Salary = int.Parse(row["salary"].ToString())
                };
                batchOperation.Insert(salary);

                rowsInBatch++;
                if (rowsInBatch >= maxOperations)
                {
                    salaryTable.ExecuteBatch(batchOperation);
                    batchOperation = new TableBatchOperation();
                    rowsInBatch = 0;
                }
            }

            if (rowsInBatch >= 1)
            {
                salaryTable.ExecuteBatch(batchOperation);
            }
        }
    }
}
