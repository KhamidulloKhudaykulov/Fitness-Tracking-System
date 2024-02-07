using Fitness_Tracking_System.Models;

namespace Fitness_Tracking_System.Interfaces;

public interface IProgress
{
    Progress Create(int wokoutId, Progress progress);
    bool Update(int id, string name);
    bool Delete(int id);
    Progress GetById(int id);
    List<Progress> GetAll();
}
