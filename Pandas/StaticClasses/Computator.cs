// classe statica di metodi per la computazione
namespace Pandas.StaticClasses;

// classe statica per la computazione
public static class Computator {

    // metodo per contare gli elementi NaN di una serie
    /// <summary>
    /// Return number of NaN observations in the Series.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="series">Series to count NaN observations.</param>
    /// <returns></returns>
    public static int CountNaN<T>(this Series<T> series) {
        Controller.CheckEmpty(series);
        if (series.HasNaNs()) {
            // se ci sono NaN nella serie, contiamo gli elementi non NaN
            return series.Values.Count(x => Controller.IsNaN(x));
        }
        return 0;
    }

    // metodo per contare gli elementi non NaN di una serie
    /// <summary>
    /// Return number of non-NA/null observations in the Series.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="series">Series to count non-NA/null observations.</param>
    /// <returns></returns>
    public static int Count<T>(this Series<T> series) {
        Controller.CheckEmpty(series);
        // se ci sono NaN nella serie, contiamo gli elementi non NaN
        return series.size - CountNaN(series);
     
    }

    // metodo per ontare gli elementi unici di una serie
    /// <summary>
    /// Return number of unique observations in the Series.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="series">Series to count unique observations.</param>
    /// <param name="checkNaN">If true, drop NaN observation</param>
    /// <returns></returns>
    public static int CountUnique<T>(this Series<T> series, bool checkNaN = false) {
        Controller.CheckEmpty(series);
        if (checkNaN) {
            // se ci sono NaN nella serie li rimuoviamo.
            series = Math.RemoveNaNs(series);
        }
        if (!series.IsUnique()) {
            // se non ci sono NaN nella serie, contiamo gli elementi unici
            return series.Values.Distinct().Count();
        }
        return series.size;
    }

    // metodo per restituire una lista con i valori unici
    /// <summary>
    /// Return unique values in the Series.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="series">Series to get unique values.</param>
    /// <param name="checkNaN">If true, drop NaN observation</param>
    /// <remarks>Default is false.</remarks>
    /// <returns></returns>
    public static List<T> ListUnique<T>(this Series<T> series, bool checkNaN = false) {
        Controller.CheckEmpty(series);
        if (checkNaN) {
            // se ci sono NaN nella serie li rimuoviamo.
            series = Math.RemoveNaNs(series);
        }
        if (!series.IsUnique()) {
            // se non ci sono NaN nella serie, contiamo gli elementi unici
            return series.Values.Distinct().ToList();
        }
        return series.Values;
    }

}

    