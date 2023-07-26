using DailyStepTracker.BL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DailyStepTracker.BL.Controller
{
    abstract class BaseController
    {
        /// <summary>
        /// Сохранение пользователей в JSON
        /// </summary>
        private void Save(string fileName, object element)
        {
            // Для удобного чтения JSON
            JsonSerializerOptions options = new JsonSerializerOptions();
            options.WriteIndented = true;
            // Сериализуем в JSON
            string jsonString = JsonSerializer.Serialize(element, options);
            // Записываем JSON в файл 
            // false позволяет записывать данные в файл, и файл будет создан, если он не существует
            using (var file = new StreamWriter("UsersData.json", false))
            {
                file.Write(jsonString);
            }
        }
    }
}
