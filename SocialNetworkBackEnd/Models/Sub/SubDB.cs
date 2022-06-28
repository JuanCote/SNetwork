using System;

namespace SocialNetworkBackEnd.Models.Sub
{
    public class SubDB
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid SubId { get; set; }
        public bool IsActive { get; set; }
        public DateTime SubTime { get; set; }
        public SubDB(Guid id, Guid userId, Guid subId, bool isActive, DateTime subTime)
        {
            Id = id;
            UserId = userId;
            SubId = subId;
            IsActive = isActive;
            SubTime = subTime;
        }
    }
}
