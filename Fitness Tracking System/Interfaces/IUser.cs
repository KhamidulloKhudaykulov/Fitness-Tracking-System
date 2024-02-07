using Fitness_Tracking_System.Models;

namespace Fitness_Tracking_System.Interfaces;

public interface IUser
{
    User Create(User user);
    User Update(User user);
    bool Delete(User user);
    User GetInformation();
}
