using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HUTWebAPI;
using HUTWebAPI.Controllers;
using HUTModels;

namespace HUTWebAPI.Tests.Controllers
{
    [TestClass]
    public class PersonControllerTest
    {
        [TestMethod]
        public void Get()
        {
            // Arrange
            PersonController controller = new PersonController();

            // Act
            IHttpActionResult result = controller.Get(1);

            // Assert
            Assert.IsNotNull(result);
            //Assert.AreEqual(2, result.Count());
            //Assert.AreEqual("value1", result.ElementAt(0));
            //Assert.AreEqual("value2", result.ElementAt(1));
        }

        [TestMethod]
        public void GetAll()
        {
            // Arrange
            PersonController controller = new PersonController();

            // Act
            IHttpActionResult result = controller.GetAll();

            // Assert
            Assert.IsNotNull(result);
        }      
    }
}
