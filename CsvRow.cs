namespace Loxifi.CsvTable
{
    /// <summary>
    /// Represents a single row of a CsvTable and contains the items
    /// found in that row
    /// </summary>
    /// <typeparam name="TData">
    /// The underlying object type to use for the cell data.
    /// Either string or object is recommended unless all cells
    /// are the same kind.
    /// </typeparam>
    public class CsvRow<TData>
    {
        /// <summary>
        /// A list of string representations of the table items
        /// </summary>
        public TData[] Items { get; private set; }

        internal CsvRow(IList<CsvColumn> columns, CsvTable<TData> table) : this(columns, new TData[columns.Count], table)
        {
        }

        internal CsvRow(IList<CsvColumn> columns, TData[] items, CsvTable<TData> table)
        {
            this.Columns = columns;
            this.Items = items;
            this.Table = table;
        }

        /// <summary>
        /// A collection of columns denoting the row schema
        /// </summary>
        public IList<CsvColumn> Columns { get; }

        /// <summary>
        /// The parent table containing this row
        /// </summary>
        public CsvTable<TData> Table { get; }
    }
}