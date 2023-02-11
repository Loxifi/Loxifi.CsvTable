namespace Loxifi.CsvTable
{
    /// <summary>
    /// A collection of options to use when saving data to the disk
    /// </summary>
    public class CsvSaveOptions
    {
        /// <summary>
        /// Serialize the headers to the first row of the file. Default true
        /// </summary>
        public bool IncludeHeaders { get; set; } = true;

        /// <summary>
        /// An optional character to use to quote items. defaults to "
        /// </summary>
        public char? QuoteCharacter { get; set; } = '"';

        /// <summary>
        /// Optionally inserts a tab before all string values
        /// Supposedly helps excel open the file without converting data types
        /// </summary>
        public bool ExcelTab { get; set; }

        /// <summary>
        /// Compression to use when writing CSV files. Default None.
        /// The output file will have the correct extension appended
        /// </summary>
        public FileStreamCompression Compression { get; set; } = FileStreamCompression.None;
    }
}