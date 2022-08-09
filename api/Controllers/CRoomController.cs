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

    public class CRoomController : Controller
    {
        private readonly Context _context;
        public CRoomController(Context context)
        {
            _context = context;

        }

        [HttpGet]
        [Route("AllCRooms")]

        public ActionResponse<List<CRoomList>> GetAllCRoomTypes()
        {
            ActionResponse<List<CRoomList>> actionResponse = new()
            {
                ResponseType = ResponseType.Ok,
                IsSuccessful = true,
            };
            var cRoomList = _context.CRooms;
            if (cRoomList != null && cRoomList.Count() > 0)
            {
                actionResponse.Data = cRoomList.ToList();
            }
            return actionResponse;
        }
    }

}
