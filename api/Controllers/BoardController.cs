using Application.Concrete;
using Application.Dtos;
using Application.Infrastructure;
using DataAccess.Concrate;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;


namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BoardController : Controller
    {


        [HttpGet]
        [Route("AllBoard")]

        public ActionResponse<List<Board>> GetAllBoards()
        {
            ActionResponse<List<Board>> actionResponse = new()
            {
                ResponseType = ResponseType.Ok,
                IsSuccessful = true,
            };

            return actionResponse;
        }

    }
}
