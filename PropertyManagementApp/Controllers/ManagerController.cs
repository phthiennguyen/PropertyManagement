using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using PropertyManagementApp.Models;


namespace PropertyManagementApp.Controllers
{
    public class ManagerController : Controller
    {
        PRMDBEntities1 db = new PRMDBEntities1();

        // GET: Manager
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Property()
        {
            List<Building> buildings = db.Buildings.ToList();
            return View(buildings);
        }

        #region Code With Building

        public ActionResult CreateBuilding()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateBuilding(Building building)
        {
            if (ModelState.IsValid)
            {
                db.Buildings.Add(building);
                db.SaveChanges();
                return RedirectToAction("Property");
            }
            return View(building);
        }



        public ActionResult EditBuilding(int id)
        {
            var data = db.Buildings.Where(x => x.BuildingId == id).FirstOrDefault();
            return View(data);
        }

        [HttpPost]
        public ActionResult EditBuilding(Building building)
        {
            if (ModelState.IsValid)
            {
                db.Entry(building).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Property");
            }
            return View(building);


        }

        public ActionResult DetailsBuilding(int id)
        {
            ViewBag.BuildingId = id;
            var data = db.Buildings.Where(x => x.BuildingId == id).FirstOrDefault();

            IList<Appartment> listAppartments = db.Appartments.Where(x => x.BuildingId == id).ToList();
            

            CombinedViewModel combinedViewModel = new CombinedViewModel();
            combinedViewModel.Building = data;
            combinedViewModel.appartments = listAppartments;


            return View(combinedViewModel);
        }

        public ActionResult DeleteBuilding(int id)
        {
            var data = db.Buildings.Where(x => x.BuildingId == id).FirstOrDefault();
            IList<Appartment> listAppartments = db.Appartments.Where(x => x.BuildingId == id).ToList();


            CombinedViewModel combinedViewModel = new CombinedViewModel();
            combinedViewModel.Building = data;
            combinedViewModel.appartments = listAppartments;

            return View(combinedViewModel);
        }
        
        [HttpPost, ActionName("DeleteBuilding")]
        public ActionResult DeleteBuilding1(int id)
        {
            
            IList<Appartment> appartments = db.Appartments.Where(x => x.BuildingId == id).ToList();
            foreach(Appartment appart in appartments)
            {
                db.Appartments.Remove(appart);
            }
            var data = db.Buildings.Where(x => x.BuildingId == id).FirstOrDefault();
            db.Buildings.Remove(data);
            db.SaveChanges();
            return RedirectToAction("Property");
        }

        #endregion

        public ActionResult CreateAppartment(int id)
        {
            //ModelState.AddModelError("UnitNumber", id.ToString());
            var building = db.Buildings.Where(x => x.BuildingId == id).FirstOrDefault();
            Appartment ap = new Appartment()
            {
                BuildingId = building.BuildingId,
            };
            return View(ap);
        }


        public ActionResult EditAppartment(int id)
        {
            var data = db.Appartments.Where( x => x.AppartmentId == id).FirstOrDefault();
            return View(data);
        }

        public ActionResult DeleteAppartment(int id)
        {
            var data = db.Appartments.Where(x => x.AppartmentId == id).FirstOrDefault();
            return View(data);
        }

        public ActionResult DetailsAppartment(int id)
        {
            var data = db.Appartments.Where(x => x.AppartmentId == id).FirstOrDefault();
            return View(data);
        }

        [HttpPost]
        public ActionResult CreateAppartment(Appartment appar) 
        {

            //ModelState.AddModelError("UnitNumber", appar.BuildingId.ToString());

            if (ModelState.IsValid)
            {
                
                db.Appartments.Add(appar);
                db.SaveChanges();
                return RedirectToAction("DetailsBuilding", new {id = appar.BuildingId});
            }
            return View(appar);
        }


        [HttpPost]
        public ActionResult EditAppartment(Appartment appar)
        {
            if (ModelState.IsValid)
            {
                db.Entry(appar).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("DetailsBuilding", new {id = appar.BuildingId});
            }
            return View(appar);
        }

        [HttpPost, ActionName("DeleteAppartment")]
        public ActionResult DeleteAppartment1(int id)
        {
            var data = db.Appartments.Where( x => x.AppartmentId == id).FirstOrDefault();
            db.Appartments.Remove(data);
            db.SaveChanges();
            return RedirectToAction("DetailsBuilding", new { id = data.BuildingId });

        }

        public ActionResult SignOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

    }
}