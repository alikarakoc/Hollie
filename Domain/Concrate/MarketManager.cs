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
    public class MarketManager : IMarketService
    {
        IMarket _marketDal;

        public MarketManager(IMarket marketDal)
        {
            _marketDal = marketDal;
        }

        public void TAdd(Market t)
        {
            _marketDal.Insert(t);
        }

        public void TDelete(Market t)
        {
            _marketDal.Delete(t);
        }

        public Market TGetById(int id)
        {
            return _marketDal.GetByID(id);
        }

        public List<Market> TGetList()
        {
            return _marketDal.GetList();
        }

        public void TUpdate(Market t)
        {
            _marketDal.Update(t);
        }
    }
}
