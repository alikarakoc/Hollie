using api.Helpers;
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
            int checkCode = _context.Contracts.Where(c => c.Code == contractdto.Code).Count();

            if (checkCode < 1)
            {
                
                Contract contract = new Contract();
                contract = _mapper.Map<Contract>(contractdto);

                contract.EnteredDate = TimeZoneInfo.ConvertTimeFromUtc(contract.EnteredDate, TimeZoneInfo.Local);
                contract.ExitDate = TimeZoneInfo.ConvertTimeFromUtc(contract.ExitDate, TimeZoneInfo.Local);
                contract.CreatedUser = contractdto.CreatedUser;
                contract.CreatedDate = DateTime.Now;
                _context.Contracts.Add(contract);
                contract.Status = true;
                

                _context.SaveChanges();

                ContractAgencyHelper.AddAgencies(contract.Id, contract.AgencyList, _context);

                ContractBoardHelper.AddBoards(contract.Id, contract.BoardList, _context);

                ContractRoomTypeHelper.AddRoomTypes(contract.Id, contract.RoomTypeList, _context);

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
            contract.RoomTypeList = _context.CRoomTypes.Where(c => c.ListId == model.Id).ToList();
            contract.MarketList = _context.CMarkets.Where(c => c.ListId == model.Id).ToList();

            ContractAgencyHelper.DeleteAgencies(contract.AgencyList, _context);

            ContractBoardHelper.DeleteBoards(contract.BoardList, _context);

            ContractRoomTypeHelper.DeleteRoomTypes(contract.RoomTypeList, _context);

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
                contract.RoomTypeList = _context.CRoomTypes.Where(c => c.ListId == model.Id).ToList();
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
                    contract.UpdatedDate = DateTime.Now;
                    contract.UpdatedUser = model.UpdatedUser;
                    contract.Status = true;
                    contract.EnteredDate = TimeZoneInfo.ConvertTimeFromUtc(model.EnteredDate, TimeZoneInfo.Local);
                    contract.ExitDate = TimeZoneInfo.ConvertTimeFromUtc(model.ExitDate, TimeZoneInfo.Local);

                    
                    ContractAgencyHelper.DeleteAgencies(contract.AgencyList, _context);
                    ContractAgencyHelper.AddAgencies(model.Id, model.AgencyList, _context);

                    ContractBoardHelper.DeleteBoards(contract.BoardList, _context);
                    ContractBoardHelper.AddBoards(model.Id, model.BoardList, _context);

                    ContractRoomTypeHelper.DeleteRoomTypes(contract.RoomTypeList, _context);
                    ContractRoomTypeHelper.AddRoomTypes(model.Id, model.RoomTypeList, _context);

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

        [HttpPost]
        [Route("searchContract")]
        public async Task<ActionResponse<List<Contract>>> searchContract([FromBody] SearchContractDto searchDto)
        {
           
            ActionResponse<List<Contract>> actionResponse = new()
            {
                ResponseType = ResponseType.Ok
            };

            searchDto.BeginDate = searchDto.BeginDate.AddDays(1);
            searchDto.EndDate = searchDto.EndDate.AddDays(1);
            if (searchDto.BeginDate > searchDto.EndDate)
            {
                actionResponse.IsSuccessful = false;
                actionResponse.Message = "EndDate can not be smaller than BeginDate";
                return actionResponse;
            }

            
            List<Contract>  contracts =  _context.Contracts.Where(x=> 
            (searchDto.BeginDate <= x.EnteredDate &&  searchDto.EndDate >= x.ExitDate)||
            (x.EnteredDate <= searchDto.BeginDate && x.ExitDate <= searchDto.EndDate && !(x.ExitDate <= searchDto.BeginDate)) || 
            (x.EnteredDate >= searchDto.BeginDate && x.ExitDate >= searchDto.EndDate && x.EnteredDate <= searchDto.EndDate) ||
            (x.EnteredDate <= searchDto.BeginDate && x.ExitDate >= searchDto.EndDate)).ToList();
            actionResponse.Data = new List<Contract>(contracts);

            if (searchDto.Hotels != null)
            {
                contracts.Clear();
                foreach (var hotelID in searchDto.Hotels)
                {
                    List<Contract> contractsTemp = actionResponse.Data.Where(x => x.HotelId == hotelID).ToList();
                    contracts.AddRange(contractsTemp);
                }
                actionResponse.Data = contracts;    
               
            }
            actionResponse.IsSuccessful = true;

            return actionResponse;

        }


        [HttpPost]
        [Route("searchAccommodation")]
        public async Task<ActionResponse<List<Contract>>> searchAccommodation([FromBody] SearchAccommodationDate searchDto)
        {

            ActionResponse<List<Contract>> actionResponse = new()
            {
                ResponseType = ResponseType.Ok
            };

            
            actionResponse.IsSuccessful = true;

            return actionResponse;

        }


    }

}
