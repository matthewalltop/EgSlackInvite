namespace EgSlackInvite.Services.Abstract
{
    using System;
    using Microsoft.Exchange.WebServices.Data;

    public abstract class OutlookMailServiceBase
    {
        private ExchangeService _service;
        protected ExchangeService Service => _service ?? (_service = CreateConnection());

        private ExchangeService CreateConnection()
        {
            // TODO: Add error handling. Not sure if there's a way to explicitly connect outside of the object constructor.

            var email = Environment.GetEnvironmentVariable("outlookServiceAccountName");
            var password = Environment.GetEnvironmentVariable("outlookServiceAccountPassword");

            var service = new ExchangeService
            {
                Credentials = new WebCredentials(email, password),
                Url = new Uri(Environment.GetEnvironmentVariable("serverName") ?? "https://outlook.office365.com/EWS/Exchange.asmx"),
                TraceEnabled = true,
                TraceFlags = TraceFlags.All
            };
            return service;
        }
    }
}
