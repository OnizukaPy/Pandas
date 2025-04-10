// classe statica per le operazioni matermatiche tra serie
namespace Pandas.StaticClasses;

public static class Math {
    
    // metodo si somma degli elementi di una serie
    /// <summary>
    /// Sum the elements of a series.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="series">Series to sum.</param>
    /// <exception cref="Exception">Type not supported for sum operation.</exception>
    /// <returns></returns>
    public static double Sum<T>(this Series<T> series) {

        Controller.CheckEmpty(series);
        series = RemoveNaNs(series);

        if (Controller.IsTypeNumber(series.Values.GetTypeOfData())) {
            double sum = 0;
            foreach (var item in series.Values) {
                sum += Convert.ToDouble(item);
            }
            return sum;
        } else {
            throw new Exception("Type not supported for sum operation.");
        }
    }

    // metodo per calcolare il prodotto degli elementi di una serie
    /// <summary>
    /// Multiply the elements of a series.
    /// /summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="series">Series to multiply.</param>
    /// <exception cref="Exception">Type not supported for product operation.</exception>
    /// <returns></returns>
    public static double Prod<T>(this Series<T> series) {

        Controller.CheckEmpty(series);
        series = RemoveNaNs(series);

        if (Controller.IsTypeNumber(series.Values.GetTypeOfData())) {
            double product = 1;
            foreach (var item in series.Values) {
                product *= Convert.ToDouble(item);
            }
            return product;
        } else {
            throw new Exception("Type not supported for product operation.");
        }
    }

    // metodo per calcolare la media degli elementi di una serie
    /// <summary>
    /// Calculate the mean of the elements of a series.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="series">Series to calculate the mean.</param>
    /// <exception cref="Exception">Type not supported for mean operation.</exception>
    /// <returns></returns>
    public static double Mean<T>(this Series<T> series) {

        Controller.CheckEmpty(series);
        series = RemoveNaNs(series);

        if (Controller.IsTypeNumber(series.Values.GetTypeOfData())) {
            if (series.size > 0)
                // calcoliamo la media degli elementi della serie e rendiamola di tipo T
                return series.Sum() / series.size;
            else
                // eccezione non si può dividere per zero
                throw new Exception("Cannot divide by zero."); 
        } else {
            throw new Exception("Type not supported for mean operation.");
        }
    }

    // metodo per calcolare la mediana di una serie
    /// <summary>
    /// Calculate the median of a series.
    /// /summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="series">Series to calculate the median.</param>
    /// <exception cref="Exception">Type not supported for median operation.</exception>
    /// <returns></returns>
    public static double Median<T>(this Series<T> series) {

        Controller.CheckEmpty(series);
        series = RemoveNaNs(series);

        if (Controller.IsTypeNumber(series.Values.GetTypeOfData())) {
            double median = 0;
            var sortedList = series.Values.OrderBy(x => x).ToList();
            int count = sortedList.Count;

            if (count % 2 == 0) {
                // se il numero di elementi è pari
                median = (Convert.ToDouble(sortedList[count / 2 - 1]) + Convert.ToDouble(sortedList[count / 2])) / 2;
            } else {
                // se il numero di elementi è dispari
                median = Convert.ToDouble(sortedList[count / 2]);
            }
            return median;
        } else {
            throw new Exception("Type not supported for median operation.");
        }
    }

    // metodo per calcolare la moda di una serie
    /// <summary>
    /// Calculate the mode of a series.
    /// /summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="series">Series to calculate the mode.</param>
    /// <exception cref="Exception">Type not supported for mode operation.</exception>
    /// <returns></returns>
    public static double Mode<T>(this Series<T> series) {

        Controller.CheckEmpty(series);
        series = RemoveNaNs(series);

        if (Controller.IsTypeNumber(series.Values.GetTypeOfData())) {
            var mode = series.Values.GroupBy(x => x)
                .OrderByDescending(g => g.Count())
                .First()
                .Key;
            return Convert.ToDouble(mode);
        } else {
            throw new Exception("Type not supported for mode operation.");
        }
    }

    // metodo per calcolare il valore massimo di una serie
    /// <summary>
    /// Calculate the maximum value of a series.
    /// /summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="series">Series to calculate the maximum value.</param>
    /// <exception cref="Exception">Type not supported for max operation.</exception>
    /// <returns></returns>
    public static double Max<T>(this Series<T> series) {

        Controller.CheckEmpty(series);
        series = RemoveNaNs(series);

        if (Controller.IsTypeNumber(series.Values.GetTypeOfData())) {
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
    /// <exception cref="Exception">Type not supported for min operation.</exception>
    /// <returns></returns>
    public static double Min<T>(this Series<T> series) {

        Controller.CheckEmpty(series);
        series = RemoveNaNs(series);

        if (Controller.IsTypeNumber(series.Values.GetTypeOfData())) {
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
    /// <exception cref="Exception">Type not supported for variance operation.</exception>
    /// <returns></returns>
    public static double Var<T>(this Series<T> series) {

        Controller.CheckEmpty(series);
        series = RemoveNaNs(series);

        if (series.size <= 1) {
            return double.NaN;
        }

        if (Controller.IsTypeNumber(series.Values.GetTypeOfData()) && series.size > 1) {
            double mean = series.Mean();
            double variance = series.Values.Sum(item =>
                        System.Math.Pow(Convert.ToDouble(item) - mean, 2)) / (series.size - 1
                        );
            return variance;
        } else {
            throw new Exception("Type not supported for variance operation.");
        }
    }    

    // metodo per calcolare la deviazione standard di una serie
    /// <summary>
    /// Calculate the standard deviation of a series.
    /// /summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="series">Series to calculate the standard deviation.</param>
    /// <exception cref="Exception">Type not supported for standard deviation operation.</exception>
    /// <returns></returns>
    public static double Std<T>(this Series<T> series) {

        Controller.CheckEmpty(series);
        series = RemoveNaNs(series);
        var variance = series.Var();
        if (Controller.IsNaN(variance)) {
            return double.NaN;
        }

        if (Controller.IsTypeNumber(series.Values.GetTypeOfData())) {
            double std = System.Math.Sqrt(series.Var());
            return std;
        } else {
            throw new Exception("Type not supported for standard deviation operation.");
        }
    }

    // metodo per restituire una serie senza NaN
    /// <summary>
    /// Remove NaN values from a series.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="series">Series to remove NaN values.</param>
    /// <returns></returns>
    public static Series<T> RemoveNaNs<T>(Series<T> series) {

        if (series.HasNaNs()) {
            series = series.RemoveNaN();
        }
        return series;
    }
}