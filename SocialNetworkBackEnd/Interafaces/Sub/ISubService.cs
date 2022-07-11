using SocialNetworkBackEnd.Models.ViewModels;
using System;
using System.Collections;
using System.Collections.Generic;

namespace SocialNetworkBackEnd.Interafaces.Sub
{
    public interface ISubService
    {
        bool SubAction(Guid userId, Guid subId);
        IEnumerable<UserMiniView> GetFollowers(Guid id);
        IEnumerable<UserMiniView> GetSubscribers(Guid id);
    }
}
