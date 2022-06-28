using System;

namespace SocialNetworkBackEnd.Models.Sub
{
    public class SubDomain
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid SubId { get; set; }
        public SubDomain(Guid id, Guid userId, Guid subId)
        {
            Id = id;
            UserId = userId;
            SubId = subId;
        }
    }
}
