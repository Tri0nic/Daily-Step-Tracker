namespace DailyStepTracker.BL.Model
{
    public class Gender
    {
        public string Name { get; }
        public Gender(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException("Имя пола не может быть пустым", nameof(name));
            }
            else
            {
                Name = name;
            }
        }
    }
}