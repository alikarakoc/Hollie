using Application.BindingModel;
using Application.Concrete;
using Application.Dtos;
using Application.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
   
    public class UserController : Controller
    {
        private readonly ILogger<UserController> _logger;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public readonly IConfiguration _configuration;

        public UserController(ILogger<UserController> logger, UserManager<AppUser> userManager, SignInManager<AppUser> signManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signManager;
            _configuration=configuration;
            _logger = logger;
        }

        [Authorize]
        [HttpPost("RegisterUser")]
        public async Task<ActionResponse<AddUpdateRegisterUserBindingModel>> RegisterUser([FromBody] AddUpdateRegisterUserBindingModel model)
        {
            ActionResponse<AddUpdateRegisterUserBindingModel> actionResponse = new()
            {
                ResponseType = ResponseType.Ok
            };
            try
            {

                var user = new AppUser()
                {
                    FullName = model.FullName,
                    UserName = model.FullName.Replace(" ", ""),
                    Email =model.Email,
                    NormalizedEmail=model.Email.Normalize().ToUpperInvariant(),
                    NormalizedUserName=model.FullName.Normalize().ToUpperInvariant(),
                    DateCreated=DateTime.UtcNow,
                    DateModified=DateTime.UtcNow
                };


                var userCheck = await _userManager.CreateAsync(user, model.Password);
                if (userCheck.Succeeded)
                {
                    actionResponse.IsSuccessful = true;
                    return actionResponse;
                }
                userCheck.Errors.ToList().ForEach(x => actionResponse.Message+=x+",");
                actionResponse.Message = actionResponse.Message.TrimEnd(',');
            }
            catch (Exception ex)
            {
                actionResponse.ResponseType = ResponseType.Error;
                actionResponse.IsSuccessful = false;
                actionResponse.Errors.Add(ex.Message);

            }
            return actionResponse;

        }

        [Authorize]
        [HttpGet("GetAllUser")]
        public async Task<ActionResponse<List<UserDto>>> GetAllUser()
        {
            ActionResponse<List<UserDto>> actionResponse = new()
            {
                ResponseType = ResponseType.Ok
            };
            try
            {
                var users = _userManager.Users.Select(x => new UserDto(x.FullName, x.Email, x.UserName, x.DateCreated)).ToList();
                actionResponse.Data=users;
                actionResponse.IsSuccessful=true;
                return actionResponse;

            }
            catch (Exception ex)
            {
                actionResponse.IsSuccessful=false;

            }
            return actionResponse;


        }

        [Authorize]
        [HttpPost("Login")]

        public async Task<ActionResponse<loginBindingModel>> Login([FromBody] loginBindingModel model)
        {
            ActionResponse<loginBindingModel> actionResponse = new()
            {
                ResponseType = ResponseType.Ok
            };

            try
            {
                var user = new AppUser()
                {
                    UserName=model.FullName.Replace(" ", ""),
                    //NormalizedEmail=model.Email.Normalize().ToUpperInvariant(),
                    PasswordHash = model.Password
                };

                if (model.FullName != "" || model.Password != "")
                {
                    var result = await _signInManager.PasswordSignInAsync(user.UserName, user.PasswordHash, false, false);

                    if (result.Succeeded)
                    {
                        actionResponse.IsSuccessful = true;
                        return actionResponse;
                    }

                }

            }
            catch (Exception ex)
            {
                actionResponse.ResponseType = ResponseType.Error;
                actionResponse.IsSuccessful = false;
                actionResponse.Errors.Add(ex.Message);
            }
            return actionResponse;

        }

        //private string GenerateJwtToken(AppUser user)
        //{
        //    var tokenHandler = new JwtSecurityTokenHandler();
        //    var key = Encoding.ASCII.GetBytes(_configuration.GetSection("AppSettings:Secret").Value);
        //    var tokenDescriptor = new SecurityTokenDescriptor
        //    {
        //      Subject = new ClaimsIdentity(new Claim[]{
        //          new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
        //          new Claim(ClaimTypes.Name, user.FullName)
        //      }),
        //      Expires =DateTime.UtcNow.AddDays(1),
        //      SigninCredentials = new SigninCredentials(new SymetricSecurityKey(key) , SecurityAlgorithms.HmacSha256Signature)
        //    };
        //
        //    var token = tokenHandler.CreateToken(tokenDescriptor);
        //    return tokenHandler.writeToken(token);

        //}
    }
}
