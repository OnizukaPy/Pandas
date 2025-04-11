// See https://aka.ms/new-console-template for more information

using System.Collections.Frozen;

Console.WriteLine("proviamo a fare un po' di pandas in c#");

var s = new Pd.Series<double>(new List<double> {1, double.NaN, 3, 4, 5}, new List<string> { "a", "b", "c", "d", "e" });
Console.WriteLine(s.ToString());

Console.WriteLine(s.Index);
Console.WriteLine(s.Values);
Console.WriteLine($"La somma dei valori è: {s.Sum()}");
Console.WriteLine($"La media dei valori è: {s.Mean()}");
Console.WriteLine($"Il valore massimo è: {s.Max()}");
Console.WriteLine($"Il valore minimo è: {s.Min()}");
Console.WriteLine($"La varianza è: {s.Std()}");

Console.WriteLine($"Elementi: {s.Count()}, {s.size}");
Console.WriteLine($"Elementi indice: {s.Index.size}");
Console.WriteLine($"E' vuota? {s.empty}");
Console.WriteLine();

s.Add("f", 6);
Console.WriteLine(s.ToString());

Console.WriteLine();
s.ResetIndex();
Console.WriteLine(s.ToString());

Console.WriteLine();
s.SetIndex(new List<string> { "1", "2", "3", "4", "5", "6" });
Console.WriteLine(s.ToString());

Console.WriteLine();
Console.WriteLine(s["1"]);

Console.WriteLine();
s["20"] = 20;
s.Name = "numeri";
Console.WriteLine(s.ToString());

Console.WriteLine(s.dtype);


Console.WriteLine("Proviamo a sostituire i NaN con la media");
s.FillNaN((float)s.Mean());
Console.WriteLine(s.ToString());

Console.WriteLine("Proviamo con una serie di stringhe");
var s2 = new Pd.Series<string?>(new List<string?> { "a", "b", "c", "d", "e" }, new List<string> { "1", "2", "3", "4", "5" });
Console.WriteLine(s2.ToString());
s2["a"] = null; // Assigning null to a nullable string
Console.WriteLine(s2.ToString());
Console.WriteLine($"Ci sono nulli?: {s2.HasNaNs()}");
s2.FillNaN("x");
Console.WriteLine(s2.ToString());
s2.Name = "stringhe";
Console.WriteLine(s2.ToString());

// copiamo la serie
var s3 = s2.Copy(false);
s3["23"] = "23";
Console.WriteLine(s3.ToString());
Console.WriteLine($"Sono uguali?: {s2.Equals(s3, false)}");

// sommiamo le due serie
Console.WriteLine($"Indice di s2: {s2.Index}");
Console.WriteLine($"Indice di s3: {s3.Index}");

//! Rimanere su questo metodo fino a quando non è concluso.
var s4 = s.Copy(false);
Console.WriteLine(s.ToString());
s4["7"] = -double.NaN;
Console.WriteLine(s4.ToString());
s.AddSeries(s4/* , fill_NaN_With: 52 */);
Console.WriteLine(s.ToString());



Console.WriteLine($"{double.NaN} + {double.NaN} = {double.NaN + double.NaN}");

// creiamo due nuove serie con dei valori NaN
var s5 = new Pd.Series<double>(new List<double> { 1, double.NaN, 3, 4, 5 }, new List<string> { "a", "b", "c", "d", "e" });
var s5_2 = new Pd.Series<double>(new List<double> { 1, double.NaN, 3, 4, 5 }, new List<string> { "a", "b", "c", "d", "e" });
var s5_3 = new Pd.Series<double>(new List<double> { 1, double.NaN, 3, 4, 5 }, new List<string> { "a", "b", "c", "d", "e" });
var s5_4 = new Pd.Series<double>(new List<double> { 1, double.NaN, 3, 4, 5 }, new List<string> { "a", "b", "c", "d", "e" });
var s6 = new Pd.Series<double>(new List<double> { double.NaN, 3, 4, 5, double.NaN }, new List<string> { "a", "b", "c", "d", "e" });

var equals = s5.EqualsTo(s5_2, IsNaN: true);
Console.WriteLine($"s5 == s5_2:\n{equals.ToString()}");
Console.WriteLine($"s5:\n{s5.ToString()}");
Console.WriteLine($"s6:\n{s6.ToString()}");
s5.AddSeries(s6);
Console.WriteLine($"Somma:\n{s5}");
s5_2.SubtractSeries(s6);
Console.WriteLine($"Sottrazione:\n{s5_2}");
s5_3.MultiplySeries(s6);
Console.WriteLine($"Moltiplicazione:\n{s5_3}");
s5_4.DivideSeries(s6);
Console.WriteLine($"Divisione:\n{s5_4}");

var s7 = new Pd.Series<double>(new List<double> { double.NaN, 3, 4.36, 5.96, 2 }, new List<string> { "a", "b", "c", "d", "e" });
s7.RoundSeries();
Console.WriteLine($"Arrotondamento:\n{s7}");
Console.WriteLine($"Numero di NaN:\n{s7.CountNaN()}");

Console.WriteLine($"Numero di elementi unici:\n{s7.CountUnique(false)}");
Console.WriteLine($"Lista: {String.Join(", ", s7.ListUnique(true))}");
Console.WriteLine($"Media: {s7.Mean()}");
Console.WriteLine($"Mediana: {s7.Median()}");
Console.WriteLine($"Moda: {s7.Mode()}");
s7[0] = 32;
Console.WriteLine($"{s7[0]}/{s7["a"]}");
Console.WriteLine($"s7:\n{s7}");
var s8 = s7.Apply(x => x * 2);
Console.WriteLine($"Applicazione funzione:\n{s8}");
var s9 = s7.Where(x => x > 8);
Console.WriteLine($"Filtraggio:\n{s9}");