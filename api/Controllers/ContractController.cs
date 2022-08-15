﻿using api.Helpers;
using Application.Concrete;
using Application.Dtos;
using Application.Infrastructure;
using AutoMapper;
using DataAccess.Concrate;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContractController : Controller
    {
      
        private readonly Context _context;
        private readonly IMapper _mapper;

        public ContractController(Context _context, IMapper mapper)
        {
            this._context = _context;
            this._mapper = mapper;
        }

        [HttpGet]
        [Route("AllContract")]
        public async Task<ActionResponse<List<Contract>>> GetAllContract()
        {
            ActionResponse<List<Contract>> actionResponse = new()
            {
                ResponseType = ResponseType.Ok,
                IsSuccessful = true,
            };

            var contract = _context.Contracts;
            if (contract != null && contract.Count() > 0)
            {
                actionResponse.Data = _context.Contracts.Where(x => x.Status == true).ToList();
            }
            return actionResponse;

        }

       

        [HttpPost]
        [Route("add")]
        public async Task<ActionResponse<Contract>> addContract([FromBody] ContractDto contractdto)
        {
            ActionResponse<Contract> actionResponse = new()
            {
                ResponseType = ResponseType.Ok,
                IsSuccessful = true,
            };
            int checkCode = (int)_context.Contracts.Where(c => c.Code == contractdto.Code)?.Count();

            if (checkCode < 1)
            {
                
                Contract contract = new Contract();
                contract = _mapper.Map<Contract>(contractdto);

                contract.EnteredDate = TimeZoneInfo.ConvertTimeFromUtc(contract.EnteredDate, TimeZoneInfo.Local);
                contract.ExitDate = TimeZoneInfo.ConvertTimeFromUtc(contract.ExitDate, TimeZoneInfo.Local);



                List<Agency> listAgency = _context.Agencies.ToList();
                _context.Contracts.Add(contract);
                contract.Status = true;

                _context.SaveChanges();

                ContractAgencyHelper.AddAgencies(contract.Id, contract.AgencyList, _context);

                ContractBoardHelper.AddBoards(contract.Id, contract.BoardList, _context);

                ContractRoomHelper.AddRooms(contract.Id, contract.RoomList, _context);

                ContractMarketHelper.AddMarkets(contract.Id, contract.MarketList, _context);

                _context.SaveChanges();


            }
            return actionResponse;
        }


        [HttpGet]
        [Route("getById")]
        public async Task<ActionResponse<Contract>> GetHotelCategoryByID([FromQuery] ContractDto model)
        {
            ActionResponse<Contract> actionResponse = new()
            {
                ResponseType = ResponseType.Ok,
                IsSuccessful = true,
            };

            var contract = await _context.Contracts.FirstOrDefaultAsync(h => h.Id == model.Id);
            if (contract != null)
            {
                actionResponse.Data = contract;
            }
            return actionResponse;
        }

        [HttpDelete]
        [Route("delete")]
        public async Task<ActionResponse<Contract>> DeleteHotelCategory([FromBody] ContractDto model)
        {
            ActionResponse<Contract> actionResponse = new()
            {
                ResponseType = ResponseType.Ok,
                IsSuccessful = true,
            };

            Contract contract = await _context.Contracts.FirstOrDefaultAsync(h => h.Id == model.Id);
            contract.AgencyList = _context.CAgencies.Where(c => c.ListId == model.Id).ToList();
            contract.BoardList = _context.CBoards.Where(c => c.ListId == model.Id).ToList();
            contract.RoomList = _context.CRooms.Where(c => c.ListId == model.Id).ToList();
            contract.MarketList = _context.CMarkets.Where(c => c.ListId == model.Id).ToList();

            ContractAgencyHelper.DeleteAgencies(contract.AgencyList, _context);

            ContractBoardHelper.DeleteBoards(contract.BoardList, _context);

            ContractRoomHelper.DeleteRooms(contract.RoomList, _context);

            ContractMarketHelper.DeleteMarkets(contract.MarketList, _context);

            contract.Status = false;
            _context.SaveChanges();
            return actionResponse;
        }

        [HttpPut]
        [Route("update")]
        public async Task<ActionResponse<Contract>> UpdateHotelCategory([FromBody] ContractDto model)
        {
            ActionResponse<Contract> actionResponse = new()
            {
                ResponseType = ResponseType.Ok,
                IsSuccessful = true,
            };

            try
            {
                Contract contract = await _context.Contracts.FirstOrDefaultAsync(h => h.Id == model.Id);

                contract.AgencyList =_context.CAgencies.Where(c => c.ListId == model.Id).ToList();
                contract.BoardList = _context.CBoards.Where(c => c.ListId == model.Id).ToList();
                contract.RoomList = _context.CRooms.Where(c => c.ListId == model.Id).ToList();
                contract.MarketList = _context.CMarkets.Where(c => c.ListId == model.Id).ToList();

                int checkCode = _context.Contracts.Where(h => h.Code == model.Code && h.Id!=model.Id).Count();
                if (checkCode > 0)
                {
                    actionResponse.Message = "Same code exists";
                    actionResponse.IsSuccessful = false;
                }
                if (contract.Code == model.Code || checkCode == 0)
                {
                    contract.Code = model.Code;
                    contract.Name = model.Name;
                    contract.HotelId = model.HotelId;
                    contract.ADP = model.ADP;
                    contract.CH1 = model.CH1;
                    contract.CH2 = model.CH2;
                    contract.CH3 = model.CH3;
                    contract.CurrencyId = model.CurrencyId;
                    contract.Status = true;
                    contract.EnteredDate = TimeZoneInfo.ConvertTimeFromUtc(model.EnteredDate, TimeZoneInfo.Local);
                    contract.ExitDate = TimeZoneInfo.ConvertTimeFromUtc(model.ExitDate, TimeZoneInfo.Local);

                    List<Agency> list = _context.Agencies.ToList();

                    
                    ContractAgencyHelper.DeleteAgencies(contract.AgencyList, _context);
                    ContractAgencyHelper.AddAgencies(model.Id, model.AgencyList, _context);

                    ContractBoardHelper.DeleteBoards(contract.BoardList, _context);
                    ContractBoardHelper.AddBoards(model.Id, model.BoardList, _context);

                    ContractRoomHelper.DeleteRooms(contract.RoomList, _context);
                    ContractRoomHelper.AddRooms(model.Id, model.RoomList, _context);

                    ContractMarketHelper.DeleteMarkets(contract.MarketList, _context);
                    ContractMarketHelper.AddMarkets(model.Id, model.MarketList, _context);



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
