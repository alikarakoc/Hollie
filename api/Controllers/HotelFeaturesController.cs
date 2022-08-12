using Application.Concrete;
using Application.Dtos;
using Application.Infrastructure;
using DataAccess.Concrate;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelFeaturesController : Controller
    {
        private readonly Context _context;

        public HotelFeaturesController(Context context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("all")]
        public async Task<ActionResponse<List<HotelFeature>>>  getAll()
        {
            ActionResponse<List<HotelFeature>> actionResponse = new()
            {
                ResponseType = ResponseType.Ok,
                IsSuccessful = true,
            };

            var features = _context.HotelFeatures;
            if (features != null && features.Count() > 0)
            {
                actionResponse.Data = _context.HotelFeatures.Where(x => x.Status == true).ToList();
            }
            return actionResponse;

        }



        [HttpPost]
        [Route("add")]
        public async Task<ActionResponse<HotelFeature>> add([FromBody] HotelFeature hotelFeature)
        {
            ActionResponse<HotelFeature> actionResponse = new()
            {
                ResponseType = ResponseType.Ok,
                IsSuccessful = true,
            };

            int checkCode = _context.HotelFeatures.Where(c => c.Code == hotelFeature.Code).Count();

            if (checkCode < 1)
            {
                _context.HotelFeatures.Add(hotelFeature);
                hotelFeature.Status = true;
                _context.SaveChanges();
            }
            return actionResponse;
        }

        [HttpDelete]
        [Route("delete")]
        public async Task<ActionResponse<HotelFeature>> delete([FromBody] HotelFeature model)
        {
            ActionResponse<HotelFeature> actionResponse = new()
            {
                ResponseType = ResponseType.Ok,
                IsSuccessful = true,
            };

            HotelFeature hotelFeature = await _context.HotelFeatures.FirstOrDefaultAsync(h => h.Id == model.Id);

            hotelFeature.Status = false;
            _context.SaveChanges();
            return actionResponse;
        }

        [HttpPut]
        [Route("update")]
        public async Task<ActionResponse<HotelFeature>> update([FromBody] HotelFeature model)
        {
            ActionResponse<HotelFeature> actionResponse = new()
            {
                ResponseType = ResponseType.Ok,
                IsSuccessful = true,
            };

            try
            {
                HotelFeature hotelFeature = await _context.HotelFeatures.FirstOrDefaultAsync(h => h.Id == model.Id);
                int checkCode = _context.HotelFeatures.Where(h => h.Code == model.Code && h.Id != model.Id).Count();
                if (checkCode > 0)
                {
                    actionResponse.Message = "Same code exists";
                    actionResponse.IsSuccessful = false;
                }
                if (hotelFeature.Code == model.Code || checkCode == 0)
                {
                    hotelFeature.Code = model.Code;
                    hotelFeature.Name = model.Name;
                    hotelFeature.BabyTop = model.BabyTop;
                    hotelFeature.ChildTop = model.ChildTop;
                    hotelFeature.TeenTop = model.TeenTop;
                    hotelFeature.Status = true;

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

