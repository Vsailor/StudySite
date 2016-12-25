using Microsoft.WindowsAzure.Storage.Table;
using StudySite.Services.Models;

namespace StudySite.Data.Models
{
    public class User : TableEntity
    {
        public User(UserEntity user)
        {
            PartitionKey = user.Guid.ToString();
            FullName = user.FullName;
            Name = user.Name;
            FamilyName = user.FamilyName;
            PictureUrl = user.PictureUrl;
            Locale = user.Locale;
            RegistrationDate = user.RegistrationDate;
            RowKey = user.Email;
        }

        public User()
        {
        }

        public string FullName { get; set; }

        public string Name { get; set; }

        public string FamilyName { get; set; }

        public string PictureUrl { get; set; }

        public string Locale { get; set; }

        public string RegistrationDate { get; set; }
    }
}