using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DailyStepTracker.BL.Model
{
    public class User
    {
        #region Свойства
        public string Name { get; }
        public Gender Gender { get; set; }
        public DateTime Birthday { get; set; }
        public int Weight { get; set; }
        public int Height { get; set; }
        #endregion
        public User(string name, Gender? gender, DateTime birthday, int weight, int height) : this(name)
        {
            #region Проверки входных параметров
            if (gender == null)
            {
                throw new ArgumentNullException("Пол не может быть пустым!", nameof(gender));
            }
            if (birthday < DateTime.Parse("01.01.1923") || birthday > DateTime.Now)
            {
                throw new ArgumentNullException("Дата рождения не может быть пустой!", nameof(birthday));
            }
            if (weight <= 0)
            {
                throw new ArgumentNullException("Вес не может быть меньше или равен нулю!", nameof(weight));
            }
            if (height <= 0)
            {
                throw new ArgumentNullException("Рост не может быть меньше или равен нулю!", nameof(height));
            }
            #endregion
            Gender = gender;
            Birthday = birthday;
            Weight = weight;
            Height = height;
        }
        [JsonConstructor]
        public User(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException("Имя не может быть пустым!", nameof(name));
            }
            Name = name;
        }
        public override string ToString()
        {
            return Name;
        }
    }
}