using System;

namespace SocialNetworkBackEnd.Interafaces.Sub
{
    public interface ISubService
    {
        bool SubAction(Guid userId, Guid subId);
    }
}
