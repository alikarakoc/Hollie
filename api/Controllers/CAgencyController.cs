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
    public class CAgencyController : Controller
    {
        private readonly Context _context;
        public CAgencyController(Context context)
        {
            _context = context;
        }



        [HttpGet]
        [Route("AllCAgencies")]

        public ActionResponse<List<CAgencyList>> GetAllCAgencies()
        {
            ActionResponse<List<CAgencyList>> actionResponse = new()
            {
                ResponseType = ResponseType.Ok,
                IsSuccessful = true,
            };
            var cAgencyList = _context.CAgencies;
            if (cAgencyList != null&& cAgencyList.Count()>0)
            {
                actionResponse.Data= cAgencyList.ToList();
            }
            return actionResponse;
        }




        


    }
}
