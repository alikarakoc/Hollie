using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomeryController : ControllerBase
    {

        [HttpGet]
        [Authorize]
        public IEnumerable<string> Get()
            => new string[] { "Sıla Soyat", "Roxlover" };
    }
}
