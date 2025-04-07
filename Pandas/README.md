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
    * **[x] at:                       Access a single value for a row/column label pair.**  (Implementata come indexer)
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
    