namespace Disclose.DataStores.AzureTableStorage
{
    public class TableStorageSettings
    {
        public string ConnectionString { get; set; }
        public string TableKey { get; set; }

        public TableStorageSettings()
        {
            TableKey = "disclosedata";
        }
    }
}
