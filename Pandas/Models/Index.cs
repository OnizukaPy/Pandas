// classe per index
namespace Pandas.Models;

/// <summary>
/// Class for Index.
/// This class implements the common methods between series and dataframes
/// </summary>
public class Index : PansdasList<string> {

    // proprietà di grandezza dell'indice
    /// <summary>
    /// Get the size of the index.
    /// </summary>
    /// <returns></returns>
    public int size{
        get {return Count; }
    }
    
    // costruttore
    /// <summary>
    /// Constructor for Index class.
    /// /summary>
    /// <param name="collection">Collection of elements.</param>
    /// <remarks>Default is "Generic".</remarks>
    /// <returns></returns>
    public Index(IEnumerable<string> collection) : base(collection) {
        SetTypeName("Index");
    }
}