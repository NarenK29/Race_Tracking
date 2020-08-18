using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Race_Track;
using Race_Track.Controllers;
using Race_Track.Models;

namespace Race_Track.Tests.Controllers
{
    [TestClass]
    public class RacesControllerTest
    {
        //To test if the application has Home page listing all vehicles 
        [TestMethod]
        public void Index()
        {
            // Arrange
            RacesController controller = new RacesController();

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        //To test if the application has Add Vehicle form
        [TestMethod]
        public void Create()
        {
            // Arrange
            RacesController controller = new RacesController();

            // Act
            ViewResult result = controller.Create() as ViewResult;                      

            // Assert            
            Assert.IsNotNull(result);
        }

        //To test if GetVehiclesCount Exists and it returns non null value
        [TestMethod]
        public void GetVehiclesCount()
        {
            // Arrange
            RacesController controller = new RacesController();

            // Act
            JsonResult result = controller.GetVehiclesCount() as JsonResult;           

            //Assert
            Assert.IsNotNull(result);
        }

        //To test if GetLiveVehicles Exists and returns live Vehicles Count
        [TestMethod]
        public void GetLiveVehicles()
        {
            // Arrange
            RacesController controller = new RacesController();

            // Act
            JsonResult result = controller.GetLiveVehicles() as JsonResult;
            //Assert
            Assert.IsNotNull(result);
            
        }

        //To Test Exception View is handled correctly
        [TestMethod]
        public void RaceDetailsView()
        {
            var controller = new RacesController();
            var result = controller.Details(-1) as ViewResult;
            //Assert
            Assert.AreEqual("~/Views/Shared/Error.cshtml", result.ViewName);
        }

    }
}
