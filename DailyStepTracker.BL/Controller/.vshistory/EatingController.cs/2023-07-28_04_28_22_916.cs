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
        public EatingController(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("Пользователь не может быть пустым!", nameof(user));
            }
            User = user;
            Foods = GetFood();
            Eating = GetEating();
        }
        public List<Food> GetFood()
        {
            return GetItems<List<Food>>(FoodFileName) ?? new List<Food>();
        }
        public Eating GetEating()
        {
            // десериализации Eating и его свойства Products
            Dictionary<Food, int> productsDeserialized;
            //try
            //{
                using (var file = new StreamReader(ProductsFileName))
                {
                    string jsonData = file.ReadToEnd();
            
                    if (string.IsNullOrWhiteSpace(jsonData)) // Если файл окажется пустым
                    {
                        productsDeserialized = new Dictionary<Food, int>();
                    }


                List<KeyValuePair<Food, double>> keyValuePairs = JsonConvert.DeserializeObject<List<KeyValuePair<Food, double>>>(jsonData);

                // Создаем новый словарь и заполняем его из списка KeyValuePair
                Dictionary<Food, int> productsDictionary = new Dictionary<Food, int>();
                foreach (var kvp in keyValuePairs)
                {
                    productsDictionary.Add(kvp.Key, kvp.Value);
                }



                if (JsonConvert.DeserializeObject<List<KeyValuePair<Food, int>>>(jsonData).ToDictionary(kv => kv.Key, kv => kv.Value) is Dictionary<Food, int> foodDict)
                    {
                        productsDeserialized = foodDict;
                    }
                    else
                    {
                        productsDeserialized = new Dictionary<Food, int>();
                    }
                }
            //}
            //catch (System.IO.FileNotFoundException) // Если файла не существует
            //{
            //    return new T();
            //}
            Eating eating = GetItems<Eating>(EatingFileName) ?? new Eating(User);
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
            Save(ProductsFileName, stringEatingProducts);
            Save(EatingFileName, Eating);
        }


    }
}
