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
    public class CMarketListManager : ICMarketListService
    {
        ICMarket _cmarketDal;

        public CMarketListManager(ICMarket cmarketDal)
        {
            _cmarketDal = cmarketDal;
        }

        public void TAdd(CMarketList t)
        {
            _cmarketDal.Insert(t);
        }

        public void TDelete(CMarketList t)
        {
            _cmarketDal.Delete(t);
        }

        public CMarketList TGetById(int id)
        {
            return _cmarketDal.GetByID(id);
        }

        public List<CMarketList> TGetList()
        {
            return _cmarketDal.GetList();
        }

        public void TUpdate(CMarketList t)
        {
            _cmarketDal.Update(t);
        }
    }
}
