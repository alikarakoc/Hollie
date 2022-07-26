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
    public class CRoomTypeListManager : ICRoomTypeService
    {
        ICRoomType _croomtypeDal;

        public CRoomTypeListManager(ICRoomType croomtypeDal)
        {
            _croomtypeDal = croomtypeDal;
        }

        public void TAdd(CRoomTypeList t)
        {
            _croomtypeDal.Insert(t);
        }

        public void TDelete(CRoomTypeList t)
        {
            _croomtypeDal.Delete(t);
        }

        public CRoomTypeList TGetById(int id)
        {
            return _croomtypeDal.GetByID(id);
        }

        public List<CRoomTypeList> TGetList()
        {
            return _croomtypeDal.GetList();
        }

        public void TUpdate(CRoomTypeList t)
        {
            _croomtypeDal.Update(t);
        }
    }
}
