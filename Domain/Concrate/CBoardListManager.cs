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
    public class CBoardListManager : ICBoardListService
    {
        ICBoard _cboardDal;

        public CBoardListManager(ICBoard cboardDal)
        {
            _cboardDal = cboardDal;
        }

        public void TAdd(CBoardList t)
        {
            _cboardDal.Insert(t);
        }

        public void TDelete(CBoardList t)
        {
            _cboardDal.Delete(t);
        }

        public CBoardList TGetById(int id)
        {
            return _cboardDal.GetByID(id);
        }

        public List<CBoardList> TGetList()
        {
            return _cboardDal.GetList();
        }

        public void TUpdate(CBoardList t)
        {
            _cboardDal.Update(t);
        }
    }
}
