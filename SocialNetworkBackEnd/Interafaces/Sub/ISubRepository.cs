using SocialNetworkBackEnd.Models.Sub;
using System;

namespace SocialNetworkBackEnd.Interafaces.Sub
{
    public interface ISubRepository
    {
        bool Subscription(SubDB sub);
    }
}
