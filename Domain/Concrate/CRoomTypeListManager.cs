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
    public class CRoomTypeListManager : ICRoomTypeListService
    {
        ICRoomType _croomTypeDal;

        public CRoomTypeListManager(ICRoomType croomTypeDal)
        {
            _croomTypeDal = croomTypeDal;
        }

        public void TAdd(CRoomTypeList t)
        {
            _croomTypeDal.Insert(t);
        }

        public void TDelete(CRoomTypeList t)
        {
            _croomTypeDal.Delete(t);
        }

        public CRoomTypeList TGetById(int id)
        {
            return _croomTypeDal.GetByID(id);
        }

        public List<CRoomTypeList> TGetList()
        {
            return _croomTypeDal.GetList();
        }

        public void TUpdate(CRoomTypeList t)
        {
            _croomTypeDal.Update(t);
        }
    }
}
