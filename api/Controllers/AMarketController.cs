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
    public class AMarketController : Controller
    {
        private readonly Context _context;
        public AMarketController(Context context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("AllAMarkets")]

        public ActionResponse<List<MarketListA>> GetAllAMarkets()
        {
            ActionResponse<List<MarketListA>> actionResponse = new()
            {
                ResponseType = ResponseType.Ok,
                IsSuccessful = true,
            };
            var aMarketList = _context.AMarkets;
            if (aMarketList != null && aMarketList.Count() > 0)
            {
                actionResponse.Data = aMarketList.ToList();
            }
            return actionResponse;
        }
    }
}
