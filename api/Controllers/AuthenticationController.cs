using api.Helpers;
using Application.Concrete;
using Application.Dtos;
using Application.Infrastructure;
using DataAccess.Concrate;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : Controller
    {
        private readonly Context _context;
        //public AuthenticationController(Context context)
        //{
        //    this._context = context;
        //}
 
        [HttpPost , Route("login")]
       public IActionResult Login([FromBody] AuthenticationDto user)
        {
            ActionResponse<Authentication> actionResponse = new()
            {
                ResponseType=ResponseType.Ok,
                IsSuccessful=true,
            };

            if (user == null)
                actionResponse.IsSuccessful = false;
            if (user.UserName == "roxlover" && user.Password == "def@123")
            {
                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey@345"));
                var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

                var tokenOptions = new JwtSecurityToken(
                    issuer: "https://localhost:44383",
                    audience: "https://localhost:44383",
                    claims : new List<Claim>(),
                    expires : DateTime.Now.AddMinutes(5),
                    signingCredentials: signingCredentials
                    );
                var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
                return Ok(new { Token = tokenString });
            }

            return Unauthorized();

        }

    }
}
