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
        public string TableKey { get; set; }

        public TableStorageSettings()
        {
            TableKey = "disclosedata";
        }
    }
}
