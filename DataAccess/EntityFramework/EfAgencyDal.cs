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
    public class EfAgencyDal : GenericRepository<Agency>, IAgency
    {
        public EfAgencyDal(Context _context) : base(_context)
        {
        }
    }
}
