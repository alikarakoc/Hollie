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

    public class BoardController : Controller
    {
        private readonly Context _context;
        public BoardController(Context _context)
        {
            this._context = _context;
        }



        [HttpGet]
        [Route("AllBoards")]
        public ActionResponse<List<Board>> GetAllBoards()
        {
            ActionResponse<List<Board>> actionResponse = new()
            {
                ResponseType = ResponseType.Ok,
                IsSuccessful = true,
            };
            var boards = _context.Boards;

            if(boards!= null && boards.Count()>0)
            {
                actionResponse.Data = boards.ToList();
            }

            return actionResponse;

        }


        [HttpGet]
        public async Task<ActionResponse<Board>> GetBoard([FromQuery] BoardDto model)
        {
            ActionResponse<Board> actionResponse = new()
            {
                ResponseType= ResponseType.Ok,
                IsSuccessful = true,
            };

            var board = await _context.Boards.FirstOrDefaultAsync(h => h.Id == model.Id);
            if (board != null)
            {
                actionResponse.Data = board;
            }

            return actionResponse;

         
        }


        [HttpPost]
        [Route("add")]

        public async Task<ActionResponse<Board>> AddBoard([FromBody] Board brd)
        {
            ActionResponse<Board> actionResponse = new()
            {
                ResponseType = ResponseType.Ok,
                IsSuccessful = true,
            };

            var checkBoard = _context.Boards.Where(h => h.Name == brd.Name)?.Count();
            if(checkBoard < 1)
            {
                _context.Boards.Add(brd);
                _context.SaveChanges();
            }
            return actionResponse;
        }




        [HttpDelete]
        [Route("DeleteHotel")]
        public async Task<ActionResponse<Board>> DeleteBoard([FromQuery] BoardDto model)
        {
            ActionResponse<Board> actionResponse = new()
            {
                ResponseType = ResponseType.Ok,
                IsSuccessful = true,
            };
            var board = await _context.Boards.FirstOrDefaultAsync(h => h.Id == model.Id);
            _context.Boards.Remove(board);
            _context.SaveChanges();
            return actionResponse;
        }


        [HttpPut]
        [Route("DeleteBoard")]

        public async Task<ActionResponse<Board>> UpdateBoard([FromQuery] BoardDto modelD, [FromBody] BoardDto model)
        {
            ActionResponse<Board> actionResponse = new()
            {
                ResponseType = ResponseType.Ok,
                IsSuccessful = true,
            };

            try
            {
                var board = await _context.Boards.FirstOrDefaultAsync(h =>h.Id == modelD.Id);
                if(board != null)
                {
                    board.Code = model.Code;
                    board.Name = model.Name;
                    board.CreatedDate = model.CreatedDate;
                    board.CreatedUser = model.CreatedUser;
                    board.UpdatedDate = model.UpdatedDate;  
                    board.UpdateUser = model.UpdateUser;
                    _context.SaveChanges();
                }
                return actionResponse;
            }
            catch(Exception ex)
            {
                actionResponse.ResponseType = ResponseType.Error;
                actionResponse.IsSuccessful = false;
                actionResponse.Errors.Add(ex.Message);
                return actionResponse;
            }
        }
    }
}


