﻿using SocialNetworkBackEnd.Models.User;
using System;
using System.Collections.Generic;

namespace SocialNetworkBackEnd.Interafaces
{
    public interface IUserRepository
	{
		IEnumerable<UserDB> GetUsers(bool showIsDeleted);
		UserDB GetUserById(Guid id);
		bool AddUser(UserDB user);
		bool DeleteUser(Guid id);
		bool EditUser(UserDB user);
	}
}