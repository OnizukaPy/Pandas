// classe statica di controllo 
namespace Pandas.StaticClasses;

public static class Controller {

    // metodo per verifiare che due serie siano uguali
    /// <summary>
    /// Check if two series are equal. 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="deep">If false, check if the series are the same Values.</param>
    /// <remarks>Default is true.</remarks>
    /// <param name="series">Series to check.</param>
    /// <param name="other">Other series to check.</param>
    /// <returns></returns>
    public static bool Equals<T>(this Series<T> series, Series<T> other, bool deep = true) {
        CheckEmpty(series);
        CheckEmpty(other);
        // controlliamo che le due serie siano della stessa lunghezza
        if (series.size != other.size) {
            return false;
        }
        if (deep) {
            // controlliamo se le due serie sono uguali
            if (series.Values.SequenceEqual(other.Values) && series.Index.SequenceEqual(other.Index) &&
                series.Name == other.Name && series.dtype == other.dtype) {
                return true;
            } else {
                return false;
            }
        } else {
            // controlliamo se le due serie hanno gli stessi valori
            if (series.Values.SequenceEqual(other.Values) ) {
                return true;
            } else {
                return false;
            }
        }
    }

    // metodo per verificare se i valori di una serie sono uniti
    /// <summary>
    /// Return boolean if values in the object are unique.
    /// </summary>
    /// <param name="series">Series to check.</param>
    /// <returns></returns>
    public static bool IsUnique<T>(this Series<T> series) {
        CheckEmpty(series);
        // controlliamo se i valori della serie sono unici
        return series.Values.Distinct().Count() == series.Values.Count();
    }

    // metodo per valutare se una seria ha dei valori NaN
    /// <summary>
    /// Check if a series has NaN values.
    /// </summary>
    /// <param name="series">Series to check.</param>
    /// <returns></returns>
    public static bool HasNaNs<T>(this Series<T> series) {

        CheckEmpty(series);

        // per ogni valore nella serie, se è NaN, lo rimuoviamo
        bool hasNaN = false;
        hasNaN = series.Values.Any(item => IsNaN(item) ||     
                                         item is null || 
                                         item.Equals(String.Empty)
                                        ); 
        return hasNaN;
    }

    // metodo per verificare se un valore è NaN e sollevata un'eccezione
    /// <summary>
    /// Check if a value is NaN and throw an exception if it is.
    /// </summary>
    /// <param name="value">Value to check.</param>
    /// <exception cref="ArgumentException">Value must not be NaN.</exception>
    public static void CheckValueIsNaN<T>(T value) {
        if (IsNaN(value)) {
            throw new ArgumentException("Value must not be NaN.");
        }
    }

    // metodo per valutare se un oggeetto T è NaN
    /// <summary>
    /// Check if a value is NaN.
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static bool IsNaN<T>(T value) {
        if (value is double.NaN || value is float.NaN || value is null || value is "") {
            return true;
        }
        return false;
    }

    // metodo per verificare se T è un numero
    /// <summary>
    /// Check if T is a number.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="value">Value to check.</param>
    /// <returns></returns>
    public static bool IsNumber<T>(T value) {
        return value is int || value is double || value is float || value is decimal;
    }

    // metodo per verifiare se un Type è un numero
    /// <summary>
    /// Check if a Type is a number.
    /// </summary>
    /// <param name="type">Type to check.</param>
    /// <returns></returns>
    public static bool IsTypeNumber(Type type) {
        return type == typeof(int) || type == typeof(double) || type == typeof(float) || type == typeof(decimal);
    }

    // metodo per verificare se una serie è numerica sollevando una eccezione
    /// <summary>
    /// Check if a series is numeric and throw an exception if it is not.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="series">Series to check.</param>
    /// <exception cref="Exception">The two series are not numeric.</exception>
    
    public static void CheckIsTypeNumber<T>(Series<T> series) {
        if (!Controller.IsTypeNumber(series.Values.GetTypeOfData())) {
            throw new Exception("The two series are not numeric.");
        }
    }

    // metodo per verificare se due serie sono dello stesso tipo
    /// <summary>
    /// Check if two series are the same type.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="series">First series.</param>
    /// <param name="other">Second series.</param>
    /// <exception cref="Exception">The two series are not the same type.</exception>
    public static void CheckIfTypesAreEquals<T>(Series<T> series, Series<T> other) {
        if (series.Values.GetTypeOfData() != other.Values.GetTypeOfData()) {
            throw new Exception("The two series are not the same type.");
        }
    }

    // metodo per verificare se una lista di serie è vuota
    /// <summary>
    /// Check if a list of series is empty.
    /// </summary>
    /// <param name="series">List of series to check.</param>
    /// <returns></returns>
    public static void CheckEmptyList<T>(List<Series<T>> series) {
        series.ForEach(s => {
            // controlliamo che la serie non sia nulla
            CheckEmpty(s);
        });
    }

    // metodo per verificare se la serie è vuota
    /// <summary>
    /// Check if a series is empty.
    /// </summary>
    /// <param name="series">Series to check.</param>
    /// <exception cref="KeyNotFoundException">Series is empty.</exception>
    public static void CheckEmpty<T>(Series<T> series) {
        // controlliamo che la serie non sia nulla
        CheckNull(series);
        // controlliamo che la serie non sia vuota
        if (series.empty) {
            throw new KeyNotFoundException("The series is empty.");
        }
    }
    
    // metodo per verificare se la serie è nulla
    /// <summary>
    /// Check if a series is null.
    /// </summary>
    /// <param name="series">Series to check.</param>
    /// <exception cref="ArgumentNullException">Series is null.</exception>
    public static void CheckNull<T>(Series<T> series) {
        if (series is null or { Values: null } or { Index: null }) {
            throw new ArgumentNullException("The series is null.");
        }
    }

    // metodo per controllare le eccezioni di default
    /// <summary>
    /// Default control exceptions for the series.
    /// /summary>
    /// <param name="list"></param>
    /// <exception cref="ArgumentNullException"></exception>
    /// <exception cref="KeyNotFoundException"></exception>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    /// <exception cref="OverflowException"></exception>
    public static void DefaultControlExceptions<T>(List<T> list, int limit, string text = "Values") {
        if (list == null) {
            throw new ArgumentNullException($"{text} cannot be null.");
        }
        if (list.Count == 0)  {
            throw new KeyNotFoundException($"{text} cannot be empty.");
        }
        if (list.Count < 0) {
            throw new ArgumentOutOfRangeException($"Length of {text} cannot be negative.");
        }
        if (list.Count > limit) {
            throw new OverflowException($"Length of {text} exceeds maximum limit.");
        }
    }

    public static void DefaultControlExceptions(List<string> indices, int limit, string text = "Indices") {
        if (indices == null) {
            throw new ArgumentNullException($"{text} cannot be null.");
        }
        if (indices.Count == 0) {
            throw new KeyNotFoundException($"{text} cannot be empty.");
        }
        if (indices.Count < 0) {
            throw new ArgumentOutOfRangeException($"Length of {text} cannot be negative.");
        }
        if (indices.Count > limit) {
            throw new OverflowException($"Length of {text} exceeds maximum limit.");
        }
    }
}