using DailyStepTracker.BL.Model;
using System.Text.Json;

namespace DailyStepTracker.BL.Controller
{
    public class UserController : BaseController
    {
        /// <summary>
        /// Название JSON файла
        /// </summary>
        private const string FileName = "UsersData.json";
        /// <summary>
        /// Список пользователей
        /// </summary>
        public List<User> Users { get; }
        /// <summary>
        /// Пользователь
        /// </summary>
        public User User { get; }
        /// <summary>
        /// Указатель новизны пользователя
        /// </summary>
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
            return GetItems<List<User>>(FileName);
        }
        /// <summary>
        /// Сохранение пользователей в JSON
        /// </summary>
        private void Save()
        {
            Save(FileName, Users);
        }
    }
}
