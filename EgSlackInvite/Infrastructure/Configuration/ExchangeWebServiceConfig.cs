namespace EgSlackInvite.Infrastructure.Configuration
{
    using System;

    public static class ExchangeWebServiceConfig
    {
        public static string ServerName 
            => Environment.GetEnvironmentVariable("serverName");

        public static string UserName 
            => Environment.GetEnvironmentVariable("outlookServiceAccountName");

        public static string Password 
            => Environment.GetEnvironmentVariable("outlookServiceAccountPassword");
    }
}
