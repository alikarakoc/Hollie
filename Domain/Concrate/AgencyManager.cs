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
    public class AgencyManager : IAgencyService
    {
        IAgency _agencyDal;

        public AgencyManager(IAgency agencyDal)
        {
            _agencyDal = agencyDal;
        }

        public void TAdd(Agency t)
        {
            _agencyDal.Insert(t);
        }

        public void TDelete(Agency t)
        {
            _agencyDal.Delete(t);
        }

        public Agency TGetById(int id)
        {
            return _agencyDal.GetByID(id);
        }

        public List<Agency> TGetList()
        {
          return  _agencyDal.GetList();
        }

        public void TUpdate(Agency t)
        {
           _agencyDal.Update(t);
        }
    }
}
