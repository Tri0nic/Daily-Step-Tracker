using DailyStepTracker.BL.Controller;

class Program
{
    public static void Main()
    {
        Console.WriteLine("Введите имя:");
        string name = Console.ReadLine();
        UserController userController = new UserController(name);
        
        if (userController.IsNewUser)
        {
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
        }
        else
        {
            Console.WriteLine($"{userController.User.Name}, добро пожаловать!");
        }
    }
}