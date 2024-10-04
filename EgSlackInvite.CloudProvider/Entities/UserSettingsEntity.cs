namespace EgSlackInvite.CloudProvider.Entities
{
    using Microsoft.WindowsAzure.Storage.Table;

    public class UserSettingsEntity : TableEntity
    {
        public string Email { get; set; }
        public string FullName { get; set; }
        public bool ReceivesEmails { get; set; }
        public bool IsWhiteListed { get; set; }
        public UserSettingsEntity(string id,
            string email,
            string fullName,
            bool isWhiteListed,
            bool receivesEmails)
        {
            this.PartitionKey = "EG_Whitelist";
            this.RowKey = id;
            this.Email = email;
            this.FullName = fullName;
            this.ReceivesEmails = receivesEmails;
            this.IsWhiteListed = isWhiteListed;
        }

        public UserSettingsEntity(string id)
        {
            this.PartitionKey = "EG_Whitelist";
            this.RowKey = id;
        }

        public UserSettingsEntity()
        {
            this.PartitionKey = "EG_Whitelist";
        }
    }
}
