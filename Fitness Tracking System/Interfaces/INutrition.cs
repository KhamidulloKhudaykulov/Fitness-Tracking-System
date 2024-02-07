using Fitness_Tracking_System.Models;

namespace Fitness_Tracking_System.Interfaces;

public interface INutrition
{
    Nutrition Create(Nutrition nutrition);
    Nutrition Update(int id, Nutrition nutrition);
    bool Delete(int id);
    Nutrition GetById(int id);
    List<Nutrition> GetAll();
}
