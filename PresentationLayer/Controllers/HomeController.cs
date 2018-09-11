using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
       

        public IActionResult Index()
        {
            return View();
        }
        //public IActionResult Create()
        //{
        //    BuildToken("default name");

        //    return View();
        //}

        public IActionResult Error()
        {
            ViewData["RequestId"] = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
            return View();
        }

        //private IActionResult BuildToken(string conn)
        //{
        //    try
        //    {
        //        var claims = new[]
        //        {
        //        new Claim("name",conn),
        //        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        //    };

        //        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Llave_super_secreta"]));
        //        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        //        var expiration = DateTime.UtcNow.AddMinutes(45);

        //        JwtSecurityToken token = new JwtSecurityToken(
        //                issuer: "localhost",
        //                audience: "localhost",
        //                claims: claims,
        //                expires: expiration,
        //                signingCredentials: creds
        //            );

        //        return Ok(new
        //        {
        //            token = new JwtSecurityTokenHandler().WriteToken(token),
        //            expiration = expiration
        //        });
        //    }
        //    catch (Exception e)
        //    {
        //        return BadRequest("No body in requese" + e.Message.ToString());
        //    }

        //}

    }
}
