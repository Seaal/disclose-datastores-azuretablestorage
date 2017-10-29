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

        public async Task<TData> GetServerDataAsync<TData>(DiscloseServer server, string key)
        {
            CloudTable table = _tableClient.GetTableReference(_tableStorageSettings.ServersKey);

            TableOperation retrieveOperation = TableOperation.Retrieve<DiscloseEntity<TData>>(server.Id.ToString(), key);

            TableResult result = await table.ExecuteAsync(retrieveOperation);

            if (result.Result == null)
            {
                return default(TData);
            }

            return (result.Result as DiscloseEntity<TData>).Data;
        }

        public async Task<TData> GetUserDataAsync<TData>(DiscloseUser user, string key)
        {
            CloudTable table = _tableClient.GetTableReference(_tableStorageSettings.UsersKey);

            TableOperation retrieveOperation = TableOperation.Retrieve<DiscloseEntity<TData>>(user.Id.ToString(), key);

            TableResult result = await table.ExecuteAsync(retrieveOperation);

            if (result.Result == null)
            {
                return default(TData);
            }

            return (result.Result as DiscloseEntity<TData>).Data;
        }

        public Task<TData> GetUserDataForServerAsync<TData>(DiscloseServer server, DiscloseUser user, string key)
        {
            throw new System.NotImplementedException();
        }

        public Task SetServerDataAsync<TData>(DiscloseServer server, string key, TData data)
        {
            CloudTable table = _tableClient.GetTableReference(_tableStorageSettings.UsersKey);

            DiscloseEntity<TData> discloseEntity = new DiscloseEntity<TData>(server.Id, key, data);

            TableOperation setOperation = TableOperation.InsertOrReplace(discloseEntity);

            return table.ExecuteAsync(setOperation);
        }

        public Task SetUserDataAsync<TData>(DiscloseUser user, string key, TData data)
        {
            CloudTable table = _tableClient.GetTableReference(_tableStorageSettings.UsersKey);

            DiscloseEntity<TData> discloseEntity = new DiscloseEntity<TData>(user.Id, key, data);

            TableOperation setOperation = TableOperation.InsertOrReplace(discloseEntity);

            return table.ExecuteAsync(setOperation);
        }

        public Task SetUserDataForServerAsync<TData>(DiscloseServer server, DiscloseUser user, string key, TData data)
        {
            throw new System.NotImplementedException();
        }
    }
}
