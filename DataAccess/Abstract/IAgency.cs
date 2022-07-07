using Application.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface IAgency
    {
        void Insert();
        void Delete();
        void Update();
        List<Agency> GetList();
    }
}
