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
     public class ContractManager : IContractService
    {
        IContract _contractDal;

        public ContractManager(IContract contractDal)
        {
            _contractDal = contractDal;
        }

        public void TAdd(Contract t)
        {
            _contractDal.Insert(t);
        }

        public void TDelete(Contract t)
        {
            _contractDal.Delete(t);
        }

        public Contract TGetById(int id)
        {
            return _contractDal.GetByID(id);
        }

        public List<Contract> TGetList()
        {
            return _contractDal.GetList();
        }

        public void TUpdate(Contract t)
        {
            _contractDal.Update(t);
        }
    }
}
