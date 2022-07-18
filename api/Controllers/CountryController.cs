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
                actionResponse.Data = countries.ToList();
            }

            return actionResponse;

        }


        [HttpGet]
        public async Task<ActionResponse<Country>> GetCountry([FromQuery] GetAllCountryDto model)
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
        [Route("AddCountry")]

        public async Task<ActionResponse<Country>> AddCountry([FromBody] Country cnt)
        {
            ActionResponse<Country> actionResponse = new()
            {
                ResponseType=(ResponseType)ResponseType.Ok,
                IsSuccessful=true,
            };
            _context.Countries.Add(cnt);
            _context.SaveChanges();
            return actionResponse;
        }

        [HttpDelete]
        [Route("DeleteCountry")]
        public async Task<ActionResponse<Country>> DeleteCountry([FromQuery] GetAllCountryDto model)
        {
            ActionResponse<Country> actionResponse = new()
            {
                ResponseType = ResponseType.Ok,
                IsSuccessful = true,
            };
            var country = await _context.Countries.FirstOrDefaultAsync(h => h.Id == model.Id);
            _context.Boards.Remove(country);
            _context.SaveChanges();
            return actionResponse;
        }


        [HttpPut]
        [Route("DeleteCountry")]

        public async Task<ActionResponse<Country>> UpdateCountry([FromQuery] GetAllCountryDto modelD, [FromBody] GetAllCountryDto model)
        {
            ActionResponse<Country> actionResponse = new()
            {
                ResponseType = ResponseType.Ok,
                IsSuccessful = true,
            };

            try
            {
                var country = await _context.Boards.FirstOrDefaultAsync(h => h.Id == modelD.Id);
                if (country != null)
                {
                    country.Code = model.Code;
                    country.Name = model.Name;
                    country.CreatedDate = model.CreatedDate;
                    country.CreatedUser = model.CreatedUser;
                    country.UpdatedDate = model.UpdatedDate;
                    country.UpdateUser = model.UpdateUser;
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