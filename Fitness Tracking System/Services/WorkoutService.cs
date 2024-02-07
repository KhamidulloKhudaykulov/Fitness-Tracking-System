using Fitness_Tracking_System.Data;
using Fitness_Tracking_System.Helpers;
using Fitness_Tracking_System.Interfaces;
using Fitness_Tracking_System.Models;

namespace Fitness_Tracking_System.Services;

public class WorkoutService : IWorkout
{
    private List<Workout> workouts;
    private string path = Constants.WORKOUTS_PATH;

    public WorkoutService()
    {
        workouts = FileIO<Workout>.Read(path);
    }
    public Workout Create(Workout workout)
    {
        var exist = workouts.FirstOrDefault(w => w.Name == workout.Name);
        if (exist != null)
            throw new Exception($"This workout with name {workout.Name} is already exist");

        if (workouts.Count == 0)
            workout.Id = 1;
        else
            workout.Id = workouts.Last().Id + 1;

        workouts.Add(workout);
        FileIO<Workout>.Write(path, workouts);
        return workout;
    }

    public bool Delete(int id)
    {
        var exist = workouts.FirstOrDefault(w => w.Id == id);
        if (exist == null)
            throw new Exception($"This workout with Id [{id}] is not found");

        workouts.Remove(exist);
        FileIO<Workout>.Write(path, workouts);
        return true;
    }

    public List<Workout> GetAll()
    {
        return workouts;
    }

    public Workout GetById(int id)
    {
        var exist = workouts.FirstOrDefault(w => w.Id == id);
        if (exist == null)
            throw new Exception($"This workout with Id [{id}] is not found");

        return exist;
    }

    public Workout Update(int id, Workout workout)
    {
        var exist = workouts.FirstOrDefault(w => w.Id == id);
        if (exist == null)
            throw new Exception($"This workout with Id [{id}] is not found");

        exist.IsActive = workout.IsActive;
        exist.Name = workout.Name;
        FileIO<Workout>.Write(path, workouts);
        return exist;
    }

    public int GetDone()
    {
        return workouts.Where(w => w.IsActive == false).ToList().Count();
    }

    public int GetNotDone()
    {
        return workouts.Where(w => w.IsActive == true).ToList().Count();
    }
}
