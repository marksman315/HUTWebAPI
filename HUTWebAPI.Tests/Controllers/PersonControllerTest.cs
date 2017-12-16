using System.Web.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HUTWebAPI.Controllers;

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
