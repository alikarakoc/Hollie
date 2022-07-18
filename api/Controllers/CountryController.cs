using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        Context _context = new Context();

        [HttpGet]
        [Route("AllBoard")]

        public ActionResponse<List<Board>> GetAllBoards()
        {
            ActionResponse<List<Board>> actionResponse = new()
            {
                ResponseType = ResponseType.Ok,
                IsSuccessful = true,
            };

            return actionResponse;
        }
    }
}
