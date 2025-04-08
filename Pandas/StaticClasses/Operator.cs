// classe dei Binary operator functions
namespace Pandas.StaticClasses;

public static class Operator {
    
    // metodo per sommare due serie
    /// <summary>
    /// Sum two series with the same type of values.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="series">First series.</param>
    /// <param name="other">Second series.</param>
    /// <returns></returns>
    public static void Add<T>(this Series<T> series, Series<T> other, T? fill_value = default) {
        // se le serie non sono dello stesso tipo lanciamo un'eccezione
        CheckIfTypesAreEquals(series, other);
        Controller.CheckEmpty(series);
        Controller.CheckEmpty(other);

        // se hanno gli stessi indici ritorniamo this
        if (series.Index.SequenceEqual(other.Index)) {
            return;
        } else {
            // scorriamo series negli indici con valore NaN e se con lo stesso indice di other 'è un valore non nullo lo stostituiamo
            if (series.HasNaNs()) {
                foreach (var index in series.Index) {
                    if (Controller.IsNaN(series[index]) && !Controller.IsNaN(other[index])) {
                        series[index] = other[index];
                    }
                }
            }
            // scorriamo i due indici e aggiungiamo in series gli indici di other che non sono in series
            // e il valore di other in series
            foreach (var index in other.Index) {
                if (!series.Index.Contains(index)) {
                    series.Index.Add(index);
                    series.Values.Add(other[index]);
                }
            }
            // se fill_value è valorizzato e non è nullo, lo usiamo per riempire i NaN
            if (!EqualityComparer<T>.Default.Equals(fill_value, default) && fill_value != null) {
                series.FillNaN(fill_value);
            }
        }
    }

    private static void CheckIfTypesAreEquals<T>(Series<T> series, Series<T> other) {
        if (series.dtype != other.dtype) {
            throw new Exception("The two series are not the same type.");
        }
    }
}