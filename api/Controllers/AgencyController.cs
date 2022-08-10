using api.Helpers;
using Application.Concrete;
using Application.Dtos;
using Application.Infrastructure;
using AutoMapper;
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

        private readonly IMapper _mapper;

        private readonly Context _context;
        public AgencyController(Context _context)
        {
            this._context = _context;
        }




        [HttpGet]
        [Route("AllAgencies")]
        public ActionResponse<List<Agency>> Agency()
        {
            ActionResponse<List<Agency>> actionResponse = new()
            {
                ResponseType = ResponseType.Ok,
                IsSuccessful = true,
            };

            var agencys = _context.Agencies;

            if (agencys != null && agencys.Count() > 0)
            {
                actionResponse.Data = _context.Agencies.Where(x => x.Status == true).ToList();
            }
            return actionResponse;
        }

        [HttpGet]
        public async Task<ActionResponse<Agency>> GetAgencies([FromQuery] AgencyDto model)
        {
         

            ActionResponse<Agency> actionResponse = new()
            {
                ResponseType = ResponseType.Ok,
                IsSuccessful = true,
            };
            var agencies = await _context.Agencies.FirstOrDefaultAsync(h => h.Id == model.Id);
            if (agencies != null)
            {
                actionResponse.Data = agencies;
            }
            return actionResponse;
        }



        [HttpPost]
        [Route("add")]
        public async Task<ActionResponse<Agency>> AddAgencies([FromBody] Agency _agency)
        {

            ActionResponse<Agency> actionResponse = new()
            {
                ResponseType = ResponseType.Ok,
                IsSuccessful = true,
            };
            //var checkName = _context.Agencies.Where(h => h.Name == _agency.Name).Count();
            //if (checkName < 1)

            //Agency s = _agency;
            ////s.Code = 5;

            var checkCode = _context.Agencies.Where(h => h.Code == _agency.Code).Count();
            if (checkCode < 1)
            {
                Agency agency = new Agency();
                agency = _mapper.Map<Agency>(_agency);

                _context.Agencies.Add(_agency);
                _agency.Status = true;


                AgencyMarketHelper.AddMarkets(agency.Id, agency.MarketList, _context);

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
            var agency = await _context.Agencies.FirstOrDefaultAsync(h => h.Id == model.Id);
            agency.Status = false;
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
                var agency = await _context.Agencies.FirstOrDefaultAsync(h => h.Id == modelID.Id);
                //var checkAgency = _context.Agencies.Where(h => h.Name == model.Name)?.Count();         
                //if (checkAgency< 1 && agency != null)
                var checkCode = _context.Agencies.Where(h => h.Code == model.Code)?.Count();
                if(agency.Code == model.Code)
                {
                    agency.Name = model.Name;
                    agency.Phone = model.Phone;
                    agency.Email = model.Email;
                    agency.Address = model.Address;
                    agency.CreatedDate = model.CreatedDate;
                    agency.CreatedUser = model.CreatedUser;
                    agency.UpdatedDate = model.UpdatedDate;
                    agency.UpdateUser = model.UpdateUser;
                    agency.Status = true;

                    _context.SaveChanges();
                }
                
                else if (checkCode < 1 && agency != null)
                {
                    agency.Code = model.Code;
                    agency.Name = model.Name;
                    agency.Phone = model.Phone;
                    agency.Email = model.Email;
                    agency.Address = model.Address;
                    agency.CreatedDate = model.CreatedDate;
                    agency.CreatedUser = model.CreatedUser;
                    agency.UpdatedDate = model.UpdatedDate;
                    agency.UpdateUser = model.UpdateUser;
                    agency.Status = true;

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
