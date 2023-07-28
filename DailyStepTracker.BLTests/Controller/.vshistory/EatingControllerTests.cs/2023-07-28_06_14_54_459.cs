using Microsoft.VisualStudio.TestTools.UnitTesting;
using DailyStepTracker.BL.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DailyStepTracker.BL.Model;

namespace DailyStepTracker.BL.Controller.Tests
{
    [TestClass()]
    public class EatingControllerTests
    {
        [TestMethod()]
        public void AddTest()
        {
            //Assert
            string name = "Boriska";
            string foodName = "Pasta";
            Random rnd = new Random();
            UserController newUserController = new UserController(name);
            EatingController newEatingController = new EatingController(newUserController.User);
            Food food = new Food(foodName, rnd.Next(0, 1000), rnd.Next(1, 1000), rnd.Next(0, 1000), rnd.Next(0, 1000));

            //Act
            newEatingController.Add(food, rnd.Next(1, 100));

            //Assert
            Assert.AreEqual(food.Name, newEatingController.Eating.Products.First().Key.Name);
        }
    }
}