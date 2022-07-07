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
    public class HotelManager : IHotelService
    {
        IHotel _hotelDal;

        public HotelManager(IHotel hotelDal)
        {
            _hotelDal = hotelDal;
        }

        public void TAdd(Hotel t)
        {
            _hotelDal.Insert(t);
        }

        public void TDelete(Hotel t)
        {
            _hotelDal.Delete(t);
        }

        public Hotel TGetById(int id)
        {
            return _hotelDal.GetByID(id);
        }

        public List<Hotel> TGetList()
        {
            return _hotelDal.GetList();
        }

        public void TUpdate(Hotel t)
        {
            _hotelDal.Update(t);
        }
    }
}
