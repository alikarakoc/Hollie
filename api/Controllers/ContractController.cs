using Application.Concrete;
using Application.Dtos;
using Application.Infrastructure;
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

        public ContractController(Context _context)
        {
            this._context = _context;
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
                actionResponse.Data = contract.ToList();
            }
            return actionResponse;

        }

        [HttpPost]
        [Route("add")]
        public async Task<ActionResponse<Contract>> addContract([FromBody] Contract contract)
        {
            ActionResponse<Contract> actionResponse = new()
            {
                ResponseType = ResponseType.Ok,
                IsSuccessful = true,
            };
            var checkName = _context.Contracts.Where(c => c.Name == contract.Name)?.Count();

            if (checkName < 1)
            {
                _context.Contracts.Add(contract);
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
        public async Task<ActionResponse<Contract>> DeleteHotelCategory([FromQuery] ContractDto model)
        {
            ActionResponse<Contract> actionResponse = new()
            {
                ResponseType = ResponseType.Ok,
                IsSuccessful = true,
            };

            var contract = await _context.Contracts.FirstOrDefaultAsync(h => h.Id == model.Id);
            _context.Contracts.Remove(contract);
            _context.SaveChanges();
            return actionResponse;
        }

        [HttpPut]
        [Route("update")]
        public async Task<ActionResponse<Contract>> UpdateHotelCategory([FromQuery] ContractDto modelID, [FromBody] ContractDto model)
        {
            ActionResponse<Contract> actionResponse = new()
            {
                ResponseType = ResponseType.Ok,
                IsSuccessful = true,
            };


            try
            {
                var contract = await _context.Contracts.FirstOrDefaultAsync(h => h.Id == modelID.Id);
                //var checkName = _context.Contracts.Where(h => h.Name == model.Name)?.Count();
                var checkCode = _context.Contracts.Where(h => h.Code == model.Code)?.Count();

                if (contract.Code == model.Code)
                {
                    contract.Name = model.Name;
                    contract.HotelId = model.HotelId;
                    contract.MarketId = model.MarketId;
                    contract.AgencyId = model.AgencyId;
                    contract.BoardId = model.BoardId;
                    contract.RoomTypeId = model.RoomTypeId;
                    contract.Price = model.Price;
                    contract.CurrencyId = model.CurrencyId;
                    contract.EnteredDate = model.EnteredDate;
                    contract.ExitDate = model.ExitDate;

                    _context.SaveChanges();
                }

                if (checkCode < 1)
                {
                    contract.Code = model.Code;
                    contract.Name = model.Name;
                    contract.HotelId = model.HotelId;
                    contract.MarketId = model.MarketId;
                    contract.AgencyId = model.AgencyId;
                    contract.BoardId = model.BoardId;
                    contract.RoomTypeId = model.RoomTypeId;
                    contract.Price = model.Price;
                    contract.CurrencyId = model.CurrencyId;
                    contract.EnteredDate = model.EnteredDate;
                    contract.ExitDate = model.ExitDate;

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
