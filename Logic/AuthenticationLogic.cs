using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Aukcio.Models;
using Aukcio.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace Logic
{
    public class AuthenticationLogic
    {
        private UserManager<AuctionUser> _userManager;
        private RoleManager<IdentityRole<Guid>> _roleManager;

        public AuthenticationLogic(UserManager<AuctionUser> userManager, RoleManager<IdentityRole<Guid>> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<string> RegisterUser(RegisterViewModel model)
        {
            var user = new AuctionUser
            {
                Email = model.Email,
                UserName = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
            };
            var res = await _userManager.CreateAsync(user, model.Password);
            if (res.Succeeded)
            {
                //  await _userManager.AddToRoleAsync(user, "Admin");
            }

            return user.UserName;
        }

        public async Task<TokenViewModel> LoginUser(LoginViewModel model)
        {
            var user = await _userManager.FindByNameAsync(model.Username);
            if (user == null || !await _userManager.CheckPasswordAsync(user, model.Password))
                throw new ArgumentException("Login failed!");
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, model.Username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("id", user.Id.ToString())
            };
            var roles = await _userManager.GetRolesAsync(user);

            claims.AddRange(roles.Select(r => new Claim(ClaimsIdentity.DefaultRoleClaimType, r)));
            var signingKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes("Very secure symmetric key"));

            var token = new JwtSecurityToken(
                issuer: "http://www.security.org",
                audience: "http://www.security.org",
                claims: claims,
                expires: DateTime.Now.AddMinutes(60),
                signingCredentials: new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256)
            );
            return new TokenViewModel()
            {
                Expiration = token.ValidTo,
                Token = new JwtSecurityTokenHandler().WriteToken(token),
            };
        }
    }
}