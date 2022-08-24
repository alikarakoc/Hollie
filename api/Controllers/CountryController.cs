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

    public class CountryController : Controller
    {

        private readonly Context _context;
        public CountryController(Context _context)
        {
            this._context = _context;
        }


        [HttpGet]
        [Route("AllCountries")]
        public ActionResponse<List<Country>> GetAllCountries()
        {
            ActionResponse<List<Country>> actionResponse = new()
            {
                ResponseType = ResponseType.Ok,
                IsSuccessful = true,
            };
            var countries = _context.Countries;

            if (countries!= null && countries.Count()>0)
            {
                actionResponse.Data = _context.Countries.Where(x => x.Status == true).ToList();
            }

            return actionResponse;

        }


        [HttpGet]
        public async Task<ActionResponse<Country>> GetCountry([FromQuery] CountryDto model)
        {
            ActionResponse<Country> actionResponse = new()
            {
                ResponseType= ResponseType.Ok,
                IsSuccessful = true,
            };

            Country country = await _context.Countries.FirstOrDefaultAsync(h => h.Id == model.Id);
            if (country != null)
            {
                actionResponse.Data = country;
            }

            return actionResponse;
        }

        [HttpPost]
        [Route("add")]

        public async Task<ActionResponse<Country>> AddCountry([FromBody] Country country)
        {
            ActionResponse<Country> actionResponse = new()
            {
                ResponseType=ResponseType.Ok,
                IsSuccessful=true,
            };

            int checkCode = _context.Countries.Where(h => h.Code == country.Code).Count();
            if (checkCode < 1)
            { 
                _context.Countries.Add(country);
                country.CreatedDate = DateTime.Now;
                country.Status = true;
                 _context.SaveChanges();
            }
            return actionResponse;
        }
        [HttpDelete]
        [Route("delete")]
        public async Task<ActionResponse<Country>> DeleteCountry([FromBody] Country model)
        {
            ActionResponse<Country> actionResponse = new()
            {
                ResponseType = ResponseType.Ok,
                IsSuccessful = true,
            };
            Country country = await _context.Countries.FirstOrDefaultAsync(h => h.Id == model.Id);
            country.UpdatedDate = DateTime.Now;
            country.Status = false;
            _context.SaveChanges();
            return actionResponse;
        }


        [HttpPut]
        [Route("update")]

        public async Task<ActionResponse<Country>> UpdateCountry([FromBody] Country model)
        {
            ActionResponse<Country> actionResponse = new()
            {
                ResponseType = ResponseType.Ok,
                IsSuccessful = true,
            };

            try
            {
                Country country = await _context.Countries.FirstOrDefaultAsync(h => h.Id == model.Id);
                int checkCode = _context.Countries.Where(h => h.Code == model.Code && h.Id != model.Id).Count();

                if(checkCode > 0)
                {
                    actionResponse.Message = "Same code exists";
                    actionResponse.IsSuccessful = false;
                }

                if (country.Code == model.Code || checkCode == 0)
                {
                    country.Code = model.Code;
                    country.Name = model.Name;
                    country.UpdatedUser = model.UpdatedUser;
                    country.UpdatedDate = DateTime.Now;
                    country.Status = true;
                    actionResponse.IsSuccessful = true;
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