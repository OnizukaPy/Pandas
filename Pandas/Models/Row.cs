// classe di gestione della riga del DataFrame
namespace Pandas.Models;

public class Row : List<object> {

    
    // costruttore
    /// <summary>
    /// Constructor for Row class.
    /// </summary>
    /// <param name="collection">Collection of elements.</param>
    /// <remarks>Default is empty.</remarks>
    public Row(IEnumerable<object> collection) : base(collection) {
    }

    // metodo per restituire la collezione di elementi
    /// <summary>
    /// Get the collection of elements.
    /// </summary>
    /// <returns></returns>
    public List<object> GetElements() {
        return this;
    }

    // metodo per stampare la riga
    /// <summary>
    /// Print the row.
    /// </summary>
    /// <returns></returns>
    public override string ToString() {
        return $"[{String.Join(", ", this)}]";
    }
}