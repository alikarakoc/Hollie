using Application.Concrete;
using Application.Dtos;
using Application.Infrastructure;
using DataAccess.Concrate;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : Controller
    {

        private readonly Context _context;
        public AuthenticationController(Context context)
        {
            this._context = context;
        }

        [HttpPost ("register")]
        public async Task<ActionResponse<Authentication>> Register ([FromBody] AuthenticationDto f)
        {
            ActionResponse<Authentication> actionResponse = new()
            {
                ResponseType = ResponseType.Ok,
                IsSuccessful = true,
            };


            return actionResponse;

         }
    }
}
