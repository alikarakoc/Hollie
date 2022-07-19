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
    public class MarketController : Controller
    {
        private readonly Context _context;
        public MarketController(Context _context)
        {
            this._context = _context;
        }


        

        [HttpGet]
        [Route("AllMarkets")]
        public ActionResponse<List<Market>> Market()
        {
            ActionResponse<List<Market>> actionResponse = new()
            {
                ResponseType = ResponseType.Ok,
                IsSuccessful = true,
            };

            var markets = _context.Markets;
            
            if (markets != null && markets.Count() > 0)
            {
                actionResponse.Data = markets.ToList();
            }
            return actionResponse;
        }



        [HttpGet]
        public async Task<ActionResponse<Market>> GetMarkets([FromQuery] MarketDto model)
        {
            if (model is null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            ActionResponse<Market> actionResponse = new()
            {
                ResponseType = ResponseType.Ok,
                IsSuccessful = true,
            };
            var markets = await _context.Markets.FirstOrDefaultAsync(h => h.Id == model.Id);
            if (markets != null)
            {
                actionResponse.Data = markets;
            }
            return actionResponse;
        }



        [HttpPost]
        [Route("add")]
        public async Task<ActionResponse<Market>> AddMarket([FromBody] Market mrk)
        {

            ActionResponse<Market> actionResponse = new()
            {
                ResponseType = ResponseType.Ok,
                IsSuccessful = true,
            };

            var checkMarket = _context.Markets.Where(h => h.Name == mrk.Name).Count();
            if (checkMarket < 1) { 
                 _context.Markets.Add(mrk);
                 _context.SaveChanges();
            }
            return actionResponse;
        }

        [HttpDelete]
        [Route("delete")]
        public async Task<ActionResponse<Hotel>> DeleteMarket([FromQuery] MarketDto model)
        {
            ActionResponse<Hotel> actionResponse = new()
            {
                ResponseType = ResponseType.Ok,
                IsSuccessful = true,
            };
            var market = await _context.Markets.FirstOrDefaultAsync(h => h.Id == model.Id);
            _context.Markets.Remove(market);
            _context.SaveChanges();
            return actionResponse;
        }

        [HttpPut]
        [Route("update")]
        public async Task<ActionResponse<Market>> UpdateMarket([FromQuery]MarketDto modelID, [FromBody] MarketDto model)
        {
            ActionResponse<Market> actionResponse = new()
            {
                ResponseType = ResponseType.Ok,
                IsSuccessful = true,
            };

            try
            {
                var market = await _context.Markets.FirstOrDefaultAsync(h => h.Id == modelID.Id);
                var checkMarket =  _context.Markets.Where(h => h.Name == model.Name)?.Count();
                if (checkMarket<1 && market != null)
                {
                    market.Code = model.Code;
                    market.Name = model.Name;
                    market.CreatedDate = model.CreatedDate;
                    market.CreatedUser = model.CreatedUser;
                    market.UpdatedDate = model.UpdatedDate;
                    market.UpdateUser = model.UpdateUser;
                
                   
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
