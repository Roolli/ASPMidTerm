using System;

namespace Aukcio.Models.ViewModels
{
    public class TokenViewModel
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
    }
}