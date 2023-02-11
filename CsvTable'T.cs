using Loxifi.CsvTable.Interfaces;

namespace Loxifi.CsvTable
{
    /// <summary>
    /// A table representing data loaded from a CSV
    /// </summary>
    /// <typeparam name="TData">
    /// The underlying object type to use for the cell data.
    /// Either string or object is recommended unless all cells
    /// are the same kind.
    /// </typeparam>
    public partial class CsvTable<TData> : ICsvAdapter
    {
        /// <summary>
        /// Instantiates a new CSV table
        /// </summary>
        /// <param name="tableName"></param>
        public CsvTable(string tableName)
        {
            TableName = tableName;
        }

        /// <summary>
        /// The name of the table, defaults to the file name
        /// without an extension
        /// </summary>
        public string TableName { get; set; }

        /// <summary>
        /// A collection of columns belonging to the table
        /// </summary>
        public IList<CsvColumn> Columns { get; set; } = new List<CsvColumn>();

        /// <summary>
        /// Constructs a new row with the existing table schema
        /// </summary>
        /// <returns></returns>
        public CsvRow<TData> NewRow() => new(Columns, this);

        /// <summary>
        /// Constructs a new row with the existing table schema
        /// </summary>
        /// <param name="items">The items to use to populate the row</param>
        /// <returns></returns>
        public CsvRow<TData> NewRow(TData[] items) => new(Columns, items, this);

        /// <summary>
        /// A collection of all rows found in this table
        /// </summary>
        public IList<CsvRow<TData>> Rows { get; private set; } = new List<CsvRow<TData>>();

        IEnumerable<string> ICsvAdapter.ColumnNames => Columns.Select(c => c.ColumnName);

        IEnumerable<IEnumerable<object?>> ICsvAdapter.Data => Rows.Select(r => r.Items.Cast<object?>());
    }
}