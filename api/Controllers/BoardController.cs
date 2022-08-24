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
                actionResponse.Data = _context.Boards.Where(x => x.Status == true).ToList();
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

        public async Task<ActionResponse<Board>> AddBoard([FromBody] Board board)
        {
            ActionResponse<Board> actionResponse = new()
            {
                ResponseType = ResponseType.Ok,
                IsSuccessful = true,
            };

            int checkCode = _context.Boards.Where(h => h.Code == board.Code).Count();
            if (checkCode < 1)
            {
                _context.Boards.Add(board);
                board.CreatedDate = DateTime.Now;
                board.Status = true;
                _context.SaveChanges();
            }
            return actionResponse;
        }
         



        [HttpDelete]
        [Route("delete")]
        public async Task<ActionResponse<Board>> DeleteBoard([FromBody] BoardDto model)
        {
            ActionResponse<Board> actionResponse = new()
            {
                ResponseType = ResponseType.Ok,
                IsSuccessful = true,
            };
            Board board = await _context.Boards.FirstOrDefaultAsync(h => h.Id == model.Id);
            board.UpdatedDate = DateTime.Now;
            board.Status = false;
            _context.SaveChanges();
            return actionResponse;
        }


        [HttpPut]
        [Route("update")]

        public async Task<ActionResponse<Board>> UpdateBoard([FromBody] BoardDto model)
        {
            ActionResponse<Board> actionResponse = new()
            {
                ResponseType = ResponseType.Ok,
                IsSuccessful = true,
            };

            try
            {
                var board = await _context.Boards.FirstOrDefaultAsync(h =>h.Id == model.Id);
                int checkCode = _context.Boards.Where(h => h.Code == model.Code && h.Id != model.Id).Count();
                if (checkCode > 0)
                {
                    actionResponse.Message = "Same code exists";
                    actionResponse.IsSuccessful = false;
                }
                if (board.Code == model.Code || checkCode == 0)
                {
                    board.Code = model.Code;
                    board.Name = model.Name;
                    board.UpdatedUser = model.UpdatedUser;
                    board.UpdatedDate = DateTime.Now;
                    board.Status = true;
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


