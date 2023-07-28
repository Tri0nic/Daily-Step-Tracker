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
        /// <summary>
        /// Название JSON файла
        /// </summary>
        private const string FileName = "FoodsData.json";
        /// <summary>
        /// Пользователь
        /// </summary>
        private readonly User User;
        /// <summary>
        /// Список еды
        /// </summary>
        public List<Food> Foods { get; }

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
            return GetItems<List<Food>>(FileName);
        }
        private void Save()
        {
            Save(FileName, Foods);
        }


    }
}
