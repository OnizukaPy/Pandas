// classe di rappresentazione di una serie avente un indice e dei valori
namespace Pandas.Models;

public class Series<T> : IPandas<T> {

    // Limite massimo di elementi nella serie
    /// <summary>
    /// Maximum limit of elements in the series.
    /// </summary>
    /// <remarks>Default is int.MaxValue</remarks>
    private int _limit { get; set; } = int.MaxValue;

    // dizionario che contiene gli indici e i valori
    /// <summary>
    /// Dictionary that contains the indices and values.
    /// </summary>
    /// <remarks>Default is empty</remarks>
    private Dictionary<string, T> _data { set; get; } = null!;

    # region "Attributi"
    // nome della serie
    /// <summary>
    /// Return the name of the Series.
    /// </summary>
    /// <remarks>Default is Values</remarks>
    private string _name { get; set; } = null!;
    public string Name { 
        get { return _name; } 
        set { SetName(value); }
    }

    // proprietà della Trasposizione della serie
    /// <summary>
    /// Return the transposed Series.
    /// </summary>
    /// <remarks>Default is empty</remarks>
    public Series<T> Transpose { 
        get { return new Series<T>(GetValues(), GetIndices(), _name); }
    }

    // proprietà di indice per accedere alla serie
    /// <summary>
    /// The index (axis labels) of the Series.
    /// </summary>
    /// <remarks>Default is empty</remarks>
    public Index Index { 
        get { return new (GetIndices()); }
        set { SetIndex(value); }
        }

    // proprietà di valori contenuti nella serie
    /// <summary>
    /// Return Series as ndarray or ndarray-like depending on the dtype.
    /// /summary>
    /// <remarks>Default is empty</remarks>
    public Values<T> Values { 
        get { return new Values<T>(GetValues()); }
    }

    // proprietà di type della serie
    /// <summary>
    /// Return the dtype object of the underlying data.
    /// /summary>
    /// <remarks>Default is Generic</remarks>
    public string dtype { 
        get { return $"dtype('{Values.GetTypeOfData()}')"; } 
    }

    // grandezza della serie
    /// <summary>
    /// Return the number of elements in the underlying data.
    /// </summary>
    /// <returns></returns>
    public int size  {
        get {return Count(); }
    }

    // proprietà per verificare se la serie è vuota
    /// <summary>
    /// Indicator whether Series/DataFrame is empty.
    /// </summary>
    /// <returns></returns>
    public bool empty {
        get { return IsEmpty(); }
    }

    // proprietà che restituisce le proprietà della serie
    /// <summary>
    /// Return the properties of the Series.
    /// </summary>
    /// <returns></returns>
    Flags _flags { get; set; } = new();
    public string? Flags {
        get { return _flags?.ToString(); }
    }

    // proprietà per verificare se la serie ha dei valori NaN
    /// <summary>
    /// Check if the series has NaN values.
    /// </summary>
    /// <returns></returns>
    public bool HasNaNs {
        get { return this.HasNaNs(); }
    }

    // proprietà per verificare se la serie ha dei valori unici
    /// <summary>
    /// Return boolean if values in the object are unique.
    /// </summary>
    /// <returns></returns>
    public bool IsUnique {
        get { return this.IsUnique(); }
    }
    #endregion

    #region "Indexer"
    // per fare in modo che richiamando la serie con la seguente sintassi
    // s["a"] = 1; funzioni come un dizionario
    /// <summary>
    /// Indexer to access the series by index.
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public T this[string index] {
        get { return GetValue(index); }
        set { 
            // se l'indice esiste già, lo aggiorniamo con 
            // il nuovo valore
            // altrimenti lo aggiungiamo alla serie
            if (_data.ContainsKey(index)) {
                _data[index] = value;
            } else if (_data.Count < _limit) {
                Add(index, value); 
            } else {
                throw new InvalidOperationException($"Cannot add more than {_limit} elements to the series.");
            }
        }
    }
    #endregion

    #region "Costruttori"
    // Costruttore di default
    /// <summary>
    /// Default constructor.
    /// /summary>
    /// <remarks>Default is int.MaxValue</remarks>
    public Series() {
        Init();
    }

    private void Init() {
        _data = new();
        _name = "Values";
    }
    
    // costruttore che accetta un array di valori
    /// <summary>
    /// Constructor that accepts an array of values.
    /// </summary>
    /// <param name="values"></param>
    /// <exception cref="ArgumentNullException"></exception>
    /// <exception cref="KeyNotFoundException"></exception>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    /// <exception cref="OverflowException"></exception>
    public Series(List<T> values) {
        DefaultControlExceptions(values, text: "Values");
        Init();
        // aggiungiamo gli indici come stringhe
        // per ogni valore nell'array, aggiungiamo il valore e l'indice come stringa
        for (int i = 0; i < values.Count; i++) {
            Add(i.ToString(), values[i]);
        }
    }

    // Costruttore che accetta un dizionario di valori
    /// <summary>
    /// Constructor that accepts a dictionary of values.
    /// </summary>
    /// <param name="data"></param>
    /// <exception cref="ArgumentNullException"></exception>
    /// <exception cref="KeyNotFoundException"></exception>
    /// <exception cref="InvalidOperationException"></exception>
    public Series(Dictionary<string, T> data) {
        Init();
        if (data == null) {
            throw new ArgumentNullException("Data cannot be null.");
        }
        if (data.Count > _limit) {
            throw new InvalidOperationException("Length of data exceeds maximum limit.");
        }
        _data = data;
    }

    // costruttore che accetta un array di valori e un array di indici
    /// <summary>
    /// Constructor that accepts a list of values and a list of indices.
    /// </summary>
    /// <param name="values"></param>
    /// <param name="indices"></param>
    /// <param name="name"></param>
    /// <remarks>Default is Values</remarks>
    /// <exception cref="ArgumentNullException"></exception>
    /// <exception cref="KeyNotFoundException"></exception>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    /// <exception cref="OverflowException"></exception>
    /// <exception cref="ArgumentException"></exception>
    public Series(List<T> values, List<string> indices, string? name = null) {
        Init();
        if (values.Count != indices.Count) {
            throw new ArgumentException("Length of values and indices must be the same.");
        }
        DefaultControlExceptions(values, text:"Values");
        DefaultControlExceptions(indices, text:"Indices");

        for (int i = 0; i < values.Count; i++) {
            Add(indices[i], values[i]);
        }

        if (!String.IsNullOrEmpty(name)) {
            SetName(name);
        }
    }
    #endregion
    
    #region "Metodi"
    // metodo per settare il limite massimo di elementi nella serie
    /// <summary>
    /// Set the maximum limit of elements in the series.
    /// </summary>
    /// <param name="limit"></param>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    /// <exception cref="OverflowException"></exception>
    public void SetLimit(int limit) {
        if (limit < 0) {
            throw new ArgumentOutOfRangeException("Limit cannot be negative.");
        }
        if (limit > int.MaxValue) {
            throw new OverflowException("Limit exceeds maximum limit.");
        }
        _limit = limit;
    }
    // Metodo per aggiungere un valore a un indice
    /// <summary>
    /// Add a value to an index.
    /// If the index already exists, it will be updated with the new value.
    /// </summary>
    /// <param name="value"></param>
    /// <param name="index"></param>
    /// <exception cref="ArgumentException"></exception>
    public void Add(string index, T value) {
        if (_data.ContainsKey(index)) {
            throw new ArgumentException($"Index '{index}' already exists in the series.");
        }
        _data[index] = value;
    }
    // Metodo per ottenere il numero di elementi nella serie
    /// <summary>
    /// Get the number of elements in the series.
    /// </summary>
    /// <returns></returns>
    /// <exception cref="KeyNotFoundException"></exception>
    private int Count() {
        if (IsEmpty()) {
            throw new KeyNotFoundException("The series is empty.");
        }
        return _data.Count;
    }
    // Metodo per verificare se la serie è vuota
    /// <summary>
    /// Check if the series is empty.
    /// </summary>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    private bool IsEmpty() {
        if (_data == null) {
            throw new ArgumentNullException("The series is null.");
        }
        return _data.Count == 0;
    }
    // questo metodo serve per implementare l'interfaccia IEnumerable<T> e permette di iterare sulla serie
    /// <summary>
    /// Get an enumerator that iterates through the series.
    /// </summary>
    /// <returns></returns>
    /// <exception cref="KeyNotFoundException"></exception>
    public IEnumerator<T> GetEnumerator() {
        if (IsEmpty()) {
            throw new KeyNotFoundException("The series is empty.");
        }
        return _data.Values.GetEnumerator();
    }
    IEnumerator IEnumerable.GetEnumerator() {
        return GetEnumerator();
    }
    
        #region "Indice"
        // Metodo per ottenere l'indice associato a un valore
        /// <summary>
        /// Get the index associated with a value.
        /// This method will return the first index found for the given value.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <exception cref="KeyNotFoundException"></exception>
        public string GetIndex(T value) {
            if (IsEmpty()) {
                throw new KeyNotFoundException("The series is empty.");
            }
            // cerchiamo l'indice associato al valore
            foreach (var kvp in _data) {
                if (EqualityComparer<T>.Default.Equals(kvp.Value, value)) {
                    return kvp.Key;
                }
            }
            throw new KeyNotFoundException($"Value '{value}' not found in the series.");
        }
        // Metodo per ottenere tutti gli indici
        /// <summary>
        /// Get all indices in the series.
        /// </summary>
        /// <returns></returns>
        List<string> GetIndices() {
            return _data.Keys.ToList();
        }
        // metodo per resettare l'indice
        /// <summary>
        /// Reset the index of the series.
        /// </summary>
        /// <param name="indices"></param>
        public void ResetIndex() {
            if (IsEmpty()) {
                throw new KeyNotFoundException("The series is empty.");
            }
            // creiamo un nuovo dizionario con gli indici e i valori
            var newData = new Dictionary<string, T>();
            for (int i = 0; i < GetValues().Count; i++) {
                newData[i.ToString()] = _data.ElementAt(i).Value;
            }
            _data = newData;
        }
        // metodo per settare un l'indice
        /// <summary>
        /// Set the index of the series.
        /// </summary>
        /// <param name="newIndecies"></param>
        public void SetIndex(List<string> newIndecies) {
            // controlliamo se la serie è vuota
            if (IsEmpty()) {
                throw new KeyNotFoundException("The series is empty.");
            }
            // controlliamo le eccezioni di default per il nuovo indice
            DefaultControlExceptions(newIndecies, text:"New Index");
            // se il nuovo indice non ha la stessa lunghezza dei valori, lanciamo un'eccezione
            if (newIndecies.Count != GetIndices().Count) {
                throw new ArgumentException("Length of new indices must be the same as the number of values.");
            }
            // creiamo un nuovo dizionario con l'indice e i valori
            var newData = new Dictionary<string, T>();
            for (int i = 0; i < newIndecies.Count; i++) {
                // aggiungiamo il nuovo indice e il valore
                newData[newIndecies[i]] = _data.ElementAt(i).Value;
            }
            _data = newData;
        }
        #endregion
        
        #region "Valori"
        // Metodo per ottenere il valore associato a un indice
        /// <summary>
        /// Get the value associated with an index.
        /// This method will throw an exception if the index is not found.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        /// <exception cref="KeyNotFoundException"></exception>
        private T GetValue(string index) {
            if (_data.ContainsKey(index)) {
                return _data[index];
            } else {
                throw new KeyNotFoundException($"Index '{index}' not found in the series.");
            }
        }
        // Metodo per ottenere tutti i valori
        /// <summary>
        /// Get all values in the series.
        /// </summary>
        /// <returns></returns>
        /// exception cref="KeyNotFoundException"></exception>
        List<T> GetValues() {
            return _data.Values.ToList();
        }
        #endregion
        
        #region "Nome della serie"
        // Metodo per settare il nome della serie
        /// <summary>
        /// Set the name of the series.
        /// </summary>
        /// <param name="name"></param>
        void SetName(string name) {
            if (string.IsNullOrEmpty(name)) {
                throw new ArgumentNullException("Name cannot be null or empty.");
            }
            _name = name;
        }
        // Metodo per dare il nome di default alla serie
        /// <summary>
        /// Set the default name of the series.
        /// </summary>
        public void SetDefaultName() {
            _name = "Values";
        }
        #endregion
    
    #endregion

    #region "Override"
    // metodo per stampare la serie in formato tabellare
    /// <summary>
    /// Override ToString to ensure it returns a non-nullable string.
    /// </summary>
    public override string ToString() {
        if (_data == null) {
            throw new ArgumentNullException("The series is null.");
        }
        // restituiamo una tabella con i valori e gli indici
        var sb = new StringBuilder();
        sb.AppendLine($"Index\t{Name}");
        foreach (var kvp in _data) {
            sb.AppendLine($"{kvp.Key}\t{kvp.Value}");
        }
        return sb.ToString();
    }
    #endregion

    #region "Controllo delle eccezioni di default"
    // metodo per controllare le eccezioni di default
    /// <summary>
    /// Default control exceptions for the series.
    /// /summary>
    /// <param name="list"></param>
    /// <exception cref="ArgumentNullException"></exception>
    /// <exception cref="KeyNotFoundException"></exception>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    /// <exception cref="OverflowException"></exception>
    private void DefaultControlExceptions(List<T> list, string text = "Values") {
        if (list == null) {
            throw new ArgumentNullException($"{text} cannot be null.");
        }
        if (list.Count == 0)  {
            throw new KeyNotFoundException($"{text} cannot be empty.");
        }
        if (list.Count < 0) {
            throw new ArgumentOutOfRangeException($"Length of {text} cannot be negative.");
        }
        if (list.Count > _limit) {
            throw new OverflowException($"Length of {text} exceeds maximum limit.");
        }
    }

    private void DefaultControlExceptions(List<string> indices, string text = "Indices") {
        if (indices == null) {
            throw new ArgumentNullException($"{text} cannot be null.");
        }
        if (indices.Count == 0) {
            throw new KeyNotFoundException($"{text} cannot be empty.");
        }
        if (indices.Count < 0) {
            throw new ArgumentOutOfRangeException($"Length of {text} cannot be negative.");
        }
        if (indices.Count > _limit) {
            throw new OverflowException($"Length of {text} exceeds maximum limit.");
        }
    }
    #endregion
}