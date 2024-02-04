using IntegracijskiModul.Modeli;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace IntegracijskiModul.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : Controller
    {
        [HttpPost("[action]")]
        public ActionResult<Tokens> GetToken()
        {
            // token ključ enkodirani
            var tokenKey = Encoding.UTF8.GetBytes("12345678998765432100123456789987");

            // postavi istek i algoritme
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Expires = DateTime.UtcNow.AddMinutes(10),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(tokenKey),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            // jwt
            var tokenHandler = new JwtSecurityTokenHandler();

            // napravi po descriptoru
            var token = tokenHandler.CreateToken(tokenDescriptor);

            // serijaliziraj
            var serializedToken = tokenHandler.WriteToken(token);

            // vrni
            return new Tokens { Token = serializedToken };
        }
    }
}
