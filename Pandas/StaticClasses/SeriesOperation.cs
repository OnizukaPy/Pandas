// classe per poter fare le operazioni tra le serie e dentro la serie
namespace Pandas.StaticClasses;

public static class SeriesOperation {

    // metodo per riempire i punti NaN con un valore specificato
    /// <summary>
    /// Fill NaN values with a specified value.
    /// / </summary>
    /// <param name="value"></param>
    public static void FillNaN<T>(this Series<T> series, T value) {

        CheckEmpty(series);
        CheckValueIsNaN(value);

        // per ogni valore nella serie, se è NaN, lo sostituiamo con il valore specificato
        if (series.HasNaNs()) {
            series.Index.ForEach(index => {
                if (IsNaN(series[index])) {
                    series[index] = value;
                }
            });
        }
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

    // metodo per rimuovere i NaN da una serie
    /// <summary>
    /// Remove NaN values from a list of objects.
    /// </summary>
    /// <param name="Series">List of objects.</param>
    /// <returns></returns>
    public static Series<T> RemoveNaN<T>(this Series<T> series) {

        CheckEmpty(series);

        // per ogni valore nella serie, se è NaN, lo rimuoviamo
        var newSeries = new Series<T>(
            series.Values.Where(item => !IsNaN(item)).ToList(), 
            series.Index.Where(index => !IsNaN(series[index])).ToList()
            ) {
                Name = series.Name,
        };
        return newSeries;
    }

    // metodo per verificare se un valore è NaN e sollevata un'eccezione
    /// <summary>
    /// Check if a value is NaN and throw an exception if it is.
    /// </summary>
    /// <param name="value">Value to check.</param>
    /// <exception cref="ArgumentException">Value must not be NaN.</exception>
    private static void CheckValueIsNaN<T>(T value) {
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
    private static bool IsNaN<T>(T value) {
        if (value is Num.NaN || value is null || value is "") {
            return true;
        }
        return false;
    }

    // metodo per verificare se i valori di una serie sono uniti
    /// <summary>
    /// Return boolean if values in the object are unique.
    /// </summary>
    /// <param name="series">Series to check.</param>
    /// <returns></returns>
    public static bool IsUnique<T>(this Series<T> series) {
        CheckEmpty(series);
        // per ogni valore nella serie, se è NaN, lo rimuoviamo
        return series.Values.Distinct().Count() == series.Values.Count();
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

    /* // metodo per impostare un cast di tipo personalizzato per una serie
    /// <summary>
    /// Cast a series to a specified type.
    /// </summary>
    /// <typeparam name="string">Type to cast to. (typeof(T))</typeparam>
    /// <param name="series">Series to cast.</param>
    /// <returns></returns>
    public static Series<T> AsAtype<T>(this Series<T> series, Type type) {
        CheckEmpty(series);
        // controlliamo che il tipo passato non sia già il tipo della serie
        if (series.Values.All(item => item is T)) {
            throw new ArgumentException($"The series is already of type {typeof(T).ToString()}.");
        } else {
            // facciamo un cast della serie al tipo specificato
            var newSeries = new Series<T>(series.Values
                .Where(item => item is not null)
                .Select(item => (T)Convert.ChangeType(item, type)!)
                .ToList(), 
                series.Index) {
                Name = series.Name,
            };
            return newSeries;
        }
    } */

    // metodo per creare una copia profonda della serie
    /// <summary>
    /// Create a deep copy of the series. 
    /// If deep is false, return the series with the same Values.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="deep">If true, create a deep copy of the series.</param>
    /// <remarks>Default is true.</remarks>
    /// <param name="series">Series to copy.</param>
    /// <returns></returns>
    public static Series<T> Copy<T>(this Series<T> series, bool deep = true) {
        CheckEmpty(series);
        if (deep) {
            return series;
        } else {
            // facciamo una copia della serie
            var newSeries = new Series<T>(series.Values.ToList());
            return newSeries;
        }
    }

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

    // metodo per convertire la serie in una lista
    /// <summary>
    /// Convert the series to a list.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="series">Series to convert.</param>
    /// <returns></returns>
    public static List<T> ToList<T>(this Series<T> series) {
        CheckEmpty(series);
        // facciamo una copia dei valori della serie in una nuova lista
        // e restituiamo la lista
        var newList = new List<T>(series.Values);
        return newList;
    }

    // metodo per convertire la serie in un dizionario
    /// <summary>
    /// Convert the series to a dictionary.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="series">Series to convert.</param>
    /// <returns></returns>
    public static Dictionary<string, T> ToDict<T>(this Series<T> series) {
        CheckEmpty(series);
        // facciamo una copia dei valori della serie in un nuovo dizionario
        // e restituiamo il dizionario
        var newDict = new Dictionary<string, T>();
        for (int i = 0; i < series.size; i++) {
            newDict.Add(series.Index[i], series.Values[i]);
        }
        return newDict;
    }
}