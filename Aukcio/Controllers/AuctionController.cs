using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Aukcio.Models;
using Aukcio.Models.ViewModels;
using Logic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Aukcio.Controllers
{
    [Authorize]
    [ApiController]
    [Route("{controller}")]
    public class AuctionController : ControllerBase
    {
        private UserManager<AuctionUser> _userManager;

        private AuctionLogic logic;

        public AuctionController(AuctionLogic logic, UserManager<AuctionUser> usermanager)
        {
            this.logic = logic;
            _userManager = usermanager;
        }

        [HttpDelete("{uid}")]
        public void DeleteAuction(string uid)
        {
            var auction = logic.GetAuction(uid);
            var claim = this.User.Claims.FirstOrDefault(v => v.Type == "id");
            if (claim != null && auction != null && auction.OwnerId == Guid.Parse(claim.Value))
            {
                logic.DeleteAuction(uid);
            }
        }

        [HttpGet("{uid}")]
        public IActionResult GetAuction(string uid)
        {
            var auc = logic.GetAuction(uid);
            return Ok(new {auc.Description, auc.AuctionName, auc.BuyoutPrice, auc.CurrentBid, auc.OwnerId, auc.UId,});
        }

        [HttpGet]
        public IEnumerable<Auction> GetAllAuctions()
        {
            return logic.GetAllAuctions();
        }

        [HttpPost]
        public void CreateAuction([FromBody] Auction auction)
        {
            var claim = this.User.Claims.FirstOrDefault(v => v.Type == "id");
            if (claim != null)
            {
                auction.OwnerId = Guid.Parse(claim.Value);
                logic.CreateAuction(auction);
            }
        }

        [HttpPut("bid/{uid}")]
        public void PlaceBid(string uid, [FromBody] Bid bid)
        {
            var auction = logic.GetAuction(uid);
            if (!(auction?.CurrentBid < bid.newBid)) return;
            if (auction.BuyoutPrice != null && auction.BuyoutPrice <= bid.newBid)
            {
                logic.DeleteAuction(uid);
            }
            else
            {
                auction.CurrentBid = bid.newBid;
                logic.UpdateAuction(uid, auction);    
            }
        }

        [HttpDelete("bid/{uid}")]
        public void Buyout(string uid)
        {
            var auction = logic.GetAuction(uid);
            if (auction is {BuyoutPrice: { }})
            {
                logic.DeleteAuction(uid);
            }
        }

        [HttpPut("{uid}")]
        public void UpdateAuction(string uid, [FromBody] Auction auction)
        {
            var claim = this.User.Claims.FirstOrDefault(v => v.Type == "id");
            if (claim != null && auction.OwnerId == Guid.Parse(claim.Value))
            {
                logic.UpdateAuction(uid, auction);
            }
        }
    }
}