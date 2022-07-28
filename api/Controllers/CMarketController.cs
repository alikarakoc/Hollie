using Application.Concrete;
using Application.Infrastructure;
using DataAccess.Concrate;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CMarketController : Controller
    {
        private readonly Context _context;
        public CMarketController(Context context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("AllCMarkets")]

        public ActionResponse<List<CMarketList>> GetAllCMarkets()
        {
            ActionResponse<List<CMarketList>> actionResponse = new()
            {
                ResponseType = ResponseType.Ok,
                IsSuccessful = true,
            };
            var cMarketList = _context.CMarkets;
            if (cMarketList != null && cMarketList.Count() > 0)
            {
                actionResponse.Data = cMarketList.ToList();
            }
            return actionResponse;
        }
    }
}
