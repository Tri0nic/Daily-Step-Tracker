using DailyStepTracker.BL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DailyStepTracker.BL.Controller
{
    public class UserController
    {
        
        public List<User> Users { get; }
        public User User { get; }
        public bool IsNewUser { get; }

        /// <summary>
        /// Создание пользователя через список List<User> после десериализации
        /// </summary>
        /// <param name="userName">Имя</param>
        public UserController(string userName)
        {
            
            if (string.IsNullOrWhiteSpace(userName))
            {
                throw new ArgumentNullException("Имя не может быть пустым!", nameof(userName));
            }
            Users = GetUsers(); // Десериализуем
            User? User = (from item in Users            // Ищем пользователя по имени в списке пользователей
                         where item.Name == userName
                         select item).SingleOrDefault();

            if (User == null) // Если имени пользователя нет в файле JSON, то добавляем его
            {
                this.User = new User(userName);
                Users.Add(this.User);
                IsNewUser = true;
                Save();
            }
            else
            {
                this.User = User;
            }
        }

        /// <summary>
        /// Заполнение свойств нового пользователя
        /// </summary>
        /// <param name="genderStr">Пол</param>
        /// <param name="birthday">Дата рождения</param>
        /// <param name="weight">Вес</param>
        /// <param name="height">Рост</param>
        public void MakeNewUser(string genderStr, DateTime birthday, int weight, int height)
        {
            // TODO: Проверка
            User.Gender = new Gender(genderStr);
            User.Birthday = birthday;
            User.Weight = weight;
            User.Height = height;
            Save();
        }

        /// <summary>
        /// Получение списка пользователей из файла
        /// </summary>
        /// <returns>Список пользователей</returns>
        private List<User> GetUsers()
        {
            try
            {
                // Если файл UsersData создан и содержит List<User>, то десериализуем его и возвращаем
                // Если нет, то возвращаем пустой список 
                using (var file = new StreamReader("UsersData.json"))
                {
                    string jsonData = file.ReadToEnd();

                    if (string.IsNullOrWhiteSpace(jsonData)) // Если файл окажется пустым
                    {
                        return new List<User>();
                    }
                    if (JsonSerializer.Deserialize<List<User>>(jsonData) is List<User> users)
                    {
                        return users;
                    }
                    else
                    {
                        return new List<User>();
                    }
                }
            }
            catch(System.IO.FileNotFoundException) // Если файла не существует
            {
                return new List<User>();
            }
        }

        /// <summary>
        /// Сохранение пользователей в JSON
        /// </summary>
        private void Save()
        {
            // Для удобного чтения JSON
            JsonSerializerOptions options = new JsonSerializerOptions();
            options.WriteIndented = true;
            // Сериализуем в JSON
            string jsonString = JsonSerializer.Serialize(Users, options);
            // Записываем JSON в файл 
            // false позволяет записывать данные в файл, и файл будет создан, если он не существует
            using (var file = new StreamWriter("UsersData.json", false))
            {
                file.Write(jsonString);
            }
        }
    }
}
