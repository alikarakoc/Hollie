using Application.Concrete;
using Application.Infrastructure;
using DataAccess.Concrate;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelCategoryController : Controller
    {
        private readonly Context _context;
        public HotelCategoryController(Context _context)
        {
            this._context = _context;
        }
        //Context c = new Context();

        [HttpGet]
        [Route("AllHotelCategory")]
        public  async Task<ActionResponse<List<HotelCategory>>> GetAllHotelCategories()
        {
            ActionResponse<List<HotelCategory>> actionResponse = new()
            {
                ResponseType = ResponseType.Ok,
                IsSuccessful = true,
            };

            var hotelCategories =  _context.HotelCategorys;
            if (hotelCategories != null && hotelCategories.Count() > 0)
            {
                actionResponse.Data = hotelCategories.ToList();
            }
            return actionResponse;
        }

        [HttpPost]
        [Route("AddHotelCategory")]
        public async Task<ActionResponse<HotelCategory>> AddHotelCategory([FromBody]HotelCategory hotelCategory)
        {
            ActionResponse<HotelCategory> actionResponse = new()
            {
                ResponseType = ResponseType.Ok,
                IsSuccessful = true,
            };
            _context.HotelCategorys.Add(hotelCategory);
            _context.SaveChanges();
            return actionResponse;
        }

    }

    


}
