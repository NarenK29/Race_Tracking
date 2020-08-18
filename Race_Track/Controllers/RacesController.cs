using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using Race_Track.Models;
using System.Web.Script.Serialization;

namespace Race_Track.Controllers
{
    public class RacesController : Controller
    {
        private RaceContext db = new RaceContext();



        [HttpGet]
        public JsonResult GetLiveVehicles()
        {
            List<Race> raceVehicles = new List<Race>();
            try
            {

                using (RaceContext rcontext = new RaceContext())
                {
                    var vehicles = (from s in rcontext.race select s).ToList();
                    if (vehicles.Count > 0)
                    {
                        raceVehicles = vehicles;
                    }
                }
            }
            catch (Exception ex)
            {
                using (EventLog eventLog = new EventLog("Application"))
                {
                    eventLog.Source = "Race_Tracking";
                    eventLog.WriteEntry("Error while fetching live vehicles : " + ex.ToString(), EventLogEntryType.Error, 101, 1);
                }
            }
            var jsonSerialiser = new JavaScriptSerializer();
            var jsonData = jsonSerialiser.Serialize(raceVehicles);
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public JsonResult GetVehiclesCount()
        {
            int vehiclesCount = 0;
            try
            {

                using (RaceContext rcontext = new RaceContext())
                {
                    var vehicles = (from s in rcontext.race select s).ToList();
                    if (vehicles.Count > 0)
                    {
                        vehiclesCount = vehicles.Count;
                    }
                }
            }
            catch (Exception ex)
            {
                using (EventLog eventLog = new EventLog("Application"))
                {
                    eventLog.Source = "Race_Tracking";
                    eventLog.WriteEntry("Error while fetching active vehicles count : " + ex.ToString(), EventLogEntryType.Error, 101, 1);
                }
            }

            return Json(new { data = vehiclesCount }, JsonRequestBehavior.AllowGet);
        }


        // GET: Races
        public ActionResult Index()
        {
            try
            {
                return View(db.race.ToList());
            }
            catch (Exception ex)
            {
                using (EventLog eventLog = new EventLog("Application"))
                {
                    eventLog.Source = "Race_Tracking";
                    eventLog.WriteEntry("Error While loading race vehicles" + ex.ToString(), EventLogEntryType.Error, 101, 1);
                }
                return View("~/Views/Shared/Error.cshtml");
            }
        }

        // GET: Races/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            try
            {
                Race race = db.race.Find(id);
                if (race == null)
                {
                    return HttpNotFound();
                }
                return View(race);
            }
            catch (Exception ex)
            {
                using (EventLog eventLog = new EventLog("Application"))
                {
                    eventLog.Source = "Race_Tracking";
                    eventLog.WriteEntry("Error While Loading Race Vehicle details : " + ex.ToString(), EventLogEntryType.Error, 101, 1);
                }
                return View("~/Views/Shared/Error.cshtml");
            }
        }

        // GET: Races/Create
        public ActionResult Create()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                using (EventLog eventLog = new EventLog("Application"))
                {
                    eventLog.Source = "Race_Tracking";
                    eventLog.WriteEntry("Error While Loading Add Vehicle Page : " + ex.ToString(), EventLogEntryType.Error, 101, 1);
                }
                return View("~/Views/Shared/Error.cshtml");
            }
        }

        // POST: Races/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,Type")] Race race)
        {
            try
            {
                string tire_wear = Convert.ToString(Request.Form["Tire_Wear"]);
                string ground_clearance = Convert.ToString(Request.Form["Ground_Clearance"]);
                if (ModelState.IsValid)
                {

                    race.ID = GenerateVehicleId();


                    //Used Dependency Injection and Composition
                    if (race.Type.Equals("Car"))
                    {
                        Vehicle carObj = new Vehicle();
                        carObj.Vehicle_ID = race.ID;
                        carObj.Tire_Wear = Convert.ToDouble(tire_wear);
                        carObj.Tow_Strap = true;

                        Car car = new Car(race.Name, carObj);
                        race.VehicleID = car.ID;
                    }

                    if (race.Type.Equals("Truck"))
                    {
                        Vehicle truckObj = new Vehicle();
                        truckObj.Vehicle_ID = race.ID;
                        truckObj.Ground_Clearance = Convert.ToDouble(ground_clearance);
                        truckObj.Tow_Strap = true;

                        Truck truck = new Truck(race.Name, truckObj);
                        race.VehicleID = truck.ID;
                    }
                    db.race.Add(race);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                return View(race);
            }
            catch (Exception ex)
            {
                using (EventLog eventLog = new EventLog("Application"))
                {
                    eventLog.Source = "Race_Tracking";
                    eventLog.WriteEntry("Error While Loading Adding Vehicle to race : " + ex.ToString(), EventLogEntryType.Error, 101, 1);
                }
                return View("~/Views/Shared/Error.cshtml");
            }

        }

        // GET: Races/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Race race = db.race.Find(id);
            if (race == null)
            {
                return HttpNotFound();
            }
            return View(race);
        }

        // POST: Races/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Race race = db.race.Find(id);
            db.race.Remove(race);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public int GenerateVehicleId()
        {
            int id = 1;
            try
            {
                var races = (from s in db.race select s).ToList();
                var vehiclesCount = races.Count;
                if (vehiclesCount > 0)
                {
                    id = races.Max(s => s.ID) + 1;
                }

            }
            catch (Exception ex)
            {
                using (EventLog eventLog = new EventLog("Application"))
                {
                    eventLog.Source = "Race_Tracking";
                    eventLog.WriteEntry("Error While generating Vehicle Id : " + ex.ToString(), EventLogEntryType.Error, 101, 1);
                }

            }
            return id;
        }
    }
}
