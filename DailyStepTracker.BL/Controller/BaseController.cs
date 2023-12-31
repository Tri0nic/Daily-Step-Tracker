﻿using DailyStepTracker.BL.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
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
        protected T GetItems<T>(string fileName) where T : new()
        {
            try
            {
                // Если файл создан и содержит T, то десериализуем его и возвращаем
                // Если нет, то возвращаем пустую T
                using (var file = new StreamReader(fileName))
                {
                    string jsonData = file.ReadToEnd();
        
                    if (string.IsNullOrWhiteSpace(jsonData)) // Если файл окажется пустым
                    {
                        return new T();
                    }
                    if (JsonSerializer.Deserialize<T>(jsonData) is T element)
                    {
                        return element;
                    }
                    else
                    {
                        return new T();
                    }
                }
            }
            catch (System.IO.FileNotFoundException) // Если файла не существует
            {
                return new T();
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