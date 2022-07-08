using SocialNetworkBackEnd.Models.User.ViewModels;
using SocialNetworkBackEnd.Models.ViewModels;
using System;

namespace SocialNetworkBackEnd.Models.User
{
    /// <summary>
    /// Конвертация моделей пользователя.
    /// </summary>
    public static class ConvertingUserModels
    {
        public static UserDB FromUserDomainToUserDB(UserDomain user)
        {
            return new UserDB(
                user.Id,
                user.Name,
                user.Surname,
                user.Age,
                user.Avatar,
                user.Description,
                user.Status,
                null,
                null,
                false,
                null,
                null
                );
        }
        public static UserDomain FromUserDBToUserDomain(UserDB user)
        {
            return new UserDomain(
                user.Id,
                user.Name,
                user.Surname,
                user.Age,
                user.Avatar,
                user.Description,
                user.Status,
                user.IsDeleted,
                user.Email,
                user.Password
                );
        }
        public static UserView FromUserDomainToUserView(UserDomain user)
        {
            return new UserView(
                user.Id,
                user.Name,
                user.Surname,
                user.Age,
                user.Avatar,
                user.Description,
                user.Status
                );
        }
        public static UserDB InsertConvertUserBlankToUserDb(UserBlank user, Guid id)
        {
            return new UserDB(
                id,
                user.Name,
                user.Surname,
                user.Age,
                user.Avatar,
                user.Description,
                user.Status,
                null,
                DateTime.Now,
                false,
                user.Email, 
                user.Password
                );
        }
        public static UserDB EditConvertUserBlankToUserDb(UserBlankEdit user, Guid id)
        {
            return new UserDB(
                id,
                user.Name,
                user.Surname,
                user.Age,
                user.Avatar,
                user.Description,
                user.Status,
                DateTime.Now,
                null,
                false,
                null,
                null
                );
        }

        public static UserMiniView FromUserDomainToUserMiniView(UserDomain user)
        {
            return new UserMiniView(
                user.Id,
                user.Name,
                user.Surname,
                user.Age,
                user.Avatar,
                user.Status
                );
        }
    }
}