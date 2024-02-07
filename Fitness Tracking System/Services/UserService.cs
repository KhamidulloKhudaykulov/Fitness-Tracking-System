using Fitness_Tracking_System.Data;
using Fitness_Tracking_System.Interfaces;
using Fitness_Tracking_System.Models;
using Newtonsoft.Json;

namespace Fitness_Tracking_System.Services;

public class UserService : IUser
{
    private User user;
    private string path = Constants.USER_PATH;

    public UserService()
    {
        try
        {
            var content = File.ReadAllText(path);
            var json = JsonConvert.DeserializeObject<User>(content);
            this.user = json;
        }
        catch
        {
            this.user = new User();
        }
    }
    public User Create(User user)
    {
        this.user = user;
        var json = JsonConvert.SerializeObject(this.user, Formatting.Indented);
        File.WriteAllText(path, json);
        return this.user;
    }

    public bool Delete(User user)
    {
        this.user = new User();
        File.WriteAllText(path, "");
        return true;
    }

    public User GetInformation()
    {
        try
        {
            return this.user;
        }
        catch
        {
            return null;
        }
    }

    public User Update(User user)
    {
        this.user = user;
        return this.user;
    }
}
