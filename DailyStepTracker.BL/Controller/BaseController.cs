﻿using DailyStepTracker.BL.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DailyStepTracker.BL.Controller
{
    public abstract class BaseController
    {
        /// <summary>
        /// Получение списка пользователей из файла
        /// </summary>
        /// <returns>Список пользователей</returns>
        protected T GetUsers<T>(string fileName)
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
                        return default(T);
                    }
                    if (JsonSerializer.Deserialize<T>(jsonData) is T element)
                    {
                        return element;
                    }
                    else
                    {
                        return default(T);
                    }
                }
            }
            catch (System.IO.FileNotFoundException) // Если файла не существует
            {
                return default(T);
            }
        }

        /// <summary>
        /// Сохранение пользователей в JSON
        /// </summary>
        protected void Save(string fileName, object element)
        {
            // Для удобного чтения JSON
            JsonSerializerOptions options = new JsonSerializerOptions();
            options.WriteIndented = true;
            // Сериализуем в JSON
            string jsonString = JsonSerializer.Serialize(element, options);
            // Записываем JSON в файл 
            // false позволяет записывать данные в файл, и файл будет создан, если он не существует
            using (var file = new StreamWriter(fileName, false))
            {
                file.Write(jsonString);
            }
        }
    }
}
