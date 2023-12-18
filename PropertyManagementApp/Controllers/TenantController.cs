using PropertyManagementApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PropertyManagementApp.Controllers
{
    public class TenantController : Controller
    {
        PRMDBEntities1 db = new PRMDBEntities1();

        // GET: Tenant
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Property()
        {
            List<Building> buildings = db.Buildings.ToList();
            return View(buildings);
        }

        public ActionResult DetailsBuilding(int id)
        {
            var data = db.Buildings.Where(x => x.BuildingId == id).FirstOrDefault();

            IList<Appartment> listAppartments = db.Appartments.Where(x => x.BuildingId == id).ToList();


            CombinedViewModel combinedViewModel = new CombinedViewModel();
            combinedViewModel.Building = data;
            combinedViewModel.appartments = listAppartments;


            return View(combinedViewModel);

        }

        public ActionResult DetailsAppartment(int id)
        {
            var data = db.Appartments.Where(x => x.AppartmentId == id).FirstOrDefault();
            return View(data);
        }



    }
}