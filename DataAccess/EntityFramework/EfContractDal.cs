using DataAccess.Abstract;
using DataAccess.Repository;
using Application.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Concrate;

namespace DataAccess.EntityFramework
{
    public class EfContractDal : GenericRepository<Agency>, IAgency
    {
        public EfContractDal(Context _context) : base(_context)
        {
        }
    }
}
