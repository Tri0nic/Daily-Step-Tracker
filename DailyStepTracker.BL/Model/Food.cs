﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyStepTracker.BL.Model
{
    internal class Food
    {
        #region Свойства
        /// <summary>
        /// Название продукта
        /// </summary>
        public string Name { get; }
        /// <summary>
        /// Жиры
        /// </summary>
        public double Fats {get; }
        /// <summary>
        /// Белки
        /// </summary>
        public double Proteins { get; }
        /// <summary>
        /// Углеводы
        /// </summary>
        public double Carbohydrates { get; }
        /// <summary>
        /// Калории в 100 граммах
        /// </summary>
        public double Calories { get; }
        #endregion
        public Food(string name, double fats, double proteins, double carbohydrates, double calories)
        {
            // TODO: ПРОВЕРКА
            Name = name;
            Fats = fats;
            Proteins = proteins;
            Carbohydrates = carbohydrates;
            Calories = calories;
        }
    }
}
