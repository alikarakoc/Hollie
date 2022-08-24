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
    public class RoomTypeController : Controller
    {
        private readonly Context _context;
        public RoomTypeController(Context _context)
        {
            this._context = _context;
        }


        

        [HttpGet]
        [Route("AllRoomTypes")]
        public ActionResponse<List<RoomType>> RoomType()
        {
            ActionResponse<List<RoomType>> actionResponse = new()
            {
                ResponseType = ResponseType.Ok,
                IsSuccessful = true,
            };

            var roomtypes = _context.RoomTypes;
            
            if (roomtypes != null && roomtypes.Count() > 0)
            {
                actionResponse.Data = _context.RoomTypes.Where(x => x.Status == true).ToList();
            }
            return actionResponse;
        }



        [HttpGet]
        public async Task<ActionResponse<RoomType>> GetMarkets([FromQuery] RoomTypeDto model)
        {
            if (model is null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            ActionResponse<RoomType> actionResponse = new()
            {
                ResponseType = ResponseType.Ok,
                IsSuccessful = true,
            };
            var roomtypes = await _context.RoomTypes.FirstOrDefaultAsync(h => h.Id == model.Id);
            if (roomtypes != null)
            {
                actionResponse.Data = roomtypes;
            }
            return actionResponse;
        }



        [HttpPost]
        [Route("add")]
        public async Task<ActionResponse<RoomType>> AddRoomType([FromBody] RoomType room)
        {

            ActionResponse<RoomType> actionResponse = new()
            {
                ResponseType = ResponseType.Ok,
                IsSuccessful = true,
            };
            //var checkName = _context.RoomTypes.Where(h => h.Name == room.Name).Count();
            var checkCode = _context.RoomTypes.Where(h => h.Code == room.Code).Count();
            //if (checkName < 1) 
            if (checkCode < 1)
            {
                _context.RoomTypes.Add(room);
                room.Status = true;
                room.CreatedDate = DateTime.Now;
                _context.SaveChanges();
            }
            return actionResponse;
        }

        [HttpDelete]
        [Route("delete")]
        public async Task<ActionResponse<RoomType>> DeleteRoomType([FromBody] RoomTypeDto model)
        {
            ActionResponse<RoomType> actionResponse = new()
            {
                ResponseType = ResponseType.Ok,
                IsSuccessful = true,
            };
            RoomType roomtype = await _context.RoomTypes.FirstOrDefaultAsync(h => h.Id == model.Id);
            roomtype.Status = false;
            roomtype.UpdatedUser = model.UpdatedUser;
            roomtype.UpdatedDate = DateTime.Now;
            _context.SaveChanges();
            return actionResponse;
        }

        [HttpPut]
        [Route("update")]
        public async Task<ActionResponse<RoomType>> UpdateRoomType([FromBody] RoomTypeDto model)
        {
            ActionResponse<RoomType> actionResponse = new()
            {
                ResponseType = ResponseType.Ok,
                IsSuccessful = true,
            };
            
            try
            {
                RoomType roomtype = await _context.RoomTypes.FirstOrDefaultAsync(h => h.Id == model.Id);
                int checkCode = (int)_context.RoomTypes.Where(h => h.Code == model.Code)?.Count();

                if(checkCode > 0)
                {
                    actionResponse.Message = "Same code exists";
                    actionResponse.IsSuccessful = false;
                }

                if (roomtype.Code == model.Code || checkCode == 0)
                {
                    roomtype.Code = model.Code;
                    roomtype.Name = model.Name;
                    roomtype.CreatedDate = model.CreatedDate;
                    roomtype.CreatedUser = model.CreatedUser;
                    roomtype.HotelId = model.HotelId;
                    roomtype.MaxCH = model.MaxCH;
                    roomtype.MaxAD = model.MaxAD;
                    roomtype.Pax = model.Pax;
                    roomtype.UpdatedUser = model.UpdatedUser;
                    roomtype.UpdatedDate = DateTime.Now;
                    roomtype.Status = true;
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
