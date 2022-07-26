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
    public class CHotelListManager : ICHotelListService
    {
        ICHotel _chotelDal;

        public CHotelListManager(ICHotel chotelDal)
        {
            _chotelDal = chotelDal;
        }

        public void TAdd(CHotelList t)
        {
            _chotelDal.Insert(t);
        }

        public void TDelete(CHotelList t)
        {
            _chotelDal.Delete(t);
        }

        public CHotelList TGetById(int id)
        {
            return _chotelDal.GetByID(id);
        }

        public List<CHotelList> TGetList()
        {
            return _chotelDal.GetList();
        }

        public void TUpdate(CHotelList t)
        {
            _chotelDal.Update(t);
        }
    }
}
