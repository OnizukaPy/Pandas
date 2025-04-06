// classe statica per le operazioni matermatiche tra serie
using Pandas.Models;

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
        // se la serie ha dei NaN eseguo il calcolo sulla serie senza NaN
        if (series.HasNaN()) {
            series = series.RemoveNaN();
        }
        if (series.Values.All(item => IsNumber(item))) {
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
        // se la serie ha dei NaN eseguo il calcolo sulla serie senza NaN
        if (series.HasNaN()) {
            series = series.RemoveNaN();
        }
        if (series.Values.All(item => IsNumber(item))) {
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
        // se la serie ha dei NaN eseguo il calcolo sulla serie senza NaN
        if (series.HasNaN()) {
            series = series.RemoveNaN();
        }
        if (series.Values.All(item => IsNumber(item))) {
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
        // se la serie ha dei NaN eseguo il calcolo sulla serie senza NaN
        if (series.HasNaN()) {
            series = series.RemoveNaN();
        }
        if (series.Values.All(item => IsNumber(item))) {
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
        // se la serie ha dei NaN eseguo il calcolo sulla serie senza NaN
        if (series.HasNaN()) {
            series = series.RemoveNaN();
        }
        if (series.Values.All(item => IsNumber(item)) && series.size > 1) {
            double mean = series.Mean();
            double variance = series.Values.Sum(item => 
                        System.Math.Pow(Convert.ToDouble(item) - mean, 2)) / (series.size - 1
                        );
            return variance;
        } else {
            throw new Exception("Type not supported for variance operation.");
        }
    }

    // metodo per verificare se T è un numero
    /// <summary>
    /// Check if T is a number.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="value">Value to check.</param>
    /// <returns></returns>
    static bool IsNumber<T>(T value) {
        return value is int || value is double || value is float || value is decimal;
    }

    // metodo per riempire i punti NaN con un valore specificato
    /// <summary>
    /// Fill NaN values with a specified value.
    /// / </summary>
    /// <param name="value"></param>
    public static void FillNaN<T>(this Series<T> series, T value) {
        if (IsNaN(value)) {
            throw new ArgumentException("Value must not be NaN.");
        }
        if (series.empty) {
            throw new KeyNotFoundException("The series is empty.");
        }
        // per ogni valore nella serie, se è NaN, lo sostituiamo con il valore specificato
        if (series.HasNaN()) {
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
    public static bool HasNaN<T>(this Series<T> series) {
        // per ogni valore nella serie, se è NaN, lo rimuoviamo
        return series.Values.Any(item => IsNaN(item) ||     
                                         item is null || 
                                         item.Equals(String.Empty)
                                        ); 
    }

    // metodo per rimuovere i NaN da una serie
    /// <summary>
    /// Remove NaN values from a list of objects.
    /// </summary>
    /// <param name="Series">List of objects.</param>
    /// <returns></returns>
    public static Series<T> RemoveNaN<T>(this Series<T> series) {
        if (series.empty) {
            throw new KeyNotFoundException("The series is empty.");
        }
        // per ogni valore nella serie, se è NaN, lo rimuoviamo
        return new Series<T>(
            series.Values.Where(item => !IsNaN(item)).ToList(), 
            series.Index.Where(index => !IsNaN(series[index])).ToList()
            ) {
                Name = series.Name,
        };
    }

    // metodo per valutare se un oggeetto T è NaN
    /// <summary>
    /// Check if a value is NaN.
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static bool IsNaN<T>(T value) {
        if (value is Num.NaN || value is null || value is "") {
            return true;
        }
        return false;
    }
}