namespace Loxifi.CsvTable
{
    /// <summary>
    /// Represents the data applicable to one column of a CSV
    /// </summary>
    public class CsvColumn
    {
        /// <summary>
        /// Instantiates a new CSV column with the provided column name
        /// </summary>
        /// <param name="columnName"></param>
        public CsvColumn(string columnName)
        {
            ColumnName = columnName;
        }

        /// <summary>
        /// The name of the CSV column
        /// </summary>
        public string ColumnName { get; }
    }
}