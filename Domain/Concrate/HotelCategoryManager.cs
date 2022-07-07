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
    public class HotelCategoryManager : IHotelCategoryService
    {
        IHotelCategory _hotelcategoryDal;

        public HotelCategoryManager(IHotelCategory hotelcategoryDal)
        {
            _hotelcategoryDal = hotelcategoryDal;
        }

        public void TAdd(HotelCategory t)
        {
            _hotelcategoryDal.Insert(t);
        }

        public void TDelete(HotelCategory t)
        {
            _hotelcategoryDal.Delete(t);
        }

        public HotelCategory TGetById(int id)
        {
            return _hotelcategoryDal.GetByID(id);
        }

        public List<HotelCategory> TGetList()
        {
            return _hotelcategoryDal.GetList();
        }

        public void TUpdate(HotelCategory t)
        {
            _hotelcategoryDal.Update(t);
        }
    }
}
