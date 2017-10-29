using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Disclose.DataStores.AzureTableStorage
{
    public class DiscloseEntity<TData> : TableEntity
    {
        public TData Data { get; set; }

        public DiscloseEntity(ulong id, string key, TData data)
        {
            PartitionKey = id.ToString();
            RowKey = key;
            Data = data;
        }

        public DiscloseEntity() { }
    }
}
