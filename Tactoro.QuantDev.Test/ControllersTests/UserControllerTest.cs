using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tactoro.QuantDev.Controllers;
using Tactoro.QuantDev.Models;

namespace Tactoro.QuantDev.Test.ControllersTests
{ 
    [TestClass]
    public class UserControllerTest
    {
        ControllersTestsSetup _setup;
        public UserControllerTest()
        {
            _setup = new ControllersTestsSetup();
        }

        [TestMethod]
        public async Task GetUserTest_ReturnsUsers()
        {
            //Arrange
            UserController controller = new UserController(_setup.mockDbContext);

            //Act
            var response = await controller.GetUser();

            //Assert
            Assert.IsNotNull(response.Value); //should be not null
        }

        [TestMethod]
        public async Task GetUserByIdTest_ReturnsUserNameVqs()
        {
            //Arrange
            UserController controller = new UserController(_setup.mockDbContext);

            //Act
            var response = await controller.GetUser(1);

            //Assert
            Assert.AreEqual<string>((response.Value as UserInfo).UserName.ToLower(), "vqs");
        }
    }
}
