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
                actionResponse.Data = roomtypes.ToList();
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
        [Route("AddRoomType")]
        public async Task<ActionResponse<RoomType>> AddRoomType([FromBody] RoomType room)
        {

            ActionResponse<RoomType> actionResponse = new()
            {
                ResponseType = ResponseType.Ok,
                IsSuccessful = true,
            };
            _context.RoomTypes.Add(room);
            _context.SaveChanges();
            return actionResponse;
        }

        [HttpDelete]
        [Route("DeleteRoomType")]
        public async Task<ActionResponse<RoomType>> DeleteRoomType([FromQuery] RoomTypeDto model)
        {
            ActionResponse<RoomType> actionResponse = new()
            {
                ResponseType = ResponseType.Ok,
                IsSuccessful = true,
            };
            var roomtype = await _context.RoomTypes.FirstOrDefaultAsync(h => h.Id == model.Id);
            _context.RoomTypes.Remove(roomtype);
            _context.SaveChanges();
            return actionResponse;
        }

        [HttpPut]
        [Route("UpdateRoomType")]
        public async Task<ActionResponse<RoomType>> UpdateRoomType([FromQuery]RoomTypeDto modelID, [FromBody] RoomTypeDto model)
        {
            ActionResponse<RoomType> actionResponse = new()
            {
                ResponseType = ResponseType.Ok,
                IsSuccessful = true,
            };

            try
            {
                var roomtype = await _context.RoomTypes.FirstOrDefaultAsync(h => h.Id == modelID.Id);
                if (roomtype != null)
                {
                    roomtype.Code = model.Code;
                    roomtype.Name = model.Name;
                    roomtype.CreatedDate = model.CreatedDate;
                    roomtype.CreatedUser = model.CreatedUser;
                    roomtype.UpdatedDate = model.UpdatedDate;
                    roomtype.UpdateUser = model.UpdateUser;
                
                   
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