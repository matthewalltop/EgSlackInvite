namespace EgSlackInvite.Admin.Models
{
    using System.ComponentModel.DataAnnotations;

    public class UserModel
    {
        [DataType(DataType.Text)]
        public string Id { get; set; }
        [DataType(DataType.Text)]
        public string UserName { get; set; }
        [DataType(DataType.Text)]
        public string RealName { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public bool ReceivesEmail { get; set; }
        public bool IsWhiteListed { get; set; }
    }
}
