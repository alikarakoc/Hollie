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
    public class CAgencyListManager : ICAgencyListService
    {
        ICAgency _cagencyDal;

        public CAgencyListManager(ICAgency cagencyDal)
        {
            _cagencyDal = cagencyDal;
        }

        public void TAdd(CAgencyList t)
        {
            _cagencyDal.Insert(t);
        }

        public void TDelete(CAgencyList t)
        {
            _cagencyDal.Delete(t);
        }

        public CAgencyList TGetById(int id)
        {
            return _cagencyDal.GetByID(id);
        }

        public List<CAgencyList> TGetList()
        {
            return _cagencyDal.GetList();
        }

        public void TUpdate(CAgencyList t)
        {
            _cagencyDal.Update(t);
        }
    }
}
