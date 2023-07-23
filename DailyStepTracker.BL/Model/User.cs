using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DailyStepTracker.BL.Model
{
    public class User
    {
        #region Свойства
        public string Name { get; }
        public Gender Gender { get; }
        public DateTime Birthday { get; }
        public int Weight { get; }
        public int Height { get; }
        #endregion
        public User(string name, Gender gender, DateTime birthday, int weight, int height)
        {
            #region Проверки входных параметров
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException("Имя не может быть пустым!", nameof(name));
            }
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
            Name = name;
            Gender = gender;
            Birthday = birthday;
            Weight = weight;
            Height = height;
        }
        public User()
        {

        }
        public override string ToString()
        {
            return Name;
        }
    }
}
