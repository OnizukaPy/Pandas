// classe per Values
namespace Pandas.Models;

/// <summary>
/// Class for Values.
/// This class implements the common methods between series and dataframes
/// </summary>
/// <typeparam name="T"></typeparam>
public class Values<T> : PansdasList<T> {

    // costruttore
    /// <summary>
    /// Constructor for Values class.
    /// <summary>
    /// <param name="collection">Collection of elements.</param>
    /// <remarks>Default is "Generic".</remarks>
    /// <returns></returns>
    public Values(IEnumerable<T> collection) : base(collection) {
        SetTypeName("list");
    }
}