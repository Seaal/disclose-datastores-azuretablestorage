using Microsoft.WindowsAzure.Storage.Table;
using Newtonsoft.Json;

namespace Disclose.DataStores.AzureTableStorage
{
    public class DiscloseEntity<TData> : TableEntity
    {
        [IgnoreProperty]
        public TData Value {
            get
            {
                return Data == null ? default(TData) : JsonConvert.DeserializeObject<TData>(Data);
            }
            set
            {
                Data = JsonConvert.SerializeObject(value);
            }
        }

        public string Data { get; set; }

        public DiscloseEntity(ulong id, string key, TData data)
        {
            PartitionKey = id.ToString();
            RowKey = key;
            Value = data;
        }

        public DiscloseEntity() { }
    }
}
