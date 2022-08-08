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
    public class CRoomListManager : ICRoomListService
    {
        ICRoom _croomDal;

        public CRoomListManager(ICRoom croomDal)
        {
            _croomDal = croomDal;
        }

        public void TAdd(CRoomList t)
        {
            _croomDal.Insert(t);
        }

        public void TDelete(CRoomList t)
        {
            _croomDal.Delete(t);
        }

        public CRoomList TGetById(int id)
        {
            return _croomDal.GetByID(id);
        }

        public List<CRoomList> TGetList()
        {
            return _croomDal.GetList();
        }

        public void TUpdate(CRoomList t)
        {
            _croomDal.Update(t);
        }
    }
}
