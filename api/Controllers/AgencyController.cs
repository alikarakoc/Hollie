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
using System.Net;
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
                actionResponse.Data = agencys.Where(x => x.Status == true).ToList();
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
            int checkCode = _context.Agencies.Where(h => h.Code == _agency.Code).Count();
            if (checkCode < 1)
            {
                _agency.CreatedDate = DateTime.Now;
                _agency.Status = true;
                _context.Agencies.Add(_agency);

                _context.SaveChanges();

                AgencyMarketHelper.AddMarkets(_agency.Id, _agency.MarketList, _context);

                _context.SaveChanges();
            }
            return actionResponse;
        }

        [HttpDelete]
        [Route("delete")]
        public async Task<ActionResponse<Hotel>> DeleteAgency([FromBody] AgencyDto model)
        {
            ActionResponse<Hotel> actionResponse = new()
            {
                ResponseType = ResponseType.Ok,
                IsSuccessful = true,
            };
            var agency = await _context.Agencies.FirstOrDefaultAsync(h => h.Id == model.Id);
            agency.UpdatedDate = DateTime.Now;
            agency.Status = false;
            _context.SaveChanges();
            return actionResponse;
        }

        [HttpPut]
        [Route("update")]
        public async Task<ActionResponse<Agency>> UpdateAgency([FromBody] AgencyDto model)
        {
            ActionResponse<Agency> actionResponse = new()
            {
                ResponseType = ResponseType.Ok,
                IsSuccessful = true,
            };

            try
            {
                Agency agency = await _context.Agencies.FirstOrDefaultAsync(h => h.Id == model.Id);
                agency.MarketList = _context.AMarkets.Where(c => c.ListId == model.Id).ToList();

                int checkCode = _context.Agencies.Where(h => h.Code == model.Code).Count();

                if (checkCode > 0)
                {
                    actionResponse.Message = "Same code exists";
                    actionResponse.IsSuccessful = false;
                }
                if (agency.Code == model.Code || checkCode == 0)
                {
                    agency.Code = model.Code;
                    agency.Name = model.Name;
                    agency.Phone = model.Phone;
                    agency.Email = model.Email;
                    agency.Address = model.Address;
                    agency.UpdatedUser = model.UpdatedUser;
                    agency.UpdatedDate = DateTime.Now;
                    agency.Status = true;

                    AgencyMarketHelper.DeleteMarkets(agency.MarketList, _context);
                    AgencyMarketHelper.AddMarkets(model.Id, model.MarketList, _context);

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
