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
    public class AgencyController : Controller
    {
        private readonly Context _context;
        public AgencyController(Context _context)
        {
            this._context = _context;
        }




        [HttpGet]
        [Route("AllAgencys")]
        public ActionResponse<List<Agency>> Agency()
        {
            ActionResponse<List<Agency>> actionResponse = new()
            {
                ResponseType = ResponseType.Ok,
                IsSuccessful = true,
            };

            var agencys = _context.Agencys;

            if (agencys != null && agencys.Count() > 0)
            {
                actionResponse.Data = agencys.ToList();
            }
            return actionResponse;
        }



        [HttpGet]
        public async Task<ActionResponse<Agency>> GetAgencys([FromQuery] AgencyDto model)
        {
            if (model is null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            ActionResponse<Agency> actionResponse = new()
            {
                ResponseType = ResponseType.Ok,
                IsSuccessful = true,
            };
            var agencys = await _context.Agencys.FirstOrDefaultAsync(h => h.Id == model.Id);
            if (agencys != null)
            {
                actionResponse.Data = agencys;
            }
            return actionResponse;
        }



        [HttpPost]
        [Route("add")]
        public async Task<ActionResponse<Agency>> AddAgencys([FromBody] Agency _agency)
        {

            ActionResponse<Agency> actionResponse = new()
            {
                ResponseType = ResponseType.Ok,
                IsSuccessful = true,
            };
            var agencyCheck = _context.Agencys.Where(h => h.Name == _agency.Name).Count();
            if (agencyCheck < 1)
            {
                _context.Agencys.Add(_agency);
                _context.SaveChanges();
            }
            return actionResponse;
        }

        [HttpDelete]
        [Route("delete")]
        public async Task<ActionResponse<Hotel>> DeleteAgency([FromQuery] AgencyDto model)
        {
            ActionResponse<Hotel> actionResponse = new()
            {
                ResponseType = ResponseType.Ok,
                IsSuccessful = true,
            };
            var agency = await _context.Agencys.FirstOrDefaultAsync(h => h.Id == model.Id);
            _context.Agencys.Remove(agency);
            _context.SaveChanges();
            return actionResponse;
        }

        [HttpPut]
        [Route("update")]
        public async Task<ActionResponse<Agency>> UpdateAgency([FromQuery] AgencyDto modelID, [FromBody] AgencyDto model)
        {
            ActionResponse<Agency> actionResponse = new()
            {
                ResponseType = ResponseType.Ok,
                IsSuccessful = true,
            };

            try
            {
                var agency = await _context.Agencys.FirstOrDefaultAsync(h => h.Id == modelID.Id);
                if (agency != null)
                {
                    agency.Code = model.Code;
                    agency.Name = model.Name;
                    agency.CreatedDate = model.CreatedDate;
                    agency.CreatedUser = model.CreatedUser;
                    agency.UpdatedDate = model.UpdatedDate;
                    agency.UpdateUser = model.UpdateUser;


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
