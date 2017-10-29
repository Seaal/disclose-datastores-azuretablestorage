using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System.Threading.Tasks;

namespace Disclose.DataStores.AzureTableStorage
{
    public class TableStorageDataStore : IDataStore
    {
        private readonly CloudTableClient _tableClient;
        private readonly TableStorageSettings _tableStorageSettings;

        public TableStorageDataStore(TableStorageSettings tableStorageSettings)
        {
            _tableStorageSettings = tableStorageSettings;
            _tableClient = CloudStorageAccount.Parse(tableStorageSettings.ConnectionString).CreateCloudTableClient();
        }

        public Task<TData> GetServerDataAsync<TData>(DiscloseServer server, string key)
        {
            return GetData<TData>(server.Id, key);
        }

        public Task<TData> GetUserDataAsync<TData>(DiscloseUser user, string key)
        {
            return GetData<TData>(user.Id, key);
        }

        public Task SetServerDataAsync<TData>(DiscloseServer server, string key, TData data)
        {
            return SetData(server.Id, key, data);
        }

        public Task SetUserDataAsync<TData>(DiscloseUser user, string key, TData data)
        {
            return SetData(user.Id, key, data);
        }

        private async Task<TData> GetData<TData>(ulong id, string key)
        {
            CloudTable table = _tableClient.GetTableReference(_tableStorageSettings.TableKey);

            TableOperation retrieveOperation = TableOperation.Retrieve<DiscloseEntity<TData>>(id.ToString(), key);

            TableResult result = await table.ExecuteAsync(retrieveOperation);

            if (result.Result == null)
            {
                return default(TData);
            }

            return (result.Result as DiscloseEntity<TData>).Data;
        }

        private Task SetData<TData>(ulong id, string key, TData data)
        {
            CloudTable table = _tableClient.GetTableReference(_tableStorageSettings.TableKey);

            DiscloseEntity<TData> discloseEntity = new DiscloseEntity<TData>(id, key, data);

            TableOperation setOperation = TableOperation.InsertOrReplace(discloseEntity);

            return table.ExecuteAsync(setOperation);
        }
    }
}
