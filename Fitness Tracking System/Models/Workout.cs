namespace Fitness_Tracking_System.Models;

public class Workout
{
    public int Id { get; set; }
    public string Name { get; set; }
    public bool IsActive { get; set; } = true;
}
