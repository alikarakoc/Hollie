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
    public class RoomsController : Controller
    {

        private readonly Context _context;
        public RoomsController(Context _context)
        {
            this._context = _context;
        }

        [HttpGet]
        [Route("AllRooms")]
        public ActionResponse<List<Room>> GetAllRooms()
        {
            ActionResponse<List<Room>> actionResponse = new()
            {
                ResponseType = ResponseType.Ok,
                IsSuccessful = true,
            };

            var rooms = _context.Rooms;
            //Otelleri Çek await ile
            //eğer hata var ise actionResponse.IsSuccessful=false set edilir.
            //actionResponse.Data = "çekilen otel listesi";
            if (rooms != null && rooms.Count() > 0)
            {
                actionResponse.Data = _context.Rooms.Where(x => x.Status == true).ToList();
            }
            return actionResponse;
        }
        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResponse<Room>> GetRoom([FromQuery] RoomDto model)
        {
            ActionResponse<Room> actionResponse = new()
            {
                ResponseType = ResponseType.Ok,
                IsSuccessful = true,
            };
            var room = await _context.Rooms.FirstOrDefaultAsync(h => h.Id == model.Id);
            if (room != null)
            {
                actionResponse.Data = room;
            }
            return actionResponse;
        }
        [HttpPost]
        [Route("add")]
        public async Task<ActionResponse<Room>> AddRoom([FromBody] Room rom)
        {

            ActionResponse<Room> actionResponse = new()
            {
                ResponseType = ResponseType.Ok,
                IsSuccessful = true,
            };

            var checkCode = _context.Rooms.Where(c => c.Code == rom.Code).Count();
            if (checkCode < 1)
            {
                _context.Rooms.Add(rom);
                rom.Status = true;
                _context.SaveChanges();
            }
            return actionResponse;
        }
        [HttpDelete]
        [Route("delete")]
        public async Task<ActionResponse<Room>> DeleteRoom([FromBody] RoomDto model)
        {
            ActionResponse<Room> actionResponse = new()
            {
                ResponseType = ResponseType.Ok,
                IsSuccessful = true,
            };
            Room room = await _context.Rooms.FirstOrDefaultAsync(h => h.Id == model.Id);
            room.Status = false;
            _context.SaveChanges();
            return actionResponse;
        }


        [HttpPut]
        [Route("update")]
        public async Task<ActionResponse<Room>> UpdateRoom([FromBody] RoomDto model)
        {
            ActionResponse<Room> actionResponse = new()
            {
                ResponseType = ResponseType.Ok,
                IsSuccessful = true,
            };

            try
            {
                Room room = await _context.Rooms.FirstOrDefaultAsync(h => h.Id == model.Id);
                int checkCode = _context.Rooms.Where(h => h.Code == model.Code && h.Id != model.Id).Count();

                if (checkCode > 0)
                {
                    actionResponse.Message = "Same code exists";
                    actionResponse.IsSuccessful = false;
                }

                if (room.Code == model.Code || checkCode == 0)
                {
                    room.Code = model.Code;
                    room.Name = model.Name;
                    room.HotelId = model.HotelId;
                    room.RoomTypeId = model.RoomTypeId;
                    room.Clean = model.Clean;
                    room.Reservation = model.Reservation;
                    room.Status = true;
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
