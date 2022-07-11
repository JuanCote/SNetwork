using SocialNetworkBackEnd.Models.User;
using System;
using System.Collections.Generic;

namespace SocialNetworkBackEnd.Interafaces.User
{
    public interface IUserRepository
    {
        IEnumerable<UserDB> GetUsers(bool showIsDeleted);
        UserDB GetUserById(Guid id);
        bool AddUser(UserDB user);
        bool DeleteUser(Guid id);
        bool EditUser(UserDB user);
        UserDB FindUser(string email);
        bool AdminCheck(Guid id);
        int GetSubCount(Guid id);
        int GetFollowersCount(Guid id);
    }
}
