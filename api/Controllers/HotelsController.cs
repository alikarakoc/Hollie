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
using System.Net;
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
            if (hotels != null && hotels.Count() > 0)
            {
                actionResponse.Data = _context.Hotels.Where(x => x.Status == true).ToList();
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

            int checkCode = _context.Hotels.Where(c => c.Code == htl.Code).Count();
           
            if (checkCode < 1)
            {
                _context.Hotels.Add(htl);
                htl.CreatedDate = DateTime.Now;
                htl.Status = true;
                _context.SaveChanges();
            }
            return actionResponse;
        }


        [HttpDelete]
        [Route("delete")]
        public async Task<ActionResponse<Hotel>> DeleteHotel([FromBody] HotelDto model)
        {
            ActionResponse<Hotel> actionResponse = new()
            {
                ResponseType = ResponseType.Ok,
                IsSuccessful = true,
            };
            var hotel = await _context.Hotels.FirstOrDefaultAsync(h => h.Id == model.Id);
            hotel.UpdatedDate = DateTime.Now;
            hotel.Status = false;
            _context.SaveChanges();
            return actionResponse;
        }

        [HttpPut]
        [Route("update")]
        public async Task<ActionResponse<Hotel>> UpdateHotel([FromBody] HotelDto model)
        {
            ActionResponse<Hotel> actionResponse = new()
            {
                ResponseType = ResponseType.Ok,
                IsSuccessful = true,
            };

            try
            {
                Hotel hotel = await _context.Hotels.FirstOrDefaultAsync(h => h.Id == model.Id);
                int checkCode = _context.Hotels.Where(h => h.Code == model.Code && h.Id != model.Id).Count();

                if (checkCode > 0)
                {
                    actionResponse.Message = "Same code exists";
                    actionResponse.IsSuccessful = false;
                }
                if (hotel.Code == model.Code || checkCode == 0)
                {
                    hotel.Name = model.Name;
                    hotel.Address = model.Address;
                    hotel.Phone = model.Phone;
                    hotel.Email = model.Email;
                    hotel.HotelCategoryId = model.HotelCategoryId;
                    hotel.HotelFeatureId  = model.HotelFeatureId;
                    hotel.UpdatedUser = model.UpdatedUser;
                    hotel.UpdatedDate = DateTime.Now;
                    hotel.Status = true;
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
