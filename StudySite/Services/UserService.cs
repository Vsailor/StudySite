using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StudySite.Data;
using StudySite.Data.Models;
using StudySite.Models;
using StudySite.Services.Models;

namespace StudySite.Services
{
    public class UserService
    {
        UserRepository _userRepository = new UserRepository();

        public string SaveUser(UserModel user)
        {
            var userEntity = new UserEntity(user);
            //string existedUserGuid = _userRepository.GetUserGuidByEmail(user.email);
            //if (!string.IsNullOrEmpty(existedUserGuid))
            //{
            //    return existedUserGuid;
            //}

            _userRepository.SaveUser(userEntity);
            return userEntity.Guid.ToString();
        }

        public UserEntity GetUser(string guid)
        {
            User user = _userRepository.GetUser(guid);
            return new UserEntity(user);
        }
    }
}