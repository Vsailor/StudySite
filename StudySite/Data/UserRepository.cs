using System.Linq;
using Microsoft.WindowsAzure.Storage.Table;
using StudySite.Data.Abstract;
using StudySite.Data.Models;
using StudySite.Services.Models;

namespace StudySite.Data
{
    public class UserRepository : AzureTableRepository
    {
        public void SaveUser(UserEntity userEntity)
        {
            Insert(new User(userEntity));
        }

        public User GetUser(string guid)
        {
            var tq = new TableQuery<User>();
            string query = $@"PartitionKey eq '{guid}'";
            tq.Where(query).Take(1);
            User result = CloudTable.ExecuteQuery(tq).FirstOrDefault();
            return result;
        }

        public string GetUserGuidByEmail(string email)
        {
            var tq = new TableQuery<User>();
            string query = $@"RowKey eq '{email}'";
            tq.Where(query).Take(1);
            User result = CloudTable.ExecuteQuery(tq).FirstOrDefault();

            return result != null ? result.PartitionKey : null;
        }

        public UserRepository() : base("Users")
        {
        }
    }
}