// classe Base per index e values
namespace Pandas.Models.Base;

/// <summary>
/// Base class for Index and Values classes.
/// This class implements the common methods between series and dataframes
/// </summary>
/// <typeparam name="T"></typeparam>
public class PansdasList<T> : List<T> {

    // Type di dato generico
    /// <summary>
    /// Type of data in the list.
    /// </summary>
    /// <remarks>Default is "Generic".</remarks>
    private string _typeName = "Generic";

    // costruttore
    /// <summary>
    /// Constructor for PansdasList class.
    /// /summary>
    /// <param name="collection">Collection of elements.</param>
    /// <remarks>Default is "Generic".</remarks>
    public PansdasList(IEnumerable<T> collection) : base(collection) {
    }

    // metodo per ricavare il tipo di dato 
    /// <summary>
    /// Get the type of data in the list.
    /// </summary>
    /// <returns></returns>
    /// <remarks>type of T.</remarks>
    public Type GetTypeOfData() {
        return typeof(T);
    }

    // metodo per settare il nome del tipo
    /// <summary>
    /// Set the type name.
    /// /summary>
    /// <param name="typeName">Type name.</param>
    /// <remarks>Default is "Generic".</remarks>
    /// <returns></returns>
    public void SetTypeName(string typeName) {
        _typeName = typeName;
    }

    // metodo per ricavare il nome del tipo
    /// <summary>
    /// Get the type name.
    /// /summary>
    /// <returns></returns>
    /// <remarks>Default is "Generic".</remarks>
    public string GetTypeName() {
        return _typeName;
    }

    // override il metodo ToString per stampare gli indici
    /// <summary>
    /// Convert the list to a string.
    /// /summary>
    /// <returns></returns>
    public override string ToString() {
        return $"{_typeName}([{String.Join(", ", this)}], dtype='{GetTypeOfData().Name}')";
    }
}