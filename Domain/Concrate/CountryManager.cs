using Application.Concrete;
using DataAccess.Abstract;
using Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Concrate
{
    public class CountryManager : ICountryService
    {
        ICountry _countryDal;

        public CountryManager(ICountry countryDal)
        {
            _countryDal = countryDal;
        }

        public void TAdd(Country t)
        {
            _countryDal.Insert(t);
        }

        public void TDelete(Country t)
        {
            _countryDal.Delete(t);
        }

        public Country TGetById(int id)
        {
            return _countryDal.GetByID(id);
        }

        public List<Country> TGetList()
        {
            return _countryDal.GetList();
        }

        public void TUpdate(Country t)
        {
            _countryDal.Update(t);
        }
    }
}
