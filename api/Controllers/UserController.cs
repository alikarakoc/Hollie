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
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
   
    public class UserController : Controller
    {
        private readonly ILogger<UserController> _logger;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        //private readonly JWTConfig _jWTConfig;

        public readonly IConfiguration _configuration;

        public UserController(ILogger<UserController> logger, UserManager<AppUser> userManager, SignInManager<AppUser> signManager, IConfiguration configuration, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signManager;
            _logger = logger;
            _configuration = configuration;
            _roleManager=roleManager;
            //_jWTConfig=jwtConfig.Value;
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<ActionResponse<AddUpdateRegisterUserBindingModel>> Register([FromBody] AddUpdateRegisterUserBindingModel model)
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


                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    actionResponse.IsSuccessful = true;
                    return actionResponse;
                }
                result.Errors.ToList().ForEach(x => actionResponse.Message+=x+",");
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

        [AllowAnonymous]
        [HttpPost("login")]

        public async Task<ActionResponse<TokenDto>> Login([FromBody] loginBindingModel model)
        {
            ActionResponse<TokenDto> actionResponse = new()
            {
                ResponseType = ResponseType.Ok
            };

            try
            {
                var user = new AppUser()
                {
                    UserName=model.FullName.Replace(" ", ""),
                    PasswordHash = model.Password
                    //NormalizedEmail=model.Email.Normalize().ToUpperInvariant(),
                }; 

                //var userX = await _userManager.FindByNameAsync(model.FullName.Replace(" ", ""));

                if (model.FullName != "" || model.Password != "")
                {
                    var result = await _signInManager.PasswordSignInAsync(user.UserName, user.PasswordHash, false, false);
                    //var resultX = await _signInManager.CheckPasswordSignInAsync(userX, model.Password, false);

                    if (result.Succeeded)
                    {
                        
                        actionResponse.IsSuccessful = true;
                        actionResponse.Data = GenerateJwtToken(user);
                        return actionResponse;
                       
                    }
                    //return Unauthorized();

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

        private TokenDto GenerateJwtToken(AppUser user)
        {
            TokenDto tokenUser = new TokenDto();
            tokenUser.Username = user.UserName;
            tokenUser.ExpiryDate = DateTime.UtcNow.AddDays(1);

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration.GetSection("AppSettings:Secret").Value);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]{
                  new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                  new Claim(ClaimTypes.Name, user.UserName)
              }),
                Expires = tokenUser.ExpiryDate,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            
            
            var token = tokenHandler.CreateToken(tokenDescriptor);
            tokenUser.Token = tokenHandler.WriteToken(token);

            return tokenUser;

        }

    }
}
