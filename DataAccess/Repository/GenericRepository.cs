using DataAccess.Abstract;
using DataAccess.Concrate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class GenericRepository<T> : IGeneric<T> where T : class
    {

        private readonly Context c;
        public GenericRepository(Context _context){
            this.c = _context;
        }

            

        public void Delete(T t)
        {
            //using var c = new Context();

            c.Remove(t);
            c.SaveChanges();
        }

        public T GetByID(int id)
        {
            //using var c = new Context();
            return c.Set<T>().Find(id);
        }

        public List<T> GetList()
        {
            //using var c = new Context();
            return c.Set<T>().ToList();
        }

        public void Insert(T t)
        {
            //using var c = new Context();
            c.Add(t);
            c.SaveChanges();
        }

        public void Update(T t)
        {
            //using var c = new Context();
            c.Update(t);
            c.SaveChanges();
        }
    }
}
