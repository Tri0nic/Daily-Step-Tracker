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
        private const string EatingFileName = "EatingData.json";
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
        public Eating Eating { get; }
        /// <summary>
        /// Добавление еды в прием пищи
        /// </summary>
        /// <param name="food">Еда</param>
        /// <param name="weight">Количество</param>
        public void Add(Food food, int weight)
        {
            var product = Foods.SingleOrDefault(f => f.Name == food.Name);
            if (product == null)
            {
                Foods.Add(food);
                Eating.AddFood(food, weight);
                Save();
            }
            else
            {
                Eating.AddFood(product, weight);
                Save();
            }
        }
        public EatingController(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("Пользователь не может быть пустым!", nameof(user));
            }
            User = user;
            Foods = GetFood();
            Eating = GetEating();
        }
        public Eating GetEating()
        {
            return GetItems<Eating>(EatingFileName);
        }
        public List<Food> GetFood()
        {
            return GetItems<List<Food>>(FoodFileName);
        }
        private void Save()
        {
            Save(FoodFileName, Foods);
            Save(EatingFileName, Eating);
        }


    }
}
