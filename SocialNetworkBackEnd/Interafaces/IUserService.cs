using SocialNetworkBackEnd.Models.User.ViewModels;
using SocialNetworkBackEnd.Models.ViewModels;
using System;
using System.Collections.Generic;

namespace SocialNetworkBackEnd.Interafaces
{
    public interface IUserService
    {
        IEnumerable<UserMiniView> GetUsers();
        UserView GetUserById(Guid id);
        bool AddUser(UserBlank user);
        bool DeleteUser(Guid id);
        bool EditUser(UserBlankEdit user, Guid id);
        string FindUser(UserLogin user);
    }
}
