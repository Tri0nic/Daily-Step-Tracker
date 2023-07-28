using DailyStepTracker.BL.Model;
using System;
using System.Collections.Generic;
using System.IO.Enumeration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DailyStepTracker.BL.Controller
{
    public class EatingController : BaseController
    {
        private const string FoodFileName = "FoodsData.json";
        private const string EatingFileName = "EatingsData.json";
        /// <summary>
        /// Пользователь
        /// </summary>
        private readonly User User;
        /// <summary>
        /// Список еды
        /// </summary>
        public List<Food> Foods { get; }
        /// <summary>
        /// Список приемов пищи
        /// </summary>
        public List<Eating> Eatings { get; }

        public EatingController(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("Пользователь не может быть пустым!", nameof(user));
            }
            User = user;
            Foods = GetFood();
        }

        public List<Food> GetFood()
        {
            return GetItems<List<Food>>(FoodFileName);
        }
        private void Save()
        {
            Save(FoodFileName, Foods);
        }


    }
}
