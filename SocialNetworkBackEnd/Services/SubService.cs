using SocialNetworkBackEnd.Interafaces.Sub;
using SocialNetworkBackEnd.Models.Sub;
using SocialNetworkBackEnd.Models.User;
using SocialNetworkBackEnd.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SocialNetworkBackEnd.Services
{
    public class SubService : ISubService
    {
        private readonly ISubRepository _subRepository;
        public SubService(ISubRepository subRepository)
        {
            _subRepository = subRepository;
        }
        public bool SubAction(Guid userId, Guid subId)
        {
            SubResult result = _subRepository.CheckForEntity(userId, subId);
            if (result.SubId != null)
            {
                bool isOk = _subRepository.UpdateSubStatus(result.SubId, !result.isActive);
                return isOk;
            }
            return _subRepository.AddSubscription(ConvertingSubModels.SubDomainToSubDB(
                ConvertingSubModels.SubViewToSubDomain(userId, subId)
                ));
        }
        
        public IEnumerable<UserMiniView> GetFollowers(Guid id)
        {
            IEnumerable<UserDB> users = _subRepository.GetFollowers(id);
            return users.Select(ConvertingUserModels.FromUserDBToUserDomain)
                        .Select(ConvertingUserModels.FromUserDomainToUserMiniView);
        }

        public IEnumerable<UserMiniView> GetSubscribers(Guid id)
        {
            IEnumerable<UserDB> users = _subRepository.GetSubscribers(id);
            return users.Select(ConvertingUserModels.FromUserDBToUserDomain)
                        .Select(ConvertingUserModels.FromUserDomainToUserMiniView);
        }
    }
}
