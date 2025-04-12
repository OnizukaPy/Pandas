// classe per poter fare le operazioni tra le serie e dentro la serie
namespace Pandas.StaticClasses;

public static class Conversion {

    // metoso AsType per convertire i valori di una generica (object) serie in un altro tipo
    /// <summary>
    /// Convert the values of the generi series (object) to another type.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="series">Series to convert.</param>
    /// <param name="type">Type to convert to.</param>
    /// <returns></returns>
    public static Series<T> AsType<T>(this Series<object> series) {
        Controller.CheckEmpty(series);
        // controlliamo se il tipo è valido
        if (typeof(T) != null) {
            var newSeries = new Series<T>(series.Values.Select(item => {
                // controlliamo se il valore è NaN
                if (Controller.IsNaN(item)) {
                    return default!;
                } else {
                    // convertiamo il valore in un altro tipo
                    return (T)Convert.ChangeType(item, typeof(T));
                }
            }).ToList(), series.Index.ToList()) {
                Name = series.Name,
            };
            return newSeries;
        } else {
            throw new ArgumentNullException(typeof(T).Name, "Type cannot be null");
        }
    }

    // metodo per rimuovere i NaN da una serie
    /// <summary>
    /// Remove NaN values from a list of objects.
    /// </summary>
    /// <param name="Series">List of objects.</param>
    /// <returns></returns>
    public static Series<T> RemoveNaN<T>(this Series<T> series) {

        Controller.CheckEmpty(series);

        // per ogni valore nella serie, se è NaN, lo rimuoviamo
        var newSeries = new Series<T>(
            series.Values.Where(item => !Controller.IsNaN(item)).ToList(), 
            series.Index.Where(index => !Controller.IsNaN(series[index])).ToList()
            ) {
                Name = series.Name,
        };
        return newSeries;
    }

    // metodo per riempire i punti NaN con un valore specificato
    /// <summary>
    /// Fill NaN values with a specified value.
    /// / </summary>
    /// <param name="value"></param>
    public static void FillNaN<T>(this Series<T> series, T value) {

        Controller.CheckEmpty(series);
        Controller.CheckValueIsNaN(value);

        // per ogni valore nella serie, se è NaN, lo sostituiamo con il valore specificato
        if (series.HasNaNs()) {
            series.Index.ForEach(index => {
                if (Controller.IsNaN(series[index])) {
                    series[index] = value;
                }
            });
        }
    }

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
        Controller.CheckEmpty(series);
        if (deep) {
            return series;
        } else {
            // facciamo una copia della serie
            var newSeries = new Series<T>(series.Values.ToList());
            return newSeries;
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
        Controller.CheckEmpty(series);
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
        Controller.CheckEmpty(series);
        // facciamo una copia dei valori della serie in un nuovo dizionario
        // e restituiamo il dizionario
        var newDict = new Dictionary<string, T>();
        for (int i = 0; i < series.size; i++) {
            newDict.Add(series.Index[i], series.Values[i]);
        }
        return newDict;
    }
}