using DailyStepTracker.BL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DailyStepTracker.BL.Controller
{
    public class UserController
    {
        public User User { get; }
        /// <summary>
        /// Создание пользователя через десериализацию JSON
        /// </summary>
        public UserController()
        {
            // Десериализация JSON файла
            using (var file = new StreamReader("UsersData.json"))
            {
                string jsonData = file.ReadToEnd();
                User = JsonSerializer.Deserialize<User>(jsonData);
            }
        }
        /// <summary>
        /// Создание пользователя
        /// </summary>
        /// <param name="name">Имя</param>
        /// <param name="genderString">Пол</param>
        /// <param name="birthday">День рождения</param>
        /// <param name="weight">Вес</param>
        /// <param name="height">Рост</param>
        public UserController(string name, string genderString, DateTime birthday, int weight, int height)
        {
            Gender gender = new Gender(genderString);
            User user = new User(name, gender, birthday, weight, height);
            if (user == null)
            {
                throw new ArgumentNullException("Пользователь не может быть пустым!", nameof(user));
            }
            User = user;
        }

        /// <summary>
        /// Сохранение пользователя в JSON
        /// </summary>
        public void Save()
        {
            // Для удобного чтения JSON
            JsonSerializerOptions options = new JsonSerializerOptions();
            options.WriteIndented = true;
            // Сериализуем в JSON
            string jsonString = JsonSerializer.Serialize(User, options);
            // Записываем JSON в файл 
            // false позволяет записывать данные в файл, и файл будет создан, если он не существует
            using (var file = new StreamWriter("UsersData.json", false))
            {
                file.Write(jsonString);
            }
        }
    }
}
