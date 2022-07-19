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
        public async Task<ActionResponse<List<HotelCategory>>> GetAllHotelCategories()
        {
            ActionResponse<List<HotelCategory>> actionResponse = new()
            {
                ResponseType = ResponseType.Ok,
                IsSuccessful = true,
            };

            var hotelCategories = _context.HotelCategorys;
            if (hotelCategories != null && hotelCategories.Count() > 0)
            {
                actionResponse.Data = hotelCategories.ToList();
            }
            return actionResponse;
        }

        [HttpPost]
        [Route("add")]
        public async Task<ActionResponse<HotelCategory>> AddHotelCategory([FromBody] HotelCategory hotelCategory)
        {
            ActionResponse<HotelCategory> actionResponse = new()
            {
                ResponseType = ResponseType.Ok,
                IsSuccessful = true,
            };

            var checkHotel =  _context.HotelCategorys.Where(h => h.Name == hotelCategory.Name)?.Count();
            if (checkHotel < 1)
            {
                _context.HotelCategorys.Add(hotelCategory);
                _context.SaveChanges();
            }
            return actionResponse;

        }

        [HttpGet]
        [Route("getById")]
        public async Task<ActionResponse<HotelCategory>> GetHotelCategoryByID([FromQuery] HotelCategoryDto model)
        {
            ActionResponse<HotelCategory> actionResponse = new()
            {
                ResponseType = ResponseType.Ok,
                IsSuccessful = true,
            };

            var hotelCategory = await _context.HotelCategorys.FirstOrDefaultAsync(h => h.Id == model.Id);
            if (hotelCategory != null)
            {
                actionResponse.Data = hotelCategory;
            }
            return actionResponse;
        }

        [HttpDelete]
        [Route("delete")]
        public async Task<ActionResponse<HotelCategory>> DeleteHotelCategory([FromQuery] HotelCategoryDto model)
        {
            ActionResponse<HotelCategory> actionResponse = new()
            {
                ResponseType = ResponseType.Ok,
                IsSuccessful = true,
            };

            var hotelCategory = await _context.HotelCategorys.FirstOrDefaultAsync(h => h.Id == model.Id);
            _context.HotelCategorys.Remove(hotelCategory);
            _context.SaveChanges();
            return actionResponse;
        }

        [HttpPut]
        [Route("update")]
        public async Task<ActionResponse<HotelCategory>> UpdateHotelCategory([FromQuery] HotelCategoryDto modelID, [FromBody] HotelCategoryDto model)
        {
            ActionResponse<HotelCategory> actionResponse = new()
            {
                ResponseType = ResponseType.Ok,
                IsSuccessful = true,
            };


            try
            {
                //var hotelCategory = await _context.HotelCategorys.FirstOrDefaultAsync(h => h.Id == model.Id);

                //if (hotelCategory != null)
                //{
                //    hotelCategory.Name = model.Name;
                //    _context.SaveChanges();
                //}
                //return actionResponse;

                var hotelCategory = await _context.HotelCategorys.FirstOrDefaultAsync(h => h.Id == modelID.Id);
                var checkHotel = _context.HotelCategorys.Where(h => h.Name == model.Name)?.Count();
                if (checkHotel < 1)
                {
                    hotelCategory.Name = model.Name;
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



