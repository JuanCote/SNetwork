using SocialNetworkBackEnd.Models.Sub;
using System;

namespace SocialNetworkBackEnd.Interafaces.Sub
{
    public interface ISubRepository
    {
        SubResult CheckForEntity(Guid? userId, Guid subId);
        bool AddSubscription(SubDB sub);
        bool UpdateSubStatus(Guid? id, bool subStatus);
    }
}
