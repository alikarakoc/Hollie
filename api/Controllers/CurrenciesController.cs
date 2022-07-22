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
    public class CurrenciesController : Controller
    {
        private readonly Context _context;
        public CurrenciesController(Context _context)
        {
            this._context = _context;
        }
      

        [HttpGet]
        [Route("AllCurrency")]
        public async Task<ActionResponse<List<Currency>>> GetAllCurrencies()
        {
            ActionResponse<List<Currency>> actionResponse = new()
            {
                ResponseType = ResponseType.Ok,
                IsSuccessful = true,
            };

            var currencies = _context.Currencies;
            if (currencies != null && currencies.Count() > 0)
            {
                actionResponse.Data = currencies.ToList();
            }
            return actionResponse;
        }

        [HttpPost]
        [Route("add")]
        public async Task<ActionResponse<Currency>> AddCurrency([FromBody] Currency currency)
        {
            ActionResponse<Currency> actionResponse = new()
            {
                ResponseType = ResponseType.Ok,
                IsSuccessful = true,
            };

            

            var checkCurrency = _context.Currencies.Where(h => h.name == currency.name).Count();
            var checkCode = _context.Currencies.Where(c => c.Code == currency.Code)?.Count();

            if (checkCurrency < 1 && checkCode < 1)
            {
                _context.Currencies.Add(currency);
                _context.SaveChanges();
            }
            return actionResponse;

        }

        [HttpGet]
        [Route("getById")]
        public async Task<ActionResponse<Currency>> GetCurrencyID([FromQuery] HotelCategoryDto model)
        {
            ActionResponse<Currency> actionResponse = new()
            {
                ResponseType = ResponseType.Ok,
                IsSuccessful = true,
            };

            var currency = await _context.Currencies.FirstOrDefaultAsync(h => h.id == model.Id);
            if (currency != null)
            {
                actionResponse.Data = currency;
            }
            return actionResponse;
        }

        [HttpDelete]
        [Route("delete")]
        public async Task<ActionResponse<Currency>> DeleteCurrency([FromQuery] CurrencyDto model)
        {
            ActionResponse<Currency> actionResponse = new()
            {
                ResponseType = ResponseType.Ok,
                IsSuccessful = true,
            };

            var currency = await _context.Currencies.FirstOrDefaultAsync(h => h.id == model.id);
            _context.Currencies.Remove(currency);
            _context.SaveChanges();
            return actionResponse;
        }

        [HttpPut]
        [Route("update")]
        public async Task<ActionResponse<Currency>> UpdateCurrency([FromQuery] CurrencyDto modelID, [FromBody] CurrencyDto model)
        {
            ActionResponse<Currency> actionResponse = new()
            {
                ResponseType = ResponseType.Ok,
                IsSuccessful = true,
            };


            try
            {
              
                var currency = await _context.Currencies.FirstOrDefaultAsync(h => h.id == modelID.id);
                var checkName = _context.Currencies.Where(h => h.name == model.name)?.Count();
                //var checkCode = _context.HotelCategories.Where(c => c.Code == model.Code)?.Count();

               

                if (checkName < 1)
                {
                    currency.name = model.name;
                   
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


