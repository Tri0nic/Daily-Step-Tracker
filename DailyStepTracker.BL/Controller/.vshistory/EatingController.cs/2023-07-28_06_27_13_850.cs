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

            //List<KeyValuePair<Food, double>> keyValuePairs = JsonConvert.DeserializeObject<List<KeyValuePair<Food, double>>>(jsonData);
            //
            //// Создаем новый словарь и заполняем его из списка KeyValuePair
            //Dictionary<Food, double> productsDictionary = new Dictionary<Food, double>();
            //foreach (var kvp in keyValuePairs)
            //{
            //    productsDictionary.Add(kvp.Key, kvp.Value);
            //}


            ////////////////
            using (var file = new StreamReader(ProductsFileName))
            {
                string jsonData = file.ReadToEnd();
            
                if (string.IsNullOrWhiteSpace(jsonData)) // Если файл окажется пустым
                {
                    productsDeserialized = new Dictionary<Food, int>();
                }
                else if (JsonConvert.DeserializeObject<List<KeyValuePair<Food, int>>>(jsonData).ToDictionary(kv => kv.Key, kv => kv.Value) is Dictionary<Food, int> foodDict)
                {
                    productsDeserialized = foodDict;
                }
                else
                {
                    productsDeserialized = new Dictionary<Food, int>();
                }
            }
        ///////////////
            Eating eating;// = GetItems<Eating>(EatingFileName) ?? new Eating(User);
        //eating.Products = productsDeserialized;
            try
            {
                // Если файл UsersData создан и содержит List<User>, то десериализуем его и возвращаем
                // Если нет, то возвращаем пустой список
                using (var file = new StreamReader(EatingFileName))
                {
                    string jsonData = file.ReadToEnd();

                    if (string.IsNullOrWhiteSpace(jsonData)) // Если файл окажется пустым
                    {
                        eating = new Eating(User);
                    }
                    else if (System.Text.Json.JsonSerializer.Deserialize<Eating>(jsonData) is Eating element)
                    {
                        eating = element;
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
            // TODO: Изменить здесь вместо save на запись write в файл, т.к. работает только когда 
            // [{"Key":{"Name":"Pasta","Fats":142.0,"Proteins":613.0,"Carbohydrates":767.0,"Calories":486.0},"Value":43}]
            //using (var sw = new StreamWriter(ProductsFileName, true))
            //{
            //    sw.WriteLine(stringEatingProducts);
            //}
            Save(ProductsFileName, stringEatingProducts);

            //JsonSerializerSettings settings = new JsonSerializerSettings
            //{
            //    Formatting = Formatting.Indented,
            //    StringEscapeHandling = StringEscapeHandling.Default // Отключаем Unicode-escape
            //};
            //
            //string jsonString = JsonConvert.SerializeObject(stringEatingProducts, settings);
            //jsonString = jsonString.Replace("\\", "");
            //jsonString = jsonString.Trim('"');
            //using (var file = new StreamWriter(ProductsFileName, false))
            //{
            //    file.Write(jsonString);
            //}
            








            Save(EatingFileName, Eating);
        }
    }
}
