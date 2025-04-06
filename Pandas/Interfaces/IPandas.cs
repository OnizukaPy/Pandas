// questa interfaccia implementa i metodi comuni tra series e dataframes
// e permette di utilizzare i metodi di LINQ su entrambe le classi
namespace Pandas.Interfaces;

/// <summary>
/// Interface for Pandas Series and DataFrame.
/// </summary>
public interface IPandas<T> : IEnumerable<T> {

    /* // metodo per verificare è vuoto
    /// <summary>
    /// Check if the series is empty.
    /// </summary>
    /// <returns></returns>
    bool IsEmpty(); */

    // metodo per convertire in stringa 
    /// <summary>
    /// Convert the series to a string.
    /// </summary>
    /// <returns></returns>
    string ToString();
}
