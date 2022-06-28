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
        public bool Subscribe(Guid userId, Guid subId)
        {
            return _subRepository.Subscription(ConvertingSubModels.SubDomainToSubDB(
                ConvertingSubModels.SubViewToSubDomain(userId, subId)
                ));
        }
    }
}
