using SocialNetworkBackEnd.Interafaces;
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
        public string AddUser(UserBlank user)
        {
            Guid id = Guid.NewGuid();
            if (user.Avatar != "")
            {
                Regex regexAvatar = new Regex(@"https?:\/\/(www\.)?[-a-zA-Z0-9@:%._\+~#=]{2,256}\.[a-z]{2,4}\b([-a-zA-Z0-9@:%_\+.~#?&//=]*)");
                MatchCollection matches = regexAvatar.Matches(user.Avatar);
                if (matches.Count != 1)
                {
                    return "Не валлидная ссылка на фото";
                }
            }
            if (user.Name.Length < 2 || user.Surname.Length < 3)
            {
                return "Имя или фамилия слишком короткие";
            }
            Regex nameAndSurnameRegex = new Regex(@"\d");
            MatchCollection matchesName = nameAndSurnameRegex.Matches(user.Name);
            MatchCollection matchesSurname = nameAndSurnameRegex.Matches(user.Surname);
            if (matchesName.Count > 0 || matchesSurname.Count > 0)
            {
                return "В имене или фамилии содержатся недопустимые символы";
            }
            if (user.Age == 0)
            {
                user.Age = null;
            }
            else if (user.Age > 101 || user.Age < 4)
            {
                return "Не корректный возраст";
            }
            if (!user.Email.Contains("@")) return "Адрес почты неверный";
            if (user.Password.Length < 5) return "Пароль слишком короткий";
            bool result = _userRepository.AddUser(ConvertingUserModels.InsertConvertUserBlankToUserDb(user, id));
            return result ? Constants.GOOD : "Такой адрес электронной почты существует";
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
        public LoginResult FindUser(UserLogin user)
        {
            UserDB? userDB = _userRepository.FindUser(user.Email);
            LoginResult result = new LoginResult();
            if (userDB == null)
            {
                result.status = Constants.EMAIL_NOT_FOUND;
                result.user = null;
                return result;
            }
            if (userDB.Password != user.Password)
            {
                result.status = Constants.PASSWORD_NOT_CORRECT;
                result.user = null;
                return result;
            }
            result.status = Constants.GOOD;
            result.user = ConvertingUserModels.FromUserDomainToUserView(ConvertingUserModels.FromUserDBToUserDomain(userDB));
            return result;
        }
    }
}
