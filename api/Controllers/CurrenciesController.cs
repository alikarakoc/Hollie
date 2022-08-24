using Application.Concrete;
using Application.Dtos;
using Application.Infrastructure;
using DataAccess.Concrate;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
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
                actionResponse.Data = _context.Currencies.ToList();

            }
            return actionResponse;
        }

        [HttpGet]
        [Route("updateCurrency")]
        public async Task<ActionResponse<List<Currency>>> UpdateCurrencies()
        {
            ActionResponse<List<Currency>> actionResponse = new()
            {
                ResponseType = ResponseType.Ok,
                IsSuccessful = true,
            };
            var currencies = _context.Currencies;
            if (DateTime.Now.ToString("dddd") != "Cumartesi" || DateTime.Now.ToString("dddd") != "Pazar")
            {

                var connectionString = _context.Database.GetDbConnection().ConnectionString;
                SqlConnection con = new SqlConnection(connectionString);
                SqlCommand cmd = new SqlCommand();
                con.Open();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "UPR_GetDovizKurlari_MerkezBankasi";
                cmd.Parameters.Add("pYil", SqlDbType.NVarChar, 50).Value = DateTime.Now.Year;
                cmd.Parameters.Add("pAy", SqlDbType.NVarChar, 50).Value = DateTime.Now.Month;
                cmd.Parameters.Add("pGun", SqlDbType.NVarChar, 50).Value = DateTime.Now.Day;
                cmd.ExecuteNonQuery();
                con.Close();
                var deger3 = currencies.Count();
                if(deger3 <1)
                {
                    int i = 0;
                    while(deger3 <= 0)
                    {
                        i++;
                        SqlCommand cmd2 = new SqlCommand();
                        con.Open();
                        cmd2.Connection = con;
                        cmd2.CommandType = CommandType.StoredProcedure;
                        cmd2.CommandText = "UPR_GetDovizKurlari_MerkezBankasi";
                        cmd2.Parameters.Add("pYil", SqlDbType.NVarChar, 50).Value = DateTime.Now.Year;
                        cmd2.Parameters.Add("pAy", SqlDbType.NVarChar, 50).Value = DateTime.Now.Month;
                        cmd2.Parameters.Add("pGun", SqlDbType.NVarChar, 50).Value = DateTime.Now.Day - i;
                        cmd2.ExecuteNonQuery();
                        con.Close();
                        deger3 = currencies.Count();
                    }
                }
              
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

            

            //var checkName = _context.Currencies.Where(h => h.Name == currency.Name).Count();
            var checkCode = _context.Currencies.Where(c => c.Code == currency.Code)?.Count();

            if (checkCode < 1 )
            {
                _context.Currencies.Add(currency);
                _context.SaveChanges();
            }
            return actionResponse;

        }

        [HttpGet]
        [Route("getById")]
        public async Task<ActionResponse<Currency>> GetCurrencyID([FromQuery] CurrencyDto model)
        {
            ActionResponse<Currency> actionResponse = new()
            {
                ResponseType = ResponseType.Ok,
                IsSuccessful = true,
            };

            var currency = await _context.Currencies.FirstOrDefaultAsync(h => h.Id == model.Id);
            if (currency != null)
            {
                actionResponse.Data = currency;
            }
            return actionResponse;
        }

        [HttpDelete]
        [Route("delete")]
        public async Task<ActionResponse<Currency>> DeleteCurrency([FromBody] CurrencyDto model)
        {
            ActionResponse<Currency> actionResponse = new()
            {
                ResponseType = ResponseType.Ok,
                IsSuccessful = true,
            };

            var currency = await _context.Currencies.FirstOrDefaultAsync(h => h.Id == model.Id);
            _context.Remove(currency);
            _context.SaveChanges();
            return actionResponse;
        }

        [HttpPut]
        [Route("update")]
        public async Task<ActionResponse<Currency>> UpdateCurrency([FromBody] CurrencyDto model)
        {
            ActionResponse<Currency> actionResponse = new()
            {
                ResponseType = ResponseType.Ok,
                IsSuccessful = true,
            };


            try
            {
                Currency currency = await _context.Currencies.FirstOrDefaultAsync(h => h.Id == model.Id);
                //var checkName = _context.Currencies.Where(h => h.Name == model.Name)?.Count();
                //var checkCode = _context.Currencies.Where(c => c.Code == model.Code)?.Count();

                currency.Code = model.Code;
                currency.Name = model.Name;
                currency.Value = model.Value;
                _context.SaveChanges();
                
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



