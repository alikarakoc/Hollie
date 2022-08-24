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
    public class MarketController : Controller
    {
        string bilgisayarAdi = Dns.GetHostName();

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
                actionResponse.Data = _context.Markets.Where(x => x.Status == true).ToList();
            }
            return actionResponse;
        }

        [HttpGet]
        [Route("GetMarketSelectList")]
        public ActionResponse<List<MarketAgencySelectorDto>> GetMarketSelectList()
        {
            ActionResponse<List<MarketAgencySelectorDto>> actionResponse = new()
            {
                ResponseType = ResponseType.Ok,
                IsSuccessful = true,
            };

            var markets = _context.Markets.Where(x => x.Status == true).ToList();
            var marketAgencies = _context.AMarkets.ToList();
            actionResponse.Data = new List<MarketAgencySelectorDto>();
            foreach (var market in markets)
            {
                actionResponse.Data.Add(new MarketAgencySelectorDto()
                {
                    MarketId = market.Id,
                    Agencies = marketAgencies.Where(p => p.MarketId == market.Id).Select(s => s.ListId).ToList()
                });
            }

            return actionResponse;
        }


        [HttpGet]
        public async Task<ActionResponse<Market>> GetMarkets([FromQuery] MarketDto model)
        {
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
        public async Task<ActionResponse<Market>> AddMarket([FromBody] Market market)
        {

            ActionResponse<Market> actionResponse = new()
            {
                ResponseType = ResponseType.Ok,
                IsSuccessful = true,
            };
;

            int checkCode = _context.Markets.Where(h => h.Code == market.Code).Count();
            if (checkCode < 1) { 
                 _context.Markets.Add(market);
                market.CreatedDate = DateTime.Now;
                market.Status = true;
                _context.SaveChanges();
            }
            return actionResponse;
        }

        [HttpDelete]
        [Route("delete")]
        public async Task<ActionResponse<Hotel>> DeleteMarket([FromBody] MarketDto model)
        {
            ActionResponse<Hotel> actionResponse = new()
            {
                ResponseType = ResponseType.Ok,
                IsSuccessful = true,
            };
            var market = await _context.Markets.FirstOrDefaultAsync(h => h.Id == model.Id);
            market.UpdatedDate = DateTime.Now;
            market.Status = false;
            _context.SaveChanges();
            return actionResponse;
        }

        [HttpPut]
        [Route("update")]
        public async Task<ActionResponse<Market>> UpdateMarket([FromBody] MarketDto model)
        {
            ActionResponse<Market> actionResponse = new()
            {
                ResponseType = ResponseType.Ok,
                IsSuccessful = true,
            };

            try
            {
                Market market = await _context.Markets.FirstOrDefaultAsync(h => h.Id == model.Id);
                int checkCode = _context.Markets.Where(h => h.Code == model.Code).Count();

                if (checkCode > 0)
                {
                    actionResponse.Message = "Same code exists";
                    actionResponse.IsSuccessful = false;
                }
                if (market.Code == model.Code || checkCode == 0)
                {
                    market.Code = model.Code;
                    market.Name = model.Name;
                    market.UpdatedUser = model.UpdatedUser;
                    market.UpdatedDate = DateTime.Now;
                    market.Status = true;
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
