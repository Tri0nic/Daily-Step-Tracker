using DailyStepTracker.BL.Model;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO.Enumeration;
using System.Linq;
using System.Net.Http.Json;
using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DailyStepTracker.BL.Controller
{
    public class EatingController : BaseController
    {
        private const string FoodFileName = "FoodsData.json";
        private const string EatingFileName = "EatingData.json";
        private const string ProductsFileName = "ProductsData.json";
        /// <summary>
        /// Пользователь
        /// </summary>
        private readonly User User;
        /// <summary>
        /// Список еды
        /// </summary>
        public List<Food> Foods { get; }
        /// <summary>
        /// Список приемов пищи
        /// </summary>
        public Eating Eating { get; }
        public EatingController(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("Пользователь не может быть пустым!", nameof(user));
            }
            User = user;
            Foods = GetFood();
            Eating = GetEating();
            Save();
        }
        /// <summary>
        /// Добавление еды в прием пищи
        /// </summary>
        /// <param name="food">Еда</param>
        /// <param name="weight">Количество</param>
        public void Add(Food food, int weight)
        {
            Food product = (from item in Foods   // Ищем продукт в словаре
                            where item.Name == food.Name
                            select item).FirstOrDefault();
            if (product == null)
            {
                Foods.Add(food);
                Eating.AddFood(food, weight);
                Save();
            }
            else
            {
                Eating.AddFood(product, weight);
                Save();
            }
        }
        public List<Food> GetFood()
        {
            return GetItems<List<Food>>(FoodFileName) ?? new List<Food>();
        }
        public Eating GetEating()
        {
            // Десериализации Eating и его свойства Products
            Dictionary<Food, int> productsDeserialized;
            Eating eating;
            try
            {
                using (var file = new StreamReader(ProductsFileName))
                {
                    string jsonData = file.ReadToEnd();

                    if (string.IsNullOrWhiteSpace(jsonData)) // Если файл окажется пустым
                    {
                        productsDeserialized = new Dictionary<Food, int>();
                    }
                    // Десериализация словаря с пользовательским классом с помощью List
                    else if (JsonConvert.DeserializeObject<List<KeyValuePair<Food, int>>>(jsonData).ToDictionary(kv => kv.Key, kv => kv.Value) is Dictionary<Food, int> foodDict)
                    {
                        productsDeserialized = foodDict;
                    }
                    else
                    {
                        productsDeserialized = new Dictionary<Food, int>();
                    }
                }
            }
            catch (System.IO.FileNotFoundException) // Если файла не существует
            {
                productsDeserialized = new Dictionary<Food, int>();
            }

            try
            {
                using (var file = new StreamReader(EatingFileName))
                {
                    string jsonData = file.ReadToEnd();

                    if (string.IsNullOrWhiteSpace(jsonData)) // Если файл окажется пустым
                    {
                        eating = new Eating(User);
                    }
                    
                    else if (System.Text.Json.JsonSerializer.Deserialize<Eating>(jsonData) is Eating element)
                    {
                        // Возможно здесь БЫЛА ошибка: постоянно получали уже записанный прием пищи,
                        // но возможно этот прием пищи был от другого пользователя
                        // Исправление?:
                        if (element.User == User)
                        {
                            eating = element;
                        }
                        else
                        {
                            eating = new Eating(User);
                        }
                    }
                    else
                    {
                        eating = new Eating(User);
                    }
                }
            }
            catch (System.IO.FileNotFoundException) // Если файла не существует
            {
                eating = new Eating(User);
            }
            eating.Products = productsDeserialized;
            return eating;
        }
        private void Save()
        {
            Save(FoodFileName, Foods);
            /// !!!Когда словарь содержит пользовательский класс, необходимо
            /// !!!самостоятельно обработать его сериализацию, поскольку стандартные JSON
            /// !!!сериализаторы не знают, как сериализовать пользовательские классы по умолчанию. 
            string stringEatingProducts = JsonConvert.SerializeObject(Eating.Products.ToList());
            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,
                StringEscapeHandling = StringEscapeHandling.Default // Отключаем Unicode-escape
            };
            string jsonString = JsonConvert.SerializeObject(stringEatingProducts, settings);
            jsonString = jsonString.Replace("\\", "");
            jsonString = jsonString.Trim('"');
            using (var file = new StreamWriter(ProductsFileName, false))
            {
                file.Write(jsonString);
            }
            
            Save(EatingFileName, Eating);
        }
    }
}