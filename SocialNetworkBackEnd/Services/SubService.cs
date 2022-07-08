using SocialNetworkBackEnd.Interafaces.Sub;
using SocialNetworkBackEnd.Models.Sub;
using System;

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
    }
}
