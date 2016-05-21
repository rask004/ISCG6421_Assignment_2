namespace AddStrip.Calculations
{
    /// <summary>
    ///     Interface for the Calculation Class.
    /// </summary>
    internal interface ICalculationContract
    {
        /// <summary>
        ///     Add a calc line object.
        /// </summary>
        /// <param name="cl">calc line object</param>
        void Add(CalcLine cl);

        /// <summary>
        ///     Remove all calc line objects.
        /// </summary>
        void Clear();

        /// <summary>
        ///     update an associated display object (likely a form control).
        /// </summary>
        void Redisplay();

        /// <summary>
        ///     Find a calc line object at an index.
        /// </summary>
        /// <param name="n">an index integer.</param>
        /// <returns>The calc line object found at n.</returns>
        CalcLine Find(int n);

        /// <summary>
        ///     Replace a calc line object at an index.
        /// </summary>
        /// <param name="newCalc">new calc line object.</param>
        /// <param name="n">index integer.</param>
        void Replace(CalcLine newCalc, int n);

        /// <summary>
        ///     Insert a new calc line object at an index.
        /// </summary>
        /// <param name="newCalc">new calc line object.</param>
        /// <param name="n">index integer.</param>
        void Insert(CalcLine newCalc, int n);

        /// <summary>
        ///     Remove a calc line object at an index.
        /// </summary>
        /// <param name="n">inddex of the calc line object to remove.</param>
        void Delete(int n);

        /// <summary>
        ///     Save a set of calc line objects to a file.
        /// </summary>
        /// <param name="filename">filename to save to.</param>
        void SaveToFile(string filename);

        /// <summary>
        ///     Load a set of calc line objects to a file.
        /// </summary>
        /// <param name="filename">filename to load from.</param>
        void LoadFromFile(string filename);


    }
}
