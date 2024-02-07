using Fitness_Tracking_System.Data;
using Fitness_Tracking_System.Helpers;
using Fitness_Tracking_System.Interfaces;
using Fitness_Tracking_System.Models;

namespace Fitness_Tracking_System.Services;

public class ProgressService : IProgress
{
    private List<Progress> progresses;
    private string path = Constants.PROGRESSES_PATH;

    private WorkoutService workoutService;

    public ProgressService(WorkoutService workoutService)
    {
        var content = FileIO<Progress>.Read(path);
        this.progresses = content;

        this.workoutService = workoutService;
    }
    public Progress Create(int workoutId, Progress progress)
    {
        var existWorkout = workoutService.GetAll().FirstOrDefault(w => w.IsActive == true);
        if (existWorkout == null)
            throw new Exception($"This workout {existWorkout.Name} is done already");

        existWorkout.IsActive = false;
        progress.DoneWorkouts.Add(existWorkout);
        progresses.Add(progress);
        FileIO<Progress>.Write(path, progresses);
        return progress;
    }

    public bool Delete(int id)
    {
        var exist = progresses.FirstOrDefault(p => p.Id == id);
        if (exist == null)
            throw new Exception($"This progress with ID {id} is not found");

        exist.DoneWorkouts.ForEach(w => w.IsActive = true);
        progresses.Remove(exist);
        FileIO<Progress>.Write(path, progresses);
        return true;
    }

    public List<Progress> GetAll()
    {
        return progresses;
    }

    public Progress GetById(int id)
    {
        var exist = progresses.FirstOrDefault(p => p.Id == id);
        if (exist == null)
            throw new Exception($"This progress with ID {id} is not found");

        return exist;
    }

    public bool Update(int id, string Name)
    {
        var exist = progresses.FirstOrDefault(p => p.Id == id);
        if (exist == null)
            throw new Exception($"This progress with ID {id} is not found");

        exist.Name = Name;
        return true;
    }
}
