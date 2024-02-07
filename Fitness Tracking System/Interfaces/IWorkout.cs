using Fitness_Tracking_System.Models;

namespace Fitness_Tracking_System.Interfaces;

public interface IWorkout
{
    Workout Create(Workout workout);
    Workout Update(int id, Workout workout);
    bool Delete(int id);
    Workout GetById(int id);
    List<Workout> GetAll();
}
