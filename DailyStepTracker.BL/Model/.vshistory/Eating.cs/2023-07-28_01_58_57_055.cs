using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using DailyStepTracker.BL.Controller;

namespace DailyStepTracker.BL.Model
{
    /// <summary>
    /// Прием пищи
    /// </summary>
    public class Eating
    {
        #region Свойства
        /// <summary>
        /// Время
        /// </summary>
        public DateTime DateTime { get; set; }
        /// <summary>
        /// Продукты
        /// </summary>
        public Dictionary<Food, int> Products { get; }
        /// <summary>
        /// Пользователь
        /// </summary>
        public User User { get; }
        #endregion
        //public Eating() 
        //{
        //    //DateTime = DateTime.Now;
        //    //Products = new Dictionary<Food, int>();
        //}
        [JsonConstructor]
        public Eating(User user) 
        {
            if (user == null)
            {
                throw new ArgumentNullException("Пользователь не может быть пустым!", nameof(user));
            }
            User = user;
            DateTime = DateTime.Now;
            Products = new Dictionary<Food, int>();
        }
        /// <summary>
        /// Добавление еды в словарь приема пищи
        /// </summary>
        /// <param name="food">Еда</param>
        /// <param name="weight">Количество</param>
        public void AddFood(Food food, int weight) 
        {
            Food product = (from item in Products.Keys    // Ищем продукт в словаре
                           where item.Name == food.Name
                            select item).FirstOrDefault();
            if (product == null)
            {
                Products.Add(food, weight);
            }
            else
            {
                Products[product] += weight;
            }
        }
    }
}
