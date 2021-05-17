using Logic;
using Microsoft.AspNetCore.Mvc;

namespace Aukcio.Controllers
{
    [ApiController]
    [Route("{controller}")]
    public class DbSeedController : ControllerBase
    {
        private AuctionLogic _logic;

        public DbSeedController(AuctionLogic logic)
        {
            _logic = logic;
        }

        // GET
        [HttpGet]
        public void Index()
        {
            
        }
    }
}