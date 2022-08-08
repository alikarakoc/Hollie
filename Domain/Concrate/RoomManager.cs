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
    public class RoomManager : IRoomService
    {
        IRoom _roomDal;

        public RoomManager(IRoom roomDal)
        {
            _roomDal = roomDal;
        }

        public void TAdd(Room t)
        {
            _roomDal.Insert(t);
        }

        public void TDelete(Room t)
        {
            _roomDal.Delete(t);
        }

        public Room TGetById(int id)
        {
            return _roomDal.GetByID(id);
        }

        public List<Room> TGetList()
        {
            return _roomDal.GetList();
        }

        public void TUpdate(Room t)
        {
            _roomDal.Update(t);
        }
    }
}
