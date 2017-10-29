using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Disclose.DataStores.AzureTableStorage
{
    public class TableStorageSettings
    {
        public string ConnectionString { get; set; }
        public string ServersKey { get; set; }
        public string UsersKey { get; set; }

        public TableStorageSettings()
        {
            ServersKey = "servers";
            UsersKey = "users";
        }
    }
}
