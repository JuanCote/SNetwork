using SocialNetworkBackEnd.Models.Sub;
using SocialNetworkBackEnd.Models.User;
using SocialNetworkBackEnd.Models.ViewModels;
using System;
using System.Collections.Generic;

namespace SocialNetworkBackEnd.Interafaces.Sub
{
    public interface ISubRepository
    {
        SubResult CheckForEntity(Guid? userId, Guid subId);
        bool AddSubscription(SubDB sub);
        bool UpdateSubStatus(Guid? id, bool subStatus);
        IEnumerable<UserDB> GetFollowers(Guid id);
        IEnumerable<UserDB> GetSubscribers(Guid id);
    }
}
