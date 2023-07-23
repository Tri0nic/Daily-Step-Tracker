using DailyStepTracker.BL.Controller;

class Program
{
    public static void Main()
    {
        Console.WriteLine("Введите имя:");
        string name = Console.ReadLine();
        Console.WriteLine("Введите пол:");
        string gender = Console.ReadLine();
        Console.WriteLine("Введите дату рождения:");
        DateTime birthday = DateTime.Parse(Console.ReadLine());
        Console.WriteLine("Введите вес:");
        int weight = int.Parse(Console.ReadLine());
        Console.WriteLine("Введите рост:");
        int height = int.Parse(Console.ReadLine());
        UserController userController = new UserController(name, gender, birthday, weight, height);
        userController.Save();
    }
}