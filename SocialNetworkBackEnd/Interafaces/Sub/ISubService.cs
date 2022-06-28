using System;

namespace SocialNetworkBackEnd.Interafaces.Sub
{
    public interface ISubService
    {
        bool Subscribe(Guid userId, Guid subId);
    }
}
