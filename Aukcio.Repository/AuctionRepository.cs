using System;
using System.Collections.Generic;
using System.Linq;
using Aukcio.Data;
using Aukcio.Models;

namespace Aukcio.Repository
{
    public class AuctionRepository
    {
        private AuctionContext context = new AuctionContext();
        public void Create(Auction auction)
        {
            context.Auctions.Add(auction);
            context.SaveChanges();
        }

        public Auction Read(string uid)
        {
            return context.Auctions.Find(uid);
        }

        public IQueryable<Auction> GetAll()
        {
            return context.Auctions.AsQueryable();
        }

        public void Delete(string uid)
        {
            Delete(Read(uid));
        }

        public void Delete(Auction item)
        {
            context.Remove(item);
            context.SaveChanges();
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void Update(string uid, Auction newItem)
        {
            var old = Read(uid);
            old.Description = newItem.Description;
            old.BuyoutPrice = newItem.BuyoutPrice;
            old.CurrentBid = newItem.CurrentBid;
            old.AuctionName = newItem.AuctionName;
            old.Images.Clear();
            foreach (var item in newItem.Images)
            {
                old.Images.Add(item);
            }

            context.SaveChanges();
        }
        
    }
}