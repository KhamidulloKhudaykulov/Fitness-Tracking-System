namespace Fitness_Tracking_System.Models;

public class User
{
    public long Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int Age { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string Phone { get; set; }
    public decimal Height { get; set; }
    public decimal Weight { get; set; }
    public List<Workout> Workouts { get; set; }
    public List<Nutrition> Nutritions { get; set; }
    public Progress Progress { get; set; }
}
