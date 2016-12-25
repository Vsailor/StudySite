using System;
using StudySite.Data.Models;
using StudySite.Models;

namespace StudySite.Services.Models
{
    public class UserEntity
    {
        public Guid Guid { get; set; }

        public string Email { get; set; }

        public string FullName { get; set; }

        public string Name { get; set; }

        public string FamilyName { get; set; }

        public string PictureUrl { get; set; }

        public string Locale { get; set; }

        public string RegistrationDate { get; set; }

        public UserEntity(User user)
        {
            Guid = Guid.Parse(user.PartitionKey);
            Email = user.RowKey;
            FullName = user.FullName;
            Name = user.Name;
            FamilyName = user.FamilyName;
            PictureUrl = user.PictureUrl;
            Locale = user.Locale;
            RegistrationDate = user.RegistrationDate;
        }

        public UserEntity(UserModel user)
        {
            Guid = Guid.NewGuid();
            Name = user.name;
            Email = user.email;
            FamilyName = user.family_name;
            FullName = user.name;
            Locale = user.locale;
            PictureUrl = user.picture;
            RegistrationDate = TimeService.GetUnixTime(DateTime.UtcNow).ToString();
        }
    }
}