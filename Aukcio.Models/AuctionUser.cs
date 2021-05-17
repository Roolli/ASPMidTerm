using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Aukcio.Models
{
    public class AuctionUser : IdentityUser<Guid>
    {
        public virtual ICollection<Auction> Auctions { get; set; }

        public AuctionUser()
        {
            Auctions = new List<Auction>();
        }
    }
}