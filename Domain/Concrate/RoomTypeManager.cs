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
    public class RoomTypeManager : IRoomTypeService
    {
        IRoomType _roomtypeDal;

        public RoomTypeManager(IRoomType roomtypeDal)
        {
            _roomtypeDal = roomtypeDal;
        }

        public void TAdd(RoomType t)
        {
            _roomtypeDal.Insert(t);
        }

        public void TDelete(RoomType t)
        {
            _roomtypeDal.Delete(t);
        }

        public RoomType TGetById(int id)
        {
            return _roomtypeDal.GetByID(id);
        }

        public List<RoomType> TGetList()
        {
            return _roomtypeDal.GetList();
        }

        public void TUpdate(RoomType t)
        {
            _roomtypeDal.Update(t);
        }
    }
}
