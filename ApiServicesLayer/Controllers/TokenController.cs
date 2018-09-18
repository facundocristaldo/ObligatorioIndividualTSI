using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Web.Http;
using System.Web.Http.Cors;

namespace ApiServices.Controllers
{
    [EnableCors(origins: "http://localhost:52390", headers: "", methods: "*")]
    public class TokenController : ApiController
    {
        [HttpGet]
        public string CreateToken(string name)
        {
            
                string token = createToken(name);
                //return the token
                return (token);
        }

        private string createToken(string username)
        {
            
            DateTime issuedAt = DateTime.UtcNow;
            
            DateTime expires = DateTime.UtcNow.AddHours(24);

            
            var tokenHandler = new JwtSecurityTokenHandler();

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            });

            const string sec = "yuinbvctruyuh786tyfu786tfyu7t6yuh8656768867tuy767rtyu675r6tyu40293f765eae731f5a65ed1";
            var now = DateTime.UtcNow;
            var securityKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(System.Text.Encoding.Default.GetBytes(sec));
            var signingCredentials = new Microsoft.IdentityModel.Tokens.SigningCredentials(securityKey, Microsoft.IdentityModel.Tokens.SecurityAlgorithms.HmacSha256Signature);


            var token =
                (JwtSecurityToken)
                    tokenHandler.CreateJwtSecurityToken(issuer: "localhost", audience: "localhost",
                        subject: claimsIdentity, notBefore: issuedAt, expires: expires, signingCredentials: signingCredentials);
            var tokenString = tokenHandler.WriteToken(token);

            return tokenString;
        }
    }
}
