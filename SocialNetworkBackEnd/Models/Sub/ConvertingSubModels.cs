
using SocialNetworkBackEnd.Models.Sub.ViewModels;
using SocialNetworkBackEnd.Models.ViewModels;
using System;

namespace SocialNetworkBackEnd.Models.Sub
{
    /// <summary>
    /// Конвертация моделей подписки.
    /// </summary>
    public static class ConvertingSubModels
    {
        public static SubView SubDomainToSubView(SubDomain sub, UserMiniView user)
        {
            return new SubView(sub.Id, user);
        }
        public static SubDB SubDomainToSubDB(SubDomain sub)
        {
            return new SubDB(
                sub.Id,
                sub.UserId,
                sub.SubId,
                true,
                DateTime.Now
                );
        }
        public static SubDomain SubDBToSubDomain(SubDB sub)
        {
            return new SubDomain(
                sub.Id,
                sub.UserId,
                sub.SubId
                );
        }
        public static SubDomain SubViewToSubDomain(Guid userId, Guid subId)
        {
            return new SubDomain(
                    Guid.NewGuid(),
                    userId,
                    subId
                    );
        }
    }
}