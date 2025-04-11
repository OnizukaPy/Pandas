# Pandas

## obbiettivo

Replicare il comportamento della libreria Pandas in Python, creando una libreria in C#.

## Descrizione

La libreria Pandas è una libreria open source per il linguaggio di programmazione Python, utilizzata per l'analisi dei dati. La libreria fornisce strutture dati e funzioni per lavorare con dati etichettati e relazionali. È ampiamente utilizzata in data science, machine learning e analisi dei dati in generale.
La libreria Pandas è costruita su NumPy, un'altra libreria open source per Python, che fornisce supporto per array multidimensionali e funzioni matematiche. Pandas estende NumPy con strutture dati come DataFrame e Series, che consentono di lavorare con dati etichettati e relazionali in modo più semplice ed efficiente.

Gli oggetti principali di Pandas sono:
- **DataFrame**: una tabella bidimensionale con etichette per righe e colonne, simile a un foglio di calcolo o a una tabella SQL. I DataFrame possono contenere dati di diversi tipi (numeri, stringhe, date, ecc.) e supportano operazioni come filtraggio, aggregazione e unione.
- **Series**: una struttura dati unidimensionale simile a un array o a una lista, con etichette per gli elementi. Le Series possono contenere dati di diversi tipi e supportano operazioni come indicizzazione, filtraggio e aggregazione.

# Series

## Doumentazione

[Guida ufficiale](https://pandas.pydata.org/docs/reference/series.html)

Una Series è un vettore mono-dimensionale i cui elementi sono etichettati con un index.

In questo senso, la Series opera un po' come una lista (si possono accedere gli elementi in sequenza) e un po' come un dizionario (si può accedere ad un elemento tramite il suo indice, che opera come una chiave e non deve essere per forza numerico)

Esempio:

```python
import pandas as pd

s = pd.Series([1, 2, 3, 4], index=['a', 'b', 'c', 'd'])
print(s)
```

Output:
```
a    1
b    2
c    3
d    4
dtype: int64
```

## Funzionalità

pandas.Series

 - Attributes

    * [x] T:                        Return the transpose, which is by definition self.
    * [x] at:                       Access a single value for a row/column label pair.  (Implementata come indexer)
    * [x] dtype:                    Return the dtype object of the underlying data.
    * [x] empty:                    Indicator whether Series/DataFrame is empty.
    * [x] hasnans:                  Return True if there are any NaNs.
    * [x] index:                    The index (axis labels) of the Series.
    * [x] is_unique:                Return boolean if values in the object are unique.
    * [x] name:                     Return the name of the Series.
    * [x] size:                     Return the number of elements in the underlying data.
    * [x] values:                   Return Series as ndarray or ndarray-like depending on the dtype.

    * [ ] **(Da implementare)** flags:                    Get the properties associated with this pandas object.  
    * [ ] **(Da implementare)**is_monotonic_decreasing:  Return boolean if values in the object are monotonically decreasing.
    * [ ] **(Da implementare)**is_monotonic_increasing:  Return boolean if values in the object are monotonically increasing.

    * *[ ] array:                    The ExtensionArray of the data backing this Series or Index.*  (Da studiare)
    * *[ ] attrs:                    Dictionary of global attributes of this dataset.*  (Non implementata)
    * *[ ] nbytes:                   Return the number of bytes in the underlying data.*  (Non implementata)
    * *[ ] ndim:                     Number of dimensions of the underlying data, by definition 1.*  (Non implementata)
    * *[ ] shape:                    Return a tuple of the shape of the underlying data.*  (Non implementata)

    * [ ] iat:                      Access a single value for a row/column pair by integer position.
    * [ ] loc:                      Access a group of rows and columns by label(s) or a boolean array.
    * [ ] iloc:                     **(DEPRECATED) Purely integer-location based indexing for selection by position.**

 - Conversion

    * [x] copy:                     Make a copy of this object’s indices and data.
    * [x] to_dict:                 Convert the Series to a dictionary.
    * [x] tolist:                  Return the Series as a (possibly not deep) copy of ndarray.  (Deprecated)
    * [x] RemoveNaN:            Remove NaN values from the series.
    * [x] FillNaN:               Fill NA/NaN values using the specified method.
    * [ ] **(Da implementare)** astype:                   Cast a pandas object to a specified dtype.
    * [ ] **(Da implementare)** convert_dtypes:           Convert columns of a DataFrame to the best possible dtypes that support the data.
    * [ ] **(Da implementare)** convert_objects:          Convert argument to a Series of objects.  (Deprecated)

  - Controller

    * [x] Equals:                 Check if two series are equal. 
    * [x] IsUnique:               Return boolean if values in the object are unique.
    * [x] HasNaNs:                Check if a series has NaN values.
    * [x] CheckValueIsNaN:        Check if a value is NaN and retrun an exception if it is.
    * [x] IsNaN:                  Check if a value is NaN.
    * [x] IsNumber:               Check if a value is a number.
    * [x] CheckEmpty:             Check if a series is empty.
    * [x] CheckNull:              Check if a series is null.

  - Binary operator functions (Operator)

    * [x] AddSeries:              Add two series.
    * [x] SubSeries:              Subtract two series.
    * [x] MultiplySeries:         Multiply two series.
    * [x] DivideSeries:           Divide two series.
    * [x] RoundSeries:            Round a Series to the given number of decimals.
    * [x] EqualsTo:               Return Equal to of series and other, element-wise 
    * [x] GreaterThan:            Return Greater than of series and other, element-wise
    * [x] GreaterThanOrEqual:     Return Greater than or equal to of series and other, element-wise
    * [x] LessThan:               Return Less than of series and other, element-wise
    * [x] LessThanOrEqual:        Return Less than or equal to of series and other, element-wise

 - Matematihe functions (Math)

    * [x] Sum:                   Return the sum of the series.
    * [x] Mean:                  Return the mean of the series.
    * [x] Median:                Return the median of the series.
    * [x] Min:                   Return the minimum of the series.
    * [x] Max:                   Return the maximum of the series.
    * [x] Var:                   Return the variance of the series.
    * [x] Std:                   Return the standard deviation of the series.
    * [x] Mode:                  Return the mode of the series.
    * [x] Prod:                  Return the product of the series.

 - Computations funtions (Computatator)

    * [x] Count:                 Return the number of non-NaN values in the series.
    * [x] CountNaN:              Return the number of NaN values in the series.
    * [x] CountUnique:           Return the number of unique values in the series.
    * [x] UniqueToList:          Return the unique values in the series as a list.

 - Function application, GroupBy & window (Applicator)

    * [x] Apply:                 Apply a function to the series.
    * [x] First:                 Return the first n rows of the series.
    * [x] FirstOrDefault:        Return the first n rows of the series or a default value if the series is empty.
    * [x] Where:                 Return the rows of the series that match a condition.

 - Accessors (Accessor)

    * [ ] Str:                   Access the string methods of the series.
    * [ ] dt:                    Access the datetime methods of the series.
   
# Nuove Implementazioni

## MultiIndex

[Guida ufficiale](https://pandas.pydata.org/docs/reference/api/pandas.MultiIndex.html)