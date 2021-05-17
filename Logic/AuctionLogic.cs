using System.Collections.Generic;
using System.Linq;
using Aukcio.Models;
using Aukcio.Repository;

namespace Logic
{
    
    public class AuctionLogic
    {
        private AuctionRepository _repository;

        public AuctionLogic(AuctionRepository repository)
        {
            _repository = repository;
        }

        public Auction GetAuction(string uid)
        {
            return _repository.Read(uid);
        }

        public IQueryable<Auction> GetAllAuctions()
        {
            return _repository.GetAll();
        }

        public void DeleteAuction(string uid)
        {
            _repository.Delete(uid);
        }

        public void CreateAuction(Auction auction)
        {
            _repository.Create(auction);
        }

        public void UpdateAuction(string uid, Auction auction)
        {
            _repository.Update(uid,auction);
        }
    }
}