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

            var country = await _context.Countries.FirstOrDefaultAsync(h => h.Id == model.Id);
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

            //var checkName = _context.Countries.Where(h => h.Name == country.Name).Count();
            //if (checkName < 1)
            var checkCode = _context.Countries.Where(h => h.Code == country.Code).Count();
            if (checkCode < 1)
            { 
            _context.Countries.Add(country);
            country.Status = true;
            _context.SaveChanges();
            }
            return actionResponse;    
}
        [HttpDelete]
        [Route("delete")]
        public async Task<ActionResponse<Country>> DeleteCountry([FromQuery] CountryDto model)
        {
            ActionResponse<Country> actionResponse = new()
            {
                ResponseType = ResponseType.Ok,
                IsSuccessful = true,
            };
            var country = await _context.Countries.FirstOrDefaultAsync(h => h.Id == model.Id);
            country.Status = false;
            _context.SaveChanges();
            return actionResponse;
        }


        [HttpPut]
        [Route("update")]

        public async Task<ActionResponse<Country>> UpdateCountry([FromQuery] CountryDto modelD, [FromBody] CountryDto model)
        {
            ActionResponse<Country> actionResponse = new()
            {
                ResponseType = ResponseType.Ok,
                IsSuccessful = true,
            };

            try
            {
                var country = await _context.Countries.FirstOrDefaultAsync(h => h.Id == modelD.Id);
                //var checkName = _context.Countries.Where(h=>h.Name == model.Name)?.Count();
                //if (checkName <1 && country != null)
                var checkCode = _context.Countries.Where(h => h.Code == model.Code)?.Count();
                if (country.Code == model.Code)
                {
                    country.Name = model.Name;
                    country.CreatedDate = model.CreatedDate;
                    country.CreatedUser = model.CreatedUser;
                    country.UpdatedDate = model.UpdatedDate;
                    country.UpdateUser = model.UpdateUser;
                    country.Status = true;
                    _context.SaveChanges();
                }

                else if (checkCode < 1 && country != null)
                {
                    country.Code = model.Code;
                    country.Name = model.Name;
                    country.CreatedDate = model.CreatedDate;
                    country.CreatedUser = model.CreatedUser;
                    country.UpdatedDate = model.UpdatedDate;
                    country.UpdateUser = model.UpdateUser;
                    country.Status = true;
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