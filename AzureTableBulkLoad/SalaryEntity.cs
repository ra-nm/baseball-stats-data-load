using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureTableBulkLoad
{
    public class SalaryEntity : TableEntity
    {
        public SalaryEntity()
        {
            this.RowKey = Guid.NewGuid().ToString();
            this.PartitionKey = "Salary";
        }
        public string LeagueId { get; set; }
        public int YearId { get; set; }
        public string PlayerId { get; set; }
        public string TeamId { get; set; }
        public int Salary { get; set; }
    }

}
