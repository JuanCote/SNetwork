using SocialNetworkBackEnd.Models.ViewModels;
using System;

namespace SocialNetworkBackEnd.Models.Sub.ViewModels
{
    public class SubView
    {
        public Guid Id { get; set; }
        public UserMiniView User { get; set; }
        public SubView(Guid id, UserMiniView user)
        {
            Id = id;
            User = user;
        }
    }
}
