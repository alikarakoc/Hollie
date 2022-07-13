using Application.Concrete;
using Application.Infrastructure;
using DataAccess.Concrate;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelsController : Controller
    {
       
        Context c = new Context();

        [HttpGet]
        [Route("AllHotels")]
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



        [HttpGet("{id}")]
      
        public ActionResponse<Hotel> GetHotel([FromBody] int id)
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

        [HttpPost]
        [Route("AddHotel")]
        public ActionResponse<Hotel> AddHotel([FromBody] Hotel htl)
        {

            ActionResponse<Hotel> actionResponse = new()
            {
                ResponseType = ResponseType.Ok,
                IsSuccessful = true,
            };
            c.Hotels.Add(htl);
            c.SaveChanges();
            return actionResponse;

        }






    }
}
