using DailyStepTracker.BL.Controller;
using DailyStepTracker.BL.Model;

class Program
{
    public static void Main()
    {
        Console.WriteLine("Введите имя:");
        string name = Console.ReadLine();
        UserController userController = new UserController(name);
        EatingController eatingController = new EatingController(userController.User);


        if (userController.IsNewUser)
        {
            #region Регистрация
            Console.WriteLine("Введите пол:");
            string gender = Console.ReadLine();

            Console.WriteLine("Введите дату рождения в формате DD.MM.YYYY");
            string birthdayString;
            DateTime birthday;
            do
                birthdayString = Console.ReadLine();
            while
                (!DateTime.TryParse(birthdayString, out birthday));

            Console.WriteLine("Введите вес:");
            int weight = int.Parse(Console.ReadLine());

            Console.WriteLine("Введите рост:");
            int height = int.Parse(Console.ReadLine());

            userController.MakeNewUser(gender, birthday, weight, height);

            Console.WriteLine($"{userController.User.Name}, поздравляем, вы успешно зарегестрировались!");
            #endregion 
        }
        else
        {
            Console.WriteLine($"{userController.User.Name}, добро пожаловать!");
        }
        Console.WriteLine("Выберите действие:");
        Console.WriteLine("A - ввести прием пищи");
        var command = Console.ReadKey(true);

        switch (command.Key)
        {
            case ConsoleKey.A:
                var food = EatingEnter();
                eatingController.Add(food.Food, food.Weight);
                break;
        }
        foreach (var product in eatingController.Eating.Products)
        {
            Console.WriteLine($"\t{product.Key} --- {product.Value}");
        }
    }
    #region Вспомогательные методы
    private static (Food Food, int Weight) EatingEnter()
    {
        Console.Write("Введите имя продукта: ");
        var food = Console.ReadLine();

        double calories = ParseDouble("Калорийность");
        double prots = ParseDouble("Белки");
        double fats = ParseDouble("Жиры");
        double carbs = ParseDouble("Углеводы");
        int weight = ParseInt("Вес");

        var product = new Food(food, calories, prots, fats, carbs);

        return (product, weight);
    }
    private static double ParseDouble(string name)
    {
        while (true)
        {
            Console.Write($"Введите {name}: ");
            if (double.TryParse(Console.ReadLine(), out double value))
            {
                return value;
            }
            else
            {
                Console.WriteLine($"Неверный формат ввода {name}");
            }
        }
    }
    private static int ParseInt(string name) // Через обобщения выходит криво
    {
        while (true)
        {
            Console.Write($"Введите {name}: ");
            if (int.TryParse(Console.ReadLine(), out int value))
            {
                return value;
            }
            else
            {
                Console.WriteLine($"Неверный формат ввода {name}");
            }
        }
    }
    #endregion
}