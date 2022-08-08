﻿using Application.Concrete;
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
                actionResponse.Data = _context.Rooms.Where(x => x.status == true).ToList();
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
                rom.status = true;
                _context.SaveChanges();
            }
            return actionResponse;
        }
        [HttpDelete]
        [Route("delete")]
        public async Task<ActionResponse<Room>> DeleteRoom([FromQuery] RoomDto model)
        {
            ActionResponse<Room> actionResponse = new()
            {
                ResponseType = ResponseType.Ok,
                IsSuccessful = true,
            };
            var room = await _context.Rooms.FirstOrDefaultAsync(h => h.Id == model.Id);
            room.status = false;
            _context.SaveChanges();
            return actionResponse;
        }


        [HttpPut]
        [Route("update")]
        public async Task<ActionResponse<Room>> UpdateRoom([FromQuery] RoomDto modelID, [FromBody] RoomDto model)
        {
            ActionResponse<Room> actionResponse = new()
            {
                ResponseType = ResponseType.Ok,
                IsSuccessful = true,
            };

            try
            {
                var room = await _context.Rooms.FirstOrDefaultAsync(h => h.Id == modelID.Id);
                var checkCode = _context.Rooms.Where(c => c.Code == model.Code)?.Count();

                if (room.Code == model.Code)
                {
                    room.Name = model.Name;
                    room.bed = model.bed;
                    room.slot = model.slot;
                    room.RoomTypeId = model.RoomTypeId;
                    room.HotelId = model.HotelId;
                    room.status = true;
                    _context.SaveChanges();
                }

                else if (checkCode < 1 && room != null)
                {
                    room.Code = model.Code;
                    room.Name = model.Name;
                    room.bed = model.bed;
                    room.slot = model.slot;
                    room.HotelId = model.HotelId;
                    room.status = true;
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