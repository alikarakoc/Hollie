using Application.Concrete;
using Application.Dtos;
using Application.Infrastructure;
using DataAccess.Concrate;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace hollie.api.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        Context c = new Context();

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        //[FromQuery] GetAllHotelDto model
        [HttpGet]
        public ActionResponse<List<Hotel>> GetAllHotels()
        {
            ActionResponse<List<Hotel>> actionResponse = new()
            {
                ResponseType = ResponseType.Ok,
                IsSuccessful = true,
            };

            var hotels = c.Hotels;
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
        public ActionResponse<Hotel> GetHotel(int id)
        {
            ActionResponse<Hotel> actionResponse = new()
            {
                ResponseType = ResponseType.Ok,
                IsSuccessful = true,
            };
            var hotel = c.Hotels.FirstOrDefault(h => h.Id == id);
            if (hotel != null)
            {
                actionResponse.Data = hotel;
            }
            return actionResponse;
        }


        //[HttpPost]
        //public ActionResponse<Hotel> AddHotel([FromBody] Hotel htl)
        //{

        //    ActionResponse<Hotel> actionResponse = new()
        //    {
        //        ResponseType = ResponseType.Ok,
        //        IsSuccessful = true,
        //    };
        //    c.Hotels.Add(htl);
        //    return actionResponse;

        //}

    }
}
