// classe che rappresenta una prioprietà
namespace Pandas.Models;

public class Flag : Dictionary<string, object> {
    
    // metotodo per stampare la proprietà
    /// <summary>
    /// Print the property.
    /// </summary>
    /// <returns></returns>
    public override string ToString() {
        string result = "";
        foreach (var item in this) {
            result += $"{item.Key}={item.Value}, ";
        }
        return result;
    }
}

public class Flags : List<Flag> {
    
    // metodo per stampare la proprietà
    /// <summary>
    /// Print the property.
    /// </summary>
    /// <returns></returns>
    public override string ToString() {
        string result = "";
        foreach (var item in this) {
            result += $"{item.ToString()}, ";
        }
        return $"Flags({result})";
    }
}