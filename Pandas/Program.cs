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
s.FillNaN(s.Mean());
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
Console.WriteLine(s4.ToString());
s4["7"] = Single.NaN;
s4.AddOther(s);
Console.WriteLine(s4.ToString());



