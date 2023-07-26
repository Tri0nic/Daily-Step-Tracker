using Microsoft.VisualStudio.TestTools.UnitTesting;
using DailyStepTracker.BL.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DailyStepTracker.BL.Controller.Tests
{
    [TestClass()]
    public class UserControllerTests
    {
        [TestMethod()]
        public void MakeNewUserTest()
        {
            //Assert
            string name = "Alisa";
            string gender = "w";
            DateTime birthday = DateTime.Parse("05.06.2001");
            int weight = 60;
            int height = 170;

            //Act
            UserController userController = new UserController(name);
            userController.MakeNewUser(gender, birthday, weight, height);

            //Assert
            Assert.AreEqual(name, userController.User.Name);
            Assert.AreEqual(gender, userController.User.Gender.Name);
            Assert.AreEqual(birthday, userController.User.Birthday);
            Assert.AreEqual(weight, userController.User.Weight);
            Assert.AreEqual(height, userController.User.Height);
        }

        [TestMethod()]
        public void UserControllerTest()
        {
            //Assert
            string userName = "Ivan";

            //Act
            UserController userController = new UserController(userName);

            //Assert
            Assert.AreEqual(userName, userController.User.Name);
        }
    }
}