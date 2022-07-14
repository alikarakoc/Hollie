using Application.Concrete;
using Application.Dtos;
using Application.Infrastructure;
using DataAccess.Concrate;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
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



        [HttpGet]
        public ActionResponse<Hotel> GetHotel([FromQuery] GetAllHotelDto model)
        {
            ActionResponse<Hotel> actionResponse = new()
            {
                ResponseType = ResponseType.Ok,
                IsSuccessful = true,
            };
            var hotel = c.Hotels.FirstOrDefault(h => h.Id == model.Id);
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

        [HttpDelete]
        [Route("DeleteHotel")]
        public ActionResponse<Hotel> DeleteHotel([FromQuery] GetAllHotelDto model)
        {
            ActionResponse<Hotel> actionResponse = new()
            {
                ResponseType = ResponseType.Ok,
                IsSuccessful = true,
            };
            var hotel = c.Hotels.FirstOrDefault(h => h.Id == model.Id);
            c.Hotels.Remove(hotel);
            c.SaveChanges();
            return actionResponse;
        }

        [HttpPut]
        [Route("UpdateHotel")]
        public ActionResponse<Hotel> UpdateHotel([FromQuery] GetAllHotelDto modelID, [FromBody] GetAllHotelDto model)
        {
            ActionResponse<Hotel> actionResponse = new()
            {
                ResponseType = ResponseType.Ok,
                IsSuccessful = true,
            };

            try
            {
                var hotel = c.Hotels.FirstOrDefault(h => h.Id == modelID.Id);
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
                    c.SaveChanges();
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
