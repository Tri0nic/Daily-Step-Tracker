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

        /// <summary>
        /// Создание пользователя через список List<User> после десериализации
        /// </summary>
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
            // TODO: если пользователь не найден, то создаем нового пользователя,
            // т.е. запрашиваем ввод остальных параметров пользователя
            // т.е. нужно засунуть сюда второй конструктор: Создание пользователя через консоль
            if (User == null)
            {
                User = new User(userName);
                Users.Add(User);
                Save();
            }
        
        }


        /// <summary>
        /// Получение списка пользователей из файла
        /// </summary>
        /// <returns>Список пользователей</returns>
        private List<User> GetUsers()
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
                var a = JsonSerializer.Deserialize<List<User>>(jsonData);
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


        /// <summary>
        /// Создание пользователя через консоль
        /// </summary>
        /// <param name="name">Имя</param>
        /// <param name="genderString">Пол</param>
        /// <param name="birthday">День рождения</param>
        /// <param name="weight">Вес</param>
        /// <param name="height">Рост</param>
        //public UserController(string name, string genderString, DateTime birthday, int weight, int height)
        //{
        //    Gender gender = new Gender(genderString);
        //    User user = new User(name, gender, birthday, weight, height);
        //    if (user == null)
        //    {
        //        throw new ArgumentNullException("Пользователь не может быть пустым!", nameof(user));
        //    }
        //    User = user;
        //}


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
