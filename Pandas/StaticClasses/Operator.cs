// classe dei Binary operator functions
namespace Pandas.StaticClasses;

public static class Operator {
    
    // metodo per sommare due serie
    /// <summary>
    /// Sum two series with the same type of values.
    /// If the two series are strings, the methos compares index and adds the values of the second series to the first one, 
    /// where the index is not the same.
    /// If the two series are numbers (not Int), the method compares the two serier and sum the values of the second series to the first one, 
    /// and adds the values of the second in the first one, where the index is not the same.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="series">First series.</param>
    /// <param name="other">Second series.</param>
    /// <param name="fill_value">Value to fill NaN values.</param>
    /// <remarks>Default is null.</remarks>
    /// <exception cref="Exception">If the two series are not the same type.</exception>
    /// <returns></returns>
    public static void AddOther<T>(this Series<T> series, Series<T> other, T? fill_value = default){

        Controller.CheckEmpty(series);
        Controller.CheckEmpty(other);
        // se le serie non sono numeriche
        if (!Controller.IsTypeNumber(series.Values.GetTypeOfData())) {
            throw new Exception("The two series are not numeric.");
        } 
        // se le serie non sono dello stesso tipo lanciamo un'eccezione
        CheckIfTypesAreEquals(series, other);

        // se le serie hanno lo stesso indice
        if (series.Index.Any(index => other.Index.Contains(index))) {
            // sommiamo i valori delle due serie
            for (int i = 0; i < series.size; i++) {
                // per far si che le due serie possano venire sommate, dato che sono di tipo T, dobbiamo fare un cast a dynamic
                // e sommarle. Se il valore della seconda serie non è Num.NaN lo sommiamo al valore della prima serie
                if (!Controller.IsNaN(series.Values[i])) {
                    dynamic? v2 = other.Values[i];
                    var index = series.Index[i];
                    if (Controller.IsNaN(series.Values[i])) {
                        if (v2 != null) {
                            series[index] = v2;
                        } else {
                            series[index] = default!;
                        }
                    } else {
                        // sommiamo i valori delle due serie
                        series[index] += v2;
                    }
                }
            } 

            // se fill_value è valorizzato e non è nullo, lo usiamo per riempire i NaN
            if (!EqualityComparer<T>.Default.Equals(fill_value, default) && fill_value != null) {
                series.FillNaN(fill_value);
            }

        }
    }

    private static void CheckIfTypesAreEquals<T>(Series<T> series, Series<T> other) {
        if (series.Values.GetTypeOfData() != other.Values.GetTypeOfData()) {
            throw new Exception("The two series are not the same type.");
        }
    }
}