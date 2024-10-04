namespace EgSlackInvite.Infrastructure.Configuration
{
    using System;

    public static class AzureConfig
    {
        public static string AzureStorageConnection
            => Environment.GetEnvironmentVariable("AzureStorageConnection");
    }
}
