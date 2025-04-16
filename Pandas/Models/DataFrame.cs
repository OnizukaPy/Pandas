// classe di gestione del DataFrame
namespace Pandas.Models;

/// <summary>
/// Class for DataFrame. Dataframe is a collection of series of generic objects.
/// It is a two-dimensional, size-mutable, potentially heterogeneous tabular data structure with labeled axes (rows and columns).
/// To have heterogeneous data, the type of the DataFrame must be "object".
/// </summary>
/// <typeparam name="T"></typeparam>
public class DataFrame<T> : IPandas<T> {

    // Proprietà del dataFrame
    // proprietà di indice per accedere alla serie
    /// <summary>
    /// The index (axis labels) of the Series.
    /// </summary>
    /// <remarks>Default is empty</remarks>
    public Index Index { get; set; } = null!;

    // proprietà delle colonne del dataFrame
    /// <summary>
    /// The columns of the DataFrame.
    /// </summary>
    /// <remarks>Default is empty</remarks>
    /// <returns></returns>
    public List<string> Columns {
        get { return _dataFrame.Keys.ToList(); }
    }

    private Dictionary<string, Series<T>> _dataFrame { get; set; } = null!;


    // Init
    private void Init() {
        _dataFrame = new Dictionary<string, Series<T>>();
        Index = new Index(new List<string>());
    }

    #region Costruttori
    // Costruttore di default
    /// <summary>
    /// Default constructor for DataFrame.
    /// Initializes the DataFrame with an empty dictionary and an empty index.
    /// </summary>
    public DataFrame() {
        Init();
    }

    // Costruttore con parametri

    // costruttore solo la lista di colonne
    /// <summary>
    /// Constructor for DataFrame, with a list of columns.
    /// </summary>
    /// <param name="columns"></param>
    /// <exception cref="ArgumentNullException">Columns cannot be null</exception>
    public DataFrame(List<string> columns) {
        // Inizializziamo il DataFrame
        Init();
        // controlliamo che le colonne non siano nulle
        if (columns == null) {
            throw new ArgumentNullException(nameof(columns), "Columns cannot be null");
        }
        // aggiungiamo le colonne al DataFrame
        foreach (var column in columns) {
            CheckColumnValue(column);
            CheckColumAlreadyExists(column);
            var newSeries = new Series<T>(new List<T>());
            newSeries.Name = column;
            _dataFrame.Add(column, newSeries);
        }
    }

    // costruttore che ha come parametro una lista di serie
    /// <summary>
    /// Constructor for DataFrame, with a list of Series.
    /// </summary>
    /// <param name="series"></param>
    /// <exception cref="ArgumentException">All series must have the same index</exception>
    /// <returns></returns>
    public DataFrame(List<Series<T>> series) {
        // Inizializziamo il DataFrame
        Init();

        // Controllo se la lista di serie è nulla
        Controller.CheckEmptyList<T>(series);

        // controllo se tutte le serie hanno lo stesso indice
        if (series.Count > 1) {
            series.ForEach(s => {
                if (!s.Index.Equals(series[0].Index)) {
                    throw new ArgumentException("All series must have the same index");
                }
            });
        }

        foreach (var s in series) {
            _dataFrame.Add(s.Name, s);
        }
        Index = series[0].Index;
        Index.Name = "Index";
    }

    // costruttore con una lista di liste di dati e una lista di colonne
    /// <summary>
    /// Constructor for DataFrame, with a list of lists of data and a list of columns.
    /// </summary>
    /// <param name="data"></param>
    /// <param name="columns"></param>
    /// <exception cref="ArgumentException"></exception>
    public DataFrame(List<List<T>> data, List<string> columns) {
        // Inizializziamo il DataFrame
        Init();
        // controlliamo che i dati abbiano lo stesso numero di righe
        if (data.Count == 0) {
            throw new ArgumentException("Data cannot be empty");
        }
        // controlliamo che le colonne non siano nulle
        if (columns == null) {
            throw new ArgumentNullException(nameof(columns), "Columns cannot be null");
        }
        var nRighe = data[0].Count;
        for (int i = 0; i < columns.Count; i++)  {
            
            var column = columns[i];
            // controlliamo che la colonna non sia nulla
            CheckColumnValue(column);
            // controlliamo che la colonna non esista già
            CheckColumAlreadyExists(column);
            // controlliamo che ogni lista abbia la stessa grandezza
            var series = new Pd.Series<T>(data[i].ToList());
            // controlliamo che la riga non sia nulla
            Controller.CheckSeriesEmpty(series);
            if (series.size != nRighe) {
                throw new ArgumentException("All rows must have the same number of columns");
            }

            // aggiungiamo le colonne al DataFrame
            AddColumn(column, series);
        }
    }
    

    // costruttore che accetta come parametro un dizionario di serie
    /// <summary>
    /// Constructor for DataFrame, with a dictionary of Series.
    /// </summary>
    /// <param name="dataFrame"></param>
    /// <exception cref="ArgumentNullException"></exception>
    /// <exception cref="ArgumentException"></exception>
    public DataFrame(Dictionary<string, Series<T>> dataFrame) {
        // Inizializziamo il DataFrame
        Init();
        // controlliamo che il dizionario non sia nullo
        if (dataFrame == null) {
            throw new ArgumentNullException(nameof(dataFrame), "DataFrame cannot be null");
        }
        // controlliamo che il dizionario non sia vuoto
        if (dataFrame.Count == 0) {
            throw new ArgumentException("DataFrame cannot be empty");
        }
        // aggiungiamo le colonne al DataFrame
        Index idx = new Index(new List<string>());
        foreach (var kvp in dataFrame) {
            // controlliamo che la colonna non sia nulla
            CheckColumnValue(kvp.Key);
            // controlliamo che la colonna non esista già
            CheckColumAlreadyExists(kvp.Key);
            // controlliamo che la serie non sia nulla
            Controller.CheckSeriesEmpty(kvp.Value);
            // salviamo l'indice della serie nell'indice del DataFrame e settiamolo
            if (idx.size == 0) {
                idx = kvp.Value.Index;
                idx.Name = "Index";
            } else if (!idx.Equals(kvp.Value.Index)) {
                throw new ArgumentException("All series must have the same index");
            }
            // aggiungiamo la serie al DataFrame
            _dataFrame.Add(kvp.Key, kvp.Value);
            // aggiungiamo il nome della colonna alla serie
            kvp.Value.Name = kvp.Key;
        }
    }

    #endregion

    #region Metodi Colonna
    // metodo per aggiungere una serie al DataFrame
    /// <summary>
    /// Add a Series to the DataFrame, by specifying the column name and the Series.
    /// </summary>
    /// <param name="series">Series to add.</param>
    /// <param name="column">Name of the column of the series</param>
    /// <returns></returns>
    public void AddColumn(string column, Series<T> series) {
        // controlliamo che la serie non sia nulla
        Controller.CheckSeriesEmpty(series);
        // controlliamo che la colonna non sia nulla
        CheckColumnValue(column);
        // controlliamo che la colonna non esista già
        CheckColumAlreadyExists(column);
        // controlliamo se l'index del DataFrame è vuoto (in questo modo posso aggiungere colonne al DataFrame vuoto)
        if (Index.size == 0) {
            Index = series.Index;
            Index.Name = "Index";
        }
        // controlliamo che la serie abbia lo stesso indice del DataFrame
        CompareSeriesIndex(series);
        // aggiungiamo la serie al DataFrame
        _dataFrame.Add(column, series);
    }

    // metodo per aggiungere una serie al DataFrame senza specificare il nome della colonna
    /// <summary>
    /// Add a Series to the DataFrame.
    /// If the column name is not specified, the Series name will be used.
    /// </summary>
    /// <param name="series"></param>
    public void AddColumn(Series<T> series) {
        AddColumn(series.Name, series);
    }
    #endregion

    #region Metodi Riga
    // metodo per aggiungere una riga al DataFrame
    /// <summary>
    /// Add a row to the DataFrame.
    /// </summary>
    /// <param name="row"></param>
    /// <param name="newIndex"></param>
    /// <remarks>Default is empty</remarks>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    /// <exception cref="ArgumentNullException"></exception>
    public void AddRow(Row row, string newIndex = "") {
        // Controlliamo che il dataframe abbia colonne
        if (Columns.Count == 0) {
            throw new ArgumentException("The DataFrame has no columns");
        }
        // controlliamo che la riga non sia nulla
        if (row.Count == 0) {
            throw new ArgumentNullException(nameof(row), "Row cannot be null");
        }
        // controlliamo che la riga abbia lo stesso numero di colonne del DataFrame
        if (row.Count != Columns.Count) {
            throw new ArgumentException("The row must have the same number of columns as the DataFrame");
        }
        // aggiungiamo l'ultima riga in fondo oppure all'indice indicato
        string index = 1.ToString();
        if (String.IsNullOrEmpty(newIndex)) {
            index = Index.size.ToString();
        } else if (Index.Contains(newIndex)) {
            throw new ArgumentException($"The index '{newIndex}' already exists");
        } else {
            index = newIndex;
        }
        
        for (int i = 0; i < row.Count; i++) {
            // alla serie della colonna aggiungiamo un elemento.
            // l'elemento andrà convertito al valore T
            _dataFrame[Columns[i]].Add(index, (T)row[i]);
        }
        //? Non so se questo serve a questo punto, dato he l'index è già stato creato
        Index.Add(index);
    }
    #endregion

    #region Indexers
    // indexer per accedere alla serie data una posizione
    /// <summary>
    /// Indexer for DataFrame.
    /// Allows access to the Series by column.
    /// </summary>
    /// <param name="column">Name of the Column to return.</param>
    /// <returns>Series at the specified Column.</returns>
    public Series<T> this[string column] {
        get { return GetSeriesByColumn(column); }
        // set { SetSeriesByColumn(column, value); }
    }

    // restituire la riga data una posizione
    /// <summary>
    /// Indexer for DataFrame.
    /// Allows access to the DataFrame by Index's position.
    /// </summary>
    /// <param name="index">Position of index.</param>
    /// <returns>Value at the specified index and column.</returns>
    /// <exception cref="KeyNotFoundException">Thrown when the index is not found.</exception>
    public Row this[int index] {
        get {
            // controlliamo che l'indice sia valido
            CheckIndexOutOfRange(index);
            // creiamo una nuova riga
            Row row = new Row(new List<object>() {
                //Index[index]
            });
            foreach (var kvp in _dataFrame) {
                    row.Add(kvp.Value[index] ?? default!);
            }
            return row;
        }
    }

    // indexer per accedere alla serie data una posizione [colonna, riga]
    /// <summary>
    /// Indexer for DataFrame with column and index.
    /// Allows access to the DataFrame by column and index.
    /// </summary>
    /// <param name="column"></param>
    /// <param name="index"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    public T this[string column, int index] {
        get {
            // controlliamo che l'indice sia valido
            CheckIndexOutOfRange(index);
            // controlliamo che la colonna sia valida
            CheckColumnNotFound(column);
            return _dataFrame[column][index];
        }
        set {
            // controlliamo che l'indice sia valido
            CheckIndexOutOfRange(index);
            // controlliamo che la colonna sia valida
            CheckColumnNotFound(column);
            // controlliamo che il valore non sia nullo
            if (value == null) {
                throw new ArgumentNullException(nameof(value), "Value cannot be null");
            }
            _dataFrame[column][index] = value;
        }
    }

    // indexer per restituire il DataFrame delle sole colonne specificate df[[col1, col2]]
    /// <summary>
    /// Indexer for DataFrame.
    /// Allows access to the DataFrame by a list of columns.
    /// </summary>
    /// <param name="columns"></param>
    /// <returns></returns>
    public DataFrame<T> this[List<string> columns] {
        get {
            // controlliamo che le colonne siano valide
            foreach (var column in columns) {
                CheckColumnNotFound(column);
            }
            // creiamo un nuovo DataFrame
            DataFrame<T> df = new DataFrame<T>();
            // aggiungiamo le colonne al DataFrame
            foreach (var column in columns) {
                df.AddColumn(column, _dataFrame[column]);
            }
            return df;
        }
    }
    
    #endregion

    public override string ToString() {
        // riaviamo le dimensioni massime di ogni colonna da usare per il padding
        int maxCw = SetMaxPad();

        // ostruiamo uno stringbuilder che costruisce delle righe di stringhe
        StringBuilder header = CreateHeader(maxCw);
        StringBuilder body = CreatBody(maxCw, Index.size);
        StringBuilder footer = new StringBuilder();
        StringBuilder sb = new StringBuilder();
        sb.Append(header);
        sb.Append(body);
        sb.Append(footer);
        return sb.ToString();
    }

    private StringBuilder CreatBody(int maxCw, int size) {
        var body = new StringBuilder();
        for (int i = 0; i < size; i++) {
            body.Append($"{Index[i]}".PadRight(maxCw));
            body.Append(" | ");
            foreach (var series in _dataFrame.Values) {
                body.Append($"{series[i]}".PadLeft(maxCw));
                body.Append(" | ");
            }
            body.AppendLine();
        }
        return body;
    }

    private StringBuilder CreateHeader(int maxCw) {
        var sb = new StringBuilder();
        // aggiungiamo l'intestazione
        sb.AppendLine("\n");
        sb.Append(Index.Name.PadRight(maxCw));
        sb.Append(" | ");
        foreach (var key in _dataFrame.Keys) {
            sb.Append(key.PadLeft(maxCw));
            sb.Append(" | ");
        }
        sb.AppendLine("\n");
        return sb;
    }

    private int SetMaxPad() {
        int maxCw = 0;
        int minCw = "index".Length;
        foreach (var key in _dataFrame.Keys) {
            maxCw = key.Length > maxCw ? key.Length : maxCw;
        }
        maxCw = maxCw > minCw ? maxCw : minCw;
        return maxCw;
    }

    private Series<T> GetSeriesByColumn(string column) {
        CheckColumnValue(column);
        CheckColumnNotFound(column);
        return _dataFrame[column];
    }

    private void CheckColumnNotFound(string column) {
        if (!_dataFrame.ContainsKey(column)) {
            throw new KeyNotFoundException($"The index {column} was not found.");
        }
    }

    private void CheckColumAlreadyExists(string column) {
        if (_dataFrame.ContainsKey(column)) {
            throw new ArgumentException($"The column {column} already exists");
        }
    }

    private void CheckColumnValue(string column) {
        // controlliamo che l'indice non sia nullo
        if (String.IsNullOrEmpty(column)) {
            throw new ArgumentNullException(nameof(column), "Index cannot be Null Or Empty");
        }
    }

    private void CompareSeriesIndex(Series<T> series) {
        if (!Index.Equals(series.Index)) {
            throw new ArgumentException("The series must have the same index as the DataFrame");
        }
    }

    private void CheckIndexOutOfRange(int index) {
        if (index < 0 || index >= Index.size) {
            throw new ArgumentOutOfRangeException(nameof(index), "Index out of range");
        }
    }

    public IEnumerator<T> GetEnumerator() {
        throw new NotImplementedException();
    }

    IEnumerator IEnumerable.GetEnumerator() {
        return GetEnumerator();
    }
}