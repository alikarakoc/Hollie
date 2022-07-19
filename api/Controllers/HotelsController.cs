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
    public class HotelsController : Controller
    {
        private readonly Context _context;
        public HotelsController(Context _context)
        {
            this._context = _context;
        }


        //private readonly ILogger<HotelsController> _logger;
        //Context c = new Context();

        //public HotelsController(ILogger<HotelsController> logger)
        //{
        //    _logger = logger;
        //}

        //public IActionResult Index()
        //{
        //    return View();
        //}

        //public IActionResult Privacy()
        //{
        //    return View();
        //}
        //Context c = new Context();

        [HttpGet]
        [Route("AllHotels")]
        public ActionResponse<List<Hotel>> GetAllHotels()
        {
            ActionResponse<List<Hotel>> actionResponse = new()
            {
                ResponseType = ResponseType.Ok,
                IsSuccessful = true,
            };

            var hotels = _context.Hotels;
            //Otelleri Çek await ile
            //eğer hata var ise actionResponse.IsSuccessful=false set edilir.
            //actionResponse.Data = "çekilen otel listesi";
            if (hotels != null && hotels.Count() > 0)
            {
                actionResponse.Data = hotels.ToList();
            }
            return actionResponse;
        }



        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResponse<Hotel>> GetHotel([FromQuery] HotelDto model)
        {
            ActionResponse<Hotel> actionResponse = new()
            {
                ResponseType = ResponseType.Ok,
                IsSuccessful = true,
            };
            var hotel = await _context.Hotels.FirstOrDefaultAsync(h => h.Id == model.Id);
            if (hotel != null)
            {
                actionResponse.Data = hotel;
            }
            return actionResponse;
        }



        [HttpPost]
        [Route("add")]
        public async Task<ActionResponse<Hotel>> AddHotel([FromBody] Hotel htl)
        {

            ActionResponse<Hotel> actionResponse = new()
            {
                ResponseType = ResponseType.Ok,
                IsSuccessful = true,
            };

            var hotelCheck = _context.Hotels.Where(h => h.Name == htl.Name).Count();
            if(hotelCheck < 1)
            {
                _context.Hotels.Add(htl);
                _context.SaveChanges();
            }
            return actionResponse;
        }


        [HttpDelete]
        [Route("delete")]
        public async Task<ActionResponse<Hotel>> DeleteHotel([FromQuery] HotelDto model)
        {
            ActionResponse<Hotel> actionResponse = new()
            {
                ResponseType = ResponseType.Ok,
                IsSuccessful = true,
            };
            var hotel = await _context.Hotels.FirstOrDefaultAsync(h => h.Id == model.Id);
            _context.Hotels.Remove(hotel);
            _context.SaveChanges();
            return actionResponse;
        }

        [HttpPut]
        [Route("update")]
        public async Task<ActionResponse<Hotel>> UpdateHotel([FromQuery] HotelDto modelID, [FromBody] HotelDto model)
        {
            ActionResponse<Hotel> actionResponse = new()
            {
                ResponseType = ResponseType.Ok,
                IsSuccessful = true,
            };

            try
            {
                var hotel = await _context.Hotels.FirstOrDefaultAsync(h => h.Id == modelID.Id);
                if (hotel != null)
                {
                    hotel.Code = model.Code;
                    hotel.Name = model.Name;
                    hotel.Address = model.Address;
                    hotel.Phone = model.Phone;
                    hotel.Email = model.Email;
                    hotel.HotelCategoryId = model.HotelCategoryId;
                    hotel.CreatedDate = model.CreatedDate;
                    hotel.CreatedUser = model.CreatedUser;
                    hotel.UpdatedDate = model.UpdatedDate;
                    hotel.UpdateUser = model.UpdateUser;
                    _context.SaveChanges();
                }
                return actionResponse;
            }
            catch (Exception ex)
            {
                actionResponse.ResponseType = ResponseType.Error;
                actionResponse.IsSuccessful = false;
                actionResponse.Errors.Add(ex.Message);
                return actionResponse;
            }



        }



    }
}
