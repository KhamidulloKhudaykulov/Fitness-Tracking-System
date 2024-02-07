namespace Fitness_Tracking_System.Models;

public class Progress
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<Workout> DoneWorkouts { get; set; }
}
