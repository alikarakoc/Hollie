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
    public class AMarketListManager : IAMarketListService
    {
        IAMarket _amarketDal;

        public AMarketListManager(IAMarket cmarketDal)
        {
            _amarketDal = cmarketDal;
        }

        public void TAdd(MarketListA t)
        {
            _amarketDal.Insert(t);
        }

        public void TDelete(MarketListA t)
        {
            _amarketDal.Delete(t);
        }

        public MarketListA TGetById(int id)
        {
            return _amarketDal.GetByID(id);
        }

        public List<MarketListA> TGetList()
        {
            return _amarketDal.GetList();
        }

        public void TUpdate(MarketListA t)
        {
            _amarketDal.Update(t);
        }
    }
}
