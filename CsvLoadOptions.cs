using StringSplitOptions = Loxifi.Extensions.StringExtensions.StringSplitOptions;

namespace Loxifi.CsvTable
{
    /// <summary>
    /// Options to use when loading and parsing CSV data from a file
    /// </summary>
    /// <typeparam name="TData">
    /// The underlying object type to use for the cell data.
    /// Either string or object is recommended unless all cells
    /// are the same kind.
    /// </typeparam>
    public class CsvLoadOptions<TData>
    {
        /// <summary>
        /// The max file size that the reader will attempt to store in memory
        /// at once. Anything over this size will be read as an IEnumerable.
        /// This can cause slowdowns, but will use less memory
        /// Default 2_000_000_000
        /// </summary>
        public int MaxFileSize { get; set; } = 2_000_000_000;

        /// <summary>
        /// Character to use a line delimeter. Defaults to newline
        /// </summary>
        public char LineDelimeter { get; set; } = '\n';

        /// <summary>
        /// If true, the first row of the CSV file will be used
        /// to populate the table headers
        /// </summary>
        public bool HasHeaders { get; set; } = true;

        /// <summary>
        /// Options to use whebn splitting CSV rows
        /// </summary>
        public StringSplitOptions LineSplitOptions { get; set; } = new StringSplitOptions();

        /// <summary>
        /// If provided, this function will be called on every CSV cell
        /// before the result is placed into the item bag for the row
        /// </summary>
        public Func<string, TData>? CellTransform { get; private set; }
    }
}