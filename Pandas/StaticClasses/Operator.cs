// classe dei Binary operator functions
using System.ComponentModel;
namespace Pandas.StaticClasses;

public static class Operator {

    enum Operations {
        [Description("Add")]
        Add,
        [Description("Subtract")]
        Subtract,
        [Description("Multiply")]
        Multiply,
        [Description("Divide")]
        Divide,
        [Description("Equals")]
        Eq,
        [Description("Less Than")]
        Lt,
        [Description("Less Than or Equals")]
        Le,
        [Description("Greater Than")]
        Gt,
        [Description("Greater Than or Equals")]
        Ge

    }
    
    // metodo per sommare due serie
    /// <summary>
    /// Addiction of two series with the same type of values.
    /// The two series must be double. The method compares the two serier and sum the values of the second series to the first one, 
    /// and adds the values of the second in the first one, where the index is not the same.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="series">First series.</param>
    /// <param name="other">Second series.</param>
    /// <param name="fill_NaN_With">Value to fill NaN values.</param>
    /// <remarks>Default is null.</remarks>
    /// <returns></returns>
    public static void AddSeries(this Series<double> series, Series<double> other, double fill_NaN_With = default) {

        Controller.CheckEmpty(series);
        Controller.CheckEmpty(other);

        ApplyOperations(series, other, Operations.Add, fill_NaN_With);
        series.Name = $"{series.Name} + {other.Name}";
    }

    // metodo per sottrarre due serie
    /// <summary>
    /// Subtraction of two series with the same type of values.
    /// The two series must be double. The method compares the two serier and ubtract the values of the second series to the first one, 
    /// and adds the values of the second in the first one, qith negativ sing, where the index is not the same.
    /// </summary>
    /// <param name="series"></param>
    /// <param name="other"></param>
    /// <param name="operation"></param>
    /// <param name="fill_NaN_With"></param>
    /// <remarks>Default is null.</remarks>
    /// <returns></returns>
    public static void SubtractSeries(this Series<double> series, Series<double> other, double fill_NaN_With = default){
        Controller.CheckEmpty(series);
        Controller.CheckEmpty(other);

        ApplyOperations(series, other, Operations.Subtract, fill_NaN_With);
        series.Name = $"{series.Name} - {other.Name}";
    }

    // metodo per moltiplicare due serie
    /// <summary>
    /// Multiplication of two series with the same type of values.
    /// The two series must be double. The method compares the two serier and multiply the values of the second series to the first one,
    /// and adds 0 if the values of the second is not in the first one, where the index is not the same.
    /// </summary>
    /// <param name="series"></param>
    /// <param name="other"></param>
    /// <param name="operation"></param>
    /// <param name="fill_NaN_With"></param>
    /// <remarks>Default is null.</remarks>
    /// <returns></returns>
    public static void MultiplySeries(this Series<double> series, Series<double> other, double fill_NaN_With = default) {
        Controller.CheckEmpty(series);
        Controller.CheckEmpty(other);

        ApplyOperations(series, other, Operations.Multiply, fill_NaN_With);
        series.Name = $"{series.Name} * {other.Name}";
    }

    // metodo per dividere due serie
    /// <summary>
    /// Division of two series with the same type of values.
    /// The two series must be double. The method compares the two serier and divide the values of the second series to the first one,
    /// and adds 0 if the values of the second is not in the first one, where the index is not the same.
    /// </summary>
    /// <param name="series"></param>
    /// <param name="other"></param>
    /// <param name="operation"></param>
    /// <param name="fill_NaN_With"></param>
    /// <remarks>Default is null.</remarks>
    /// <returns></returns>
    public static void DivideSeries(this Series<double> series, Series<double> other, double fill_NaN_With = default) {
        Controller.CheckEmpty(series);
        Controller.CheckEmpty(other);

        ApplyOperations(series, other, Operations.Divide, fill_NaN_With);
        series.Name = $"{series.Name} / {other.Name}";
    }

    // metodo per arrotondare ogni elemento di una serie 
    /// <summary>
    /// Round each element of a series.
    /// </summary>
    /// <param name="series">Series to round.</param>
    /// <param name="decimals">Number of decimals to round.</param>
    /// <returns></returns>
    /// <remarks>Default is 0.</remarks>
    public static void RoundSeries(this Series<double> series, int decimals = 0) {
        Controller.CheckEmpty(series);
        foreach (var index in series.Index) {
            series[index] = System.Math.Round(series[index], decimals);
        }
    }

    // metodo per creare una serie booleana che espremia in ogni posizione se due serie sono uguali
    /// <summary>
    /// Create a boolean series that expresses in each position if two series are equal.
    /// </summary>
    /// <param name="series">First series.</param>
    /// <param name="other">Second series.</param>
    /// <param name="operation">Operation to apply.</param>
    /// <param name="IsNaN">If true, the NaN values are considered equal.</param>
    /// <remarks>Default is false.</remarks>
    /// <returns></returns>
    public static Series<bool> EqualsTo(this Series<double> series, Series<double> other, bool IsNaN = false) {
        Controller.CheckEmpty(series);
        Controller.CheckEmpty(other);

        // controlliamo che le due serie siano dello stesso tipo
        Controller.CheckIfTypesAreEquals(series, other);
        Series<bool> result = ApplyComparisons(series, other, Operations.Eq, IsNaN);
        return result;
    }

    // metodo per creare una serie booleana che esprime in ogni posizione se la prima serie è minore della seconda
    /// <summary>
    /// Create a boolean series that expresses in each position if the first series is less than the second.
    /// </summary>
    /// <param name="series">First series.</param>
    /// <param name="other">Second series.</param>
    /// <param name="operation">Operation to apply.</param>
    /// <returns></returns>
    public static Series<bool> LessThan(this Series<double> series, Series<double> other) {
        Controller.CheckEmpty(series);
        Controller.CheckEmpty(other);

        // controlliamo che le due serie siano dello stesso tipo
        Controller.CheckIfTypesAreEquals(series, other);
        Series<bool> result = ApplyComparisons(series, other, Operations.Lt, false);
        return result;
    }

    // metodo per creare una serie booleana che esprime in ogni posizione se la prima serie è minore o uguale alla seconda
    /// <summary>
    /// Create a boolean series that expresses in each position if the first series is less than or equal to the second.
    /// In this case the NaN values are considered not equal.
    /// </summary>
    /// <param name="series">First series.</param>
    /// <param name="other">Second series.</param>
    /// <param name="operation">Operation to apply.</param>
    /// <returns></returns>
    public static Series<bool> LessThanOrEquals(this Series<double> series, Series<double> other) {
        Controller.CheckEmpty(series);
        Controller.CheckEmpty(other);

        // controlliamo che le due serie siano dello stesso tipo
        Controller.CheckIfTypesAreEquals(series, other);
        Series<bool> result = ApplyComparisons(series, other, Operations.Le, false);
        return result;
    } 

    // metodo per creare una serie booleana che esprime in ogni posizione se la prima serie è maggiore della seconda
    /// <summary>
    /// Create a boolean series that expresses in each position if the first series is greater than the second.
    /// </summary>
    /// <param name="series">First series.</param>
    /// <param name="other">Second series.</param>
    /// <param name="operation">Operation to apply.</param>
    /// <returns></returns>
    public static Series<bool> GreaterThan(this Series<double> series, Series<double> other) {
        Controller.CheckEmpty(series);
        Controller.CheckEmpty(other);

        // controlliamo che le due serie siano dello stesso tipo
        Controller.CheckIfTypesAreEquals(series, other);
        Series<bool> result = ApplyComparisons(series, other, Operations.Gt, false);
        return result;
    }

    // metodo per creare una serie booleana che esprime in ogni posizione se la prima serie è maggiore o uguale alla seconda
    /// <summary>
    /// Create a boolean series that expresses in each position if the first series is greater than or equal to the second.
    /// In this case the NaN values are considered not equal.
    /// </summary>
    /// <param name="series">First series.</param>
    /// <param name="other">Second series.</param>
    /// <param name="operation">Operation to apply.</param>
    /// <returns></returns>
    public static Series<bool> GreaterThanOrEquals(this Series<double> series, Series<double> other) {
        Controller.CheckEmpty(series);
        Controller.CheckEmpty(other);

        // controlliamo che le due serie siano dello stesso tipo
        Controller.CheckIfTypesAreEquals(series, other);
        Series<bool> result = ApplyComparisons(series, other, Operations.Ge, false);
        return result;
    }

    private static Series<bool> ApplyComparisons(Series<double> series, Series<double> other, Operations comparison, bool IsNaN) {
        // creiamo una nuova serie booleana
        var result = new Series<bool>();
        if (comparison == Operations.Eq){
            foreach (var index in series.Index) {
                // se sono entrambi NaN, sono uguali ma risutla false, quindi scriviamo true
                if (Controller.IsNaN(series[index]) && Controller.IsNaN(other[index]) && IsNaN) {
                    result.Add(index, true);
                } else if (other.Index.Contains(index)) {
                    result.Add(index, series[index] == other[index]);
                } else {
                    result.Add(index, false);
                }
            }
        }

        if (comparison == Operations.Lt){
            foreach (var index in series.Index) {
                if (other.Index.Contains(index)) {
                    result.Add(index, series[index] < other[index]);
                } else {
                    result.Add(index, false);
                }
            }
        }

        if (comparison == Operations.Le){
            foreach (var index in series.Index) {
                if (other.Index.Contains(index)) {
                    result.Add(index, series[index] <= other[index]);
                } else {
                    result.Add(index, false);
                }
            }
        }

        if (comparison == Operations.Gt){
            foreach (var index in series.Index) {
                if (other.Index.Contains(index)) {
                    result.Add(index, series[index] > other[index]);
                } else {
                    result.Add(index, false);
                }
            }
        }

        if (comparison == Operations.Ge){
            foreach (var index in series.Index) {
                if (other.Index.Contains(index)) {
                    result.Add(index, series[index] >= other[index]);
                } else {
                    result.Add(index, false);
                }
            }
        }

        return result;
    }

    private static void ApplyOperations(Series<double> series, Series<double> other, Operations operation, double fill_NaN_With = default) {
        other.Index.ForEach(index => {

            if (operation == Operations.Add) {
                // se l'indice non è presente nella prima serie, aggiungiamo l'elemento alla serie
                if (!series.Index.Contains(index)) {
                    series.Add(index, other[index]);
                } else {
                    // se l'indice è presente nella prima serie, sommiamo i valori
                    if (Controller.IsNaN(series[index]) || Controller.IsNaN(other[index])) {
                        series[index] = double.NaN;
                    } else {
                        series[index] += other[index];
                    }
                }
            }

            if (operation == Operations.Subtract) {
                // se l'indice non è contenuto nella prima serie, allora lo aggiungiamo on segno negativo
                if (!series.Index.Contains(index)) {
                    series.Add(index, -other[index]);
                } else {
                    // se l'indice è presente nella prima serie, sommiamo i valori
                    if (Controller.IsNaN(series[index]) || Controller.IsNaN(other[index])) {
                        series[index] = double.NaN;
                    } else {
                        series[index] = -other[index];
                    }
                }
            }

            if (operation == Operations.Multiply) {
                // se l'indice non è contenuto nella prima serie, allora aggiungiamo il valore 0
                if (!series.Index.Contains(index)) {
                    series.Add(index, 0.0);
                } else {
                    series[index] *= other[index];
                }
            }

            if (operation == Operations.Divide) {
                // se l'indice non è contenuto nella prima serie, allora aggiungiamo il valore 0
                if (!series.Index.Contains(index)) {
                    series.Add(index, 0.0);
                } else {
                    // se l'indice è presente nella prima serie, dividiamo i valori
                    // in questo caso quando il membro della prima serie vale 0 o NaN aggiungiamo 0
                    // se il membro della seconda serie vale 0 o NaN, mettiamo inf
                    if (Controller.IsNaN(series[index]) || series[index] == 0.0) {
                        series[index] = 0.0;
                    } else if (Controller.IsNaN(other[index]) || other[index] == 0.0) {
                        series[index] = double.PositiveInfinity;
                    } else {
                        series[index] /= other[index];
                    }
                }
            }

            // se fill_value è valorizzato e non è nullo, lo usiamo per riempire i NaN
            if (!EqualityComparer<double>.Default.Equals(fill_NaN_With, default)) {
                series.FillNaN(fill_NaN_With);
            }
        });
    }
}