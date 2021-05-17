using System;
using System.Threading.Tasks;
using Aukcio.Models.ViewModels;
using Logic;
using Microsoft.AspNetCore.Mvc;

namespace Aukcio.Controllers
{
    [ApiController]
    [Route("{controller}")]
    public class AuthenticationController : Controller
    {
        private AuthenticationLogic logic;

        public AuthenticationController(AuthenticationLogic logic)
        {
            this.logic = logic;
        }

        [HttpPost]
        public async Task<ActionResult> InsertUser([FromBody] RegisterViewModel model)
        {
            string res = await logic.RegisterUser(model);
            return Ok(new {UserName = res});
        }

        [HttpPut]
        public async Task<ActionResult> Login([FromBody] LoginViewModel model)
        {
            try
            {
                return Ok(await logic.LoginUser(model));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }
    }
}