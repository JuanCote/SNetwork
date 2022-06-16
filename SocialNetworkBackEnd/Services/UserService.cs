﻿using SocialNetworkBackEnd.Interafaces;
using SocialNetworkBackEnd.Models.User;
using SocialNetworkBackEnd.Models.User.ViewModels;
using SocialNetworkBackEnd.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace SocialNetworkBackEnd.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public IEnumerable<UserMiniView> GetUsers()
        {
            IEnumerable<UserDB> users = _userRepository.GetUsers(false);
            return users.Select(ConvertingUserModels.FromUserDBToUserDomain)
                        .Select(ConvertingUserModels.FromUserDomainToUserMiniView);
        }
        public UserView GetUserById(Guid id)
        {
            return ConvertingUserModels.FromUserDomainToUserView(
                ConvertingUserModels.FromUserDBToUserDomain(_userRepository.GetUserById(id))
                );
        }
        public bool AddUser(UserBlank user)
        {
            Guid id = Guid.NewGuid();
            if (user.Avatar != "")
            {
                Regex regexAvatar = new Regex(@"https?:\/\/(www\.)?[-a-zA-Z0-9@:%._\+~#=]{2,256}\.[a-z]{2,4}\b([-a-zA-Z0-9@:%_\+.~#?&//=]*)");
                MatchCollection matches = regexAvatar.Matches(user.Avatar);
                if (matches.Count != 1)
                {
                    return false;
                }
            }
            if (user.Name.Length < 2 || user.Surname.Length < 3)
            {
                return false;
            }
            Regex nameAndSurnameRegex = new Regex(@"\d");
            MatchCollection matchesName = nameAndSurnameRegex.Matches(user.Name);
            MatchCollection matchesSurname = nameAndSurnameRegex.Matches(user.Surname);
            if (matchesName.Count > 0 || matchesSurname.Count > 0)
            {
                return false;
            }
            if (user.Age == 0)
            {
                user.Age = null;
            }
            else if (user.Age > 101 || user.Age < 4)
            {
                return false;
            }
            return _userRepository.AddUser(ConvertingUserModels.InsertConvertUserBlankToUserDb(user, id));
        }
        public bool DeleteUser(Guid id)
        {
            return _userRepository.DeleteUser(id);
        }
        public bool EditUser(UserBlankEdit user, Guid id)
        {
            if (user.Age == 0)
            {
                user.Age = null;
            }
            else if (user.Age > 101 || user.Age < 4)
            {
                return false;
            }
            return _userRepository.EditUser(ConvertingUserModels.EditConvertUserBlankToUserDb(user, id));
        }
        public string FindUser(UserLogin user)
        {
            UserDB? userDB = _userRepository.FindUser(user.Email);
            if (userDB == null) return Constants.EMAIL_NOT_FOUND;
            if (userDB.Password != user.Password) return Constants.PASSWORD_NOT_CORRECT;
            return Constants.GOOD;
        }
    }
}
