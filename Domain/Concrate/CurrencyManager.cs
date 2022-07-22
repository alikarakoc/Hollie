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
    public class CurrencyManager : ICurrencyService
    {
        ICurrency _currencyDal;

        public CurrencyManager(ICurrency currencyDal)
        {
            _currencyDal = currencyDal;
        }

        public void TAdd(Currency t)
        {
            _currencyDal.Insert(t);
        }

        public void TDelete(Currency t)
        {
            _currencyDal.Delete(t);
        }

        public Currency TGetById(int id)
        {
            return _currencyDal.GetByID(id);
        }

        public List<Currency> TGetList()
        {
            return _currencyDal.GetList();
        }

        public void TUpdate(Currency t)
        {
            _currencyDal.Update(t);
        }
    }
}
