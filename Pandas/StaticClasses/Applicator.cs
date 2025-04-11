// classe per Function application, GroupBy & window
namespace Pandas.StaticClasses;

public static class Applicator {

    // metodo per applicare una funzione a una serie
    /// <summary>
    /// Applies a function to each element of the series and returns a new series with the results.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="series"></param>
    /// <param name="func"></param>
    /// <returns></returns>
    public static Series<T> Apply<T>(this Series<T> series, Func<T, T> func) {

        Controller.CheckEmpty(series);

        var result = series.Copy(true);
        for (int i = 0; i < result.size; i++) {
            result[i] = func(series[i]);
        }
        return result;
    }

    #region LINQ sulle serie
    // serie di metodi per replicare i metodi LINQ sulle serie
    // metodo per repliare First
    /// <summary>
    /// Returns the first element of a series that satisfies a condition.
    /// </summary>
    /// typeparam name="T"></typeparam>
    /// <param name="series"></param>
    /// <param name="predicate"></param>
    /// <exception cref="InvalidOperationException">Thrown when no element satisfies the condition.</exception>
    /// <returns></returns>
    public static T First<T>(this Series<T> series, Func<T, bool> predicate) {
        Controller.CheckEmpty(series);
        for (int i = 0; i < series.size; i++) {
            if (predicate(series[i])) {
                return series[i];
            }
        }
        throw new InvalidOperationException("No element satisfies the condition.");
    }

    // metodo per replicare FirstOrDefault
    /// <summary>
    /// Returns the first element of a series that satisfies a condition or the default value if no such element is found.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="series"></param>
    /// <param name="predicate"></param>
    /// <exception cref="InvalidOperationException">Thrown when no element satisfies the condition.</exception>
    /// <returns></returns>
    public static T? FirstOrDefault<T>(this Series<T> series, Func<T, bool> predicate) {
        Controller.CheckEmpty(series);
        for (int i = 0; i < series.size; i++) {
            if (predicate(series[i])) {
                return series[i];
            }
        }
        return default;
    }

    // metodo per replicare where
    /// <summary>
    /// Returns a new series containing elements that satisfy a condition.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="series"></param>
    /// <param name="predicate"></param>
    /// <exception cref="InvalidOperationException">Thrown when no element satisfies the condition.</exception>
    /// <returns></returns>
    public static Series<T> Where<T>(this Series<T> series, Func<T, bool> predicate) {
        Controller.CheckEmpty(series);
        var result = new Series<T>();

        series.Index.ForEach(index => {
            if (predicate(series[index])) {
                result[index] = series[index];
            }
        });
        result.Name = series.Name;
        if (result.size == 0) {
            throw new InvalidOperationException("No element satisfies the condition.");
        }
        return result;
    }

    

    #endregion
}