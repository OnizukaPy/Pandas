// classe per index
namespace Pandas.Models;

/// <summary>
/// Class for Index.
/// This class implements the common methods between series and dataframes
/// </summary>
public class Index : PansdasList<string> {

    // proprietà del nome dell'indice
    /// <summary>
    /// The name of the index.
    /// </summary>
    /// <remarks>Default is empty</remarks>
    public string Name { get; set; } = string.Empty;


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

    // metodo per stabilire quando un indice è uguale ad un altro
    /// <summary>
    /// Check if the index is equal to another index.
    /// </summary>
    /// <param name="other">Other index.</param>
    /// <returns>True if equal, false otherwise.</returns>
    /// <exception cref="ArgumentNullException">Thrown when other is null.</exception>
    /// <exception cref="InvalidOperationException">Thrown when the index is not equal.</exception>
    /// <remarks>Default is "Generic".</remarks>
    /// <returns></returns>
    public bool Equals(Index other) {
        // Controllo se l'indice è nullo
        if (other == null) {
            throw new ArgumentNullException(nameof(other), "Index cannot be null");
        }
        // Controllo se gli indici sono uguali
        if (Count != other.Count) {
            throw new InvalidOperationException("Index not equal");
        }
        bool equal = true;
        for (int i = 0; i < Count; i++) {
            if (this[i] != other[i]) {
                equal = false;
                break;
            }
        }
        return equal;
    }

    public static implicit operator Index(string v)
    {
        throw new NotImplementedException();
    }
}