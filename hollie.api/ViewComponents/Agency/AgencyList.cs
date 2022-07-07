using DataAccess.EntityFramework;
using Domain.Concrate;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace hollie.api.ViewComponents.Agency
{
    public class AgencyList : ViewComponent
    {
        AgencyManager Agencymanager = new AgencyManager(new
            EfAgencyDal());
        public IViewComponentResult Invoke()
        {
            var values = Agencymanager.TGetList();
            return View(values);
        }
    }
}
