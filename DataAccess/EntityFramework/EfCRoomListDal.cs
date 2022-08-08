﻿using Application.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrate;
using DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.EntityFramework
{
    public class EfCRoomList : GenericRepository<CRoomList>, ICRoom
    {
        public EfCRoomList(Context _context) : base(_context)
        {
        }
    }
}
