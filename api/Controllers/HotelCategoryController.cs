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

            var hotelCategories = _context.HotelCategories;
            if (hotelCategories != null && hotelCategories.Count() > 0)
            {
                actionResponse.Data = _context.HotelCategories.Where(x => x.Status == true).ToList();
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

            //var checkHotel =  _context.HotelCategories.Where(h => h.Name == hotelCategory.Name).Count();
            
            //if (checkHotel < 1)
            //{
            //    _context.HotelCategories.Add(hotelCategory);
            //    _context.SaveChanges();
            //}
            //return actionResponse;

            var checkHotel = _context.HotelCategories.Where(h => h.Name == hotelCategory.Name).Count();
            var checkCode = _context.HotelCategories.Where(c => c.Code == hotelCategory.Code)?.Count();

            if (checkHotel < 1 && checkCode < 1)
            {
                _context.HotelCategories.Add(hotelCategory);
                hotelCategory.Status = true;
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

            var hotelCategory = await _context.HotelCategories.FirstOrDefaultAsync(h => h.Id == model.Id);
            if (hotelCategory != null)
            {
                actionResponse.Data = hotelCategory;
            }
            return actionResponse;
        }

        [HttpDelete]
        [Route("delete")]
        public async Task<ActionResponse<HotelCategory>> DeleteHotelCategory([FromBody] HotelCategoryDto model)
        {
            ActionResponse<HotelCategory> actionResponse = new()
            {
                ResponseType = ResponseType.Ok,
                IsSuccessful = true,
            };

            HotelCategory hotelCategory = await _context.HotelCategories.FirstOrDefaultAsync(h => h.Id == model.Id);
            hotelCategory.Status = false;
            _context.SaveChanges();
            return actionResponse;
        }

        [HttpPut]
        [Route("update")]
        public async Task<ActionResponse<HotelCategory>> UpdateHotelCategory([FromBody] HotelCategoryDto model)
        {
            ActionResponse<HotelCategory> actionResponse = new()
            {
                ResponseType = ResponseType.Ok,
                IsSuccessful = true,
            };


            try
            {

                HotelCategory hotelCategory = await _context.HotelCategories.FirstOrDefaultAsync(h => h.Id == model.Id);
                int checkName = _context.HotelCategories.Where(h => h.Name == model.Name).Count();
                int checkCode = _context.HotelCategories.Where(c => c.Code == model.Code).Count();
                
                if (checkName < 1 ||checkCode < 1)
                {
                    hotelCategory.Name = model.Name;
                    hotelCategory.Code = model.Code;
                    hotelCategory.Status = true;
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



