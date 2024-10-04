namespace EgSlackInvite.CloudProvider.Data
{
    using Infrastructure.Configuration;
    using Microsoft.WindowsAzure.Storage;
    using Microsoft.WindowsAzure.Storage.Table;

    public abstract class CloudStorageBase
    {
        protected CloudTableClient CloudTableClient;

        protected CloudStorageBase()
        {
            var storageAccount = CloudStorageAccount.Parse(AzureConfig.AzureStorageConnection);
            CloudTableClient = storageAccount.CreateCloudTableClient();
        }
    }
}
