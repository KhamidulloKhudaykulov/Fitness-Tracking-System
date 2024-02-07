using Newtonsoft.Json;

namespace Fitness_Tracking_System.Helpers;

public static class FileIO<T>
{
    public static List<T> Read(string path)
    {
        var returnedList = new List<T>();
        var content = File.ReadAllText(path);
        if (content.Length != 0)
        {
            try
            {
                var json = JsonConvert.DeserializeObject<List<T>>(content);
                returnedList = json;
            }
            catch
            {
                var json = JsonConvert.DeserializeObject<T>(content);
                returnedList.Add(json);
            }
        }
        else
        {
            return returnedList;
        }

        return returnedList;
    }

    public static void Write(string path, List<T> values)
    {
        var content = Read(path);
        content = values;
        var json = JsonConvert.SerializeObject(content, Formatting.Indented);
        File.WriteAllText(path, json);
    }
}
