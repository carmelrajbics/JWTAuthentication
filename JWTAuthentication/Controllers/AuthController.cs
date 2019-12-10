using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace JWTAuthentication.Controllers
{
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        // GET: api/<controller>
        [HttpPost("token")]
        public IActionResult Token()
        {
            //validate based on user name & password [optional]
            var header = Request.Headers["Authorization"];
            if (header.ToString().StartsWith("Basic"))
            {
                var credentialValues = header.ToString().Substring("Basic ".Length).Trim();
                var userNameAndPwdCollection = Encoding.UTF8.GetString(Convert.FromBase64String(credentialValues));
                var userNameAndPwd = userNameAndPwdCollection.Split(":");
                if (userNameAndPwd[0] == "Admin" && userNameAndPwd[1].Equals("admin"))
                {
                    return Ok(GenerateJwtToken(userNameAndPwd[0]));
                }
            }
            return Ok("Invalid Token");
        }

        private string GenerateJwtToken(string userNameAndPwd)
        {
            var claimData = new[] { new Claim(ClaimTypes.Name, userNameAndPwd) };   

            string keyFromConfigFiles = "abdasdlkfjldksjfasdlkdslfslkdfjlaskdjflkadjsfal;sdjf";
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(keyFromConfigFiles));

            var signedInCredential = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            var token = new JwtSecurityToken(
                issuer: "eurofins.com",
                audience: "eurofinslab",
                expires: DateTime.Now.AddMinutes(1),
                claims: claimData,
                signingCredentials: signedInCredential
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
