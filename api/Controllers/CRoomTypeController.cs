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

    public class CRoomTypeController : Controller
    {
        private readonly Context _context;
        public CRoomTypeController(Context context)
        {
            _context = context;

        }

        [HttpGet]
        [Route("AllCRoomTypes")]

        public ActionResponse<List<CRoomTypeList>> GetAllCRooms()
        {
            ActionResponse<List<CRoomTypeList>> actionResponse = new()
            {
                ResponseType = ResponseType.Ok,
                IsSuccessful = true,
            };
            var cRoomTypeList = _context.CRoomTypes;
            if (cRoomTypeList != null && cRoomTypeList.Count() > 0)
            {
                actionResponse.Data = cRoomTypeList.ToList();
            }
            return actionResponse;
        }
    }

}
