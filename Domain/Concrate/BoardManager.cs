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
    public class BoardManager : IBoardService
    {
        IBoard _boardDal;

        public BoardManager(IBoard boardDal)
        {
            _boardDal = boardDal;
        }

        public void TAdd(Board t)
        {
            _boardDal.Insert(t);
        }

        public void TDelete(Board t)
        {
            _boardDal.Delete(t);
        }

        public Board TGetById(int id)
        {
            return _boardDal.GetByID(id);
        }

        public List<Board> TGetList()
        {
            return _boardDal.GetList();
        }

        public void TUpdate(Board t)
        {
            _boardDal.Update(t);
        }
    }
}
