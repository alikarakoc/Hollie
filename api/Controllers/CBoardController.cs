using Application.Concrete;
using Application.Infrastructure;
using DataAccess.Concrate;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;



namespace api.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class CBoardController : Controller
    {
        private readonly Context _context;
        public CBoardController(Context context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("AllCBoard")]

        public ActionResponse<List<CBoardList>> GetAllCBoards()
        {
            ActionResponse<List<CBoardList>> actionResponse = new()
            {
                ResponseType = ResponseType.Ok,
                IsSuccessful = true,
            };
            var cBoardList = _context.CBoards;
            if (cBoardList != null&& cBoardList.Count()>0)
            {
                actionResponse.Data = cBoardList.ToList();
            }
            return actionResponse;
        }
    }
}
