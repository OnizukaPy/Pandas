// classe statica per le operazioni matermatiche tra serie
namespace Pandas.StaticClasses;

public static class Math {
    
    // metodo si somma degli elementi di una serie
    /// <summary>
    /// Sum the elements of a series.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="series">Series to sum.</param>
    /// <returns></returns>
    public static double Sum<T>(this Series<T> series) {

        Controller.CheckEmpty(series);
        series = RemoveNaNs(series);

        if (series.Values.All(item => Controller.IsNumber(item))) {
            double sum = 0;
            foreach (var item in series.Values) {
                sum += Convert.ToDouble(item);
            }
            return sum;
        } else {
            throw new Exception("Type not supported for sum operation.");
        }
    }

    // metodo per calcolare la media degli elementi di una serie
    /// <summary>
    /// Calculate the mean of the elements of a series.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="series">Series to calculate the mean.</param>
    /// <returns></returns>
    public static double Mean<T>(this Series<T> series) {

        Controller.CheckEmpty(series);
        series = RemoveNaNs(series);

        if (series.Values.All(item => Controller.IsNumber(item))) {
            if (series.size > 0)
                return series.Sum() / series.size;
            else
                // eccezione non si può dividere per zero
                throw new Exception("Cannot divide by zero."); 
        } else {
            throw new Exception("Type not supported for mean operation.");
        }
    }

    // metodo per calcolare il valore massimo di una serie
    /// <summary>
    /// Calculate the maximum value of a series.
    /// /summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="series">Series to calculate the maximum value.</param>
    /// <returns></returns>
    public static double Max<T>(this Series<T> series) {

        Controller.CheckEmpty(series);
        series = RemoveNaNs(series);

        if (series.Values.All(item => Controller.IsNumber(item))) {
            double max = series.Values.Max(item => Convert.ToDouble(item));
            return max;
        } else {
            throw new Exception("Type not supported for max operation.");
        }
    }

    // metodo per calcolare il valore minimo di una serie
    /// <summary>
    /// Calculate the maximum value of a series.
    /// /summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="series">Series to calculate the minimum value.</param>
    /// <returns></returns>
    public static double Min<T>(this Series<T> series) {

        Controller.CheckEmpty(series);
        series = RemoveNaNs(series);

        if (series.Values.All(item => Controller.IsNumber(item))) {
            double min = series.Values.Min(item => Convert.ToDouble(item));
            return min;
        } else {
            throw new Exception("Type not supported for max operation.");
        }
    }

    // metodo per calcolare la varianza di una serie
    /// <summary>
    /// Calculate the variance of a series.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="series">Series to calculate the variance.</param>
    /// <returns></returns>
    public static double Std<T>(this Series<T> series) {

        Controller.CheckEmpty(series);
        series = RemoveNaNs(series);

        if (series.Values.All(item => Controller.IsNumber(item)) && series.size > 1) {
            double mean = series.Mean();
            double variance = series.Values.Sum(item =>
                        System.Math.Pow(Convert.ToDouble(item) - mean, 2)) / (series.size - 1
                        );
            return variance;
        } else {
            throw new Exception("Type not supported for variance operation.");
        }
    }

    // metodo per restituire una serie senza NaN
    /// <summary>
    /// Remove NaN values from a series.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="series">Series to remove NaN values.</param>
    /// <returns></returns>
    private static Series<T> RemoveNaNs<T>(Series<T> series) {

        if (series.HasNaNs()) {
            series = series.RemoveNaN();
        }
        return series;
    }
}