using Fitness_Tracking_System.Data;
using Fitness_Tracking_System.Helpers;
using Fitness_Tracking_System.Interfaces;
using Fitness_Tracking_System.Models;
using Newtonsoft.Json;

namespace Fitness_Tracking_System.Services;

public class NutritionService : INutrition
{
    private List<Nutrition> nutritions;
    private string path = Constants.NUTRITIONS_PATH;

    public NutritionService()
    {
        var content = File.ReadAllText(path);
        this.nutritions = new List<Nutrition>();
        if (content.Length != 0)
        {
            try
            {
                var json = JsonConvert.DeserializeObject<List<Nutrition>>(content);
                this.nutritions = json;
            }
            catch
            {
                var json = JsonConvert.DeserializeObject<Nutrition>(content);
                this.nutritions.Add(json);
            }
        }
    }
    public Nutrition Create(Nutrition nutrition)
    {
        var content = FileIO<Nutrition>.Read(path);
        var exist = content.FirstOrDefault(x => x.Name == nutrition.Name);
        if (exist != null)
            throw new Exception($"This Nutrition is already exists. Name = {nutrition.Name}");

        nutritions.Add(nutrition);
        FileIO<Nutrition>.Write(path, nutritions);
        return nutrition;
    }

    public bool Delete(int id)
    {
        var content = FileIO<Nutrition>.Read(path);
        var exist = content.FirstOrDefault(x => x.Id == id);
        if (exist == null)
            throw new Exception($"Nutrition with Id [{id}] is not found");

        nutritions.Remove(exist);
        FileIO<Nutrition>.Write(path, nutritions);
        return true;
    }

    public List<Nutrition> GetAll()
    {
        return nutritions;
    }

    public Nutrition GetById(int id)
    {
        var content = FileIO<Nutrition>.Read(path);
        var exist = content.FirstOrDefault(x => x.Id == id);
        if (exist == null)
            throw new Exception($"Nutrition with Id [{id}] is not found");

        return exist;
    }

    public Nutrition Update(int id, Nutrition nutrition)
    {
        var content = FileIO<Nutrition>.Read(path);
        var exist = content.FirstOrDefault(x => x.Id == id);
        if (exist == null)
            throw new Exception($"Nutrition with Id [{id}] is not found");

        exist.Name = nutrition.Name;
        FileIO<Nutrition>.Write(path, nutritions);

        return exist;
    }
}
