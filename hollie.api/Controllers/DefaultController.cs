using Application.Concrete;
using DataAccess.Concrate;
using DataAccess.EntityFramework;
using Domain.Concrate;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace hollie.api.Controllers
{
    public class DefaultController : Controller
    {
        public ActionResult Index()
        {
            AgencyManager Agencymanager = new AgencyManager(new
            EfAgencyDal());
            var values = Agencymanager.TGetList();
            return View(values);
        }
        [HttpGet]
        public PartialViewResult AgencyInsert()
        {
            return PartialView();
        }
        [HttpPost]
        public PartialViewResult AgencyInsert(Agency p)
        {
            AgencyManager agencyManager = new(new EfAgencyDal());
            p.CreatedDate = Convert.ToDateTime(DateTime.Now.ToShortDateString());
            p.UpdatedDate = Convert.ToDateTime(DateTime.Now.ToShortDateString());
            if (p.Code != null)
            { 
            agencyManager.TAdd(p);
            }
            return PartialView("/Views/Home/Index.cshtml");
        }
        [HttpGet]
        public IActionResult AgencyGetir(int id)
        {
            AgencyManager agencyManager = new(new EfAgencyDal());
            var values = agencyManager.TGetById(id);
          return View(values);
        }
        [HttpPost]
        public IActionResult AgencyGetir(Agency agency)
        {
            AgencyManager agencyManager = new(new EfAgencyDal());
            agency.UpdatedDate = Convert.ToDateTime(DateTime.Now.ToShortDateString());
            agency.CreatedDate = Convert.ToDateTime(DateTime.Now.ToShortDateString());
            agencyManager.TUpdate(agency);
            return RedirectToAction("Index");
        }
        public IActionResult AgencySil(int id)
        {
            AgencyManager agencyManager = new(new EfAgencyDal());
            var values = agencyManager.TGetById(id);
            agencyManager.TDelete(values);
            return RedirectToAction("Index");
        }
    }
}
