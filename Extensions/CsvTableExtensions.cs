using Loxifi.CsvTable.Services;
using System.Data;

namespace Loxifi.CsvTable.Extensions
{
    /// <summary>
    ///
    /// </summary>
    public static class CsvTableExtensions
    {
        /// <summary>
        /// Converts the CsvTable to a DataTable
        /// </summary>
        /// <typeparam name="TData">The underlying data type.</typeparam>
        /// <param name="csvTable">The CsvTable to convert</param>
        /// <returns>A data table version of the underlying csv table data</returns>
        public static DataTable ToDataTable<TData>(this CsvTable<TData> csvTable)
        {
            DataTable table = new()
            {
                TableName = csvTable.TableName
            };

            foreach (CsvColumn column in csvTable.Columns)
            {
                _ = table.Columns.Add(column.ColumnName);
            }

            foreach (CsvRow<TData> csvRow in csvTable.Rows)
            {
                DataRow dr = table.NewRow();

                dr.ItemArray = csvRow.Items.Cast<object>().ToArray();

                table.Rows.Add(dr);
            }

            return table;
        }

        /// <summary>
        /// Saves the CsvTable to disk
        /// </summary>
        /// <typeparam name="TData">The underlying data type of the table</typeparam>
        /// <param name="table">The table to save</param>
        /// <param name="path">The path to save the table</param>
        /// <param name="options">Optional options for saving the table</param>
        public static void Save<TData>(this CsvTable<TData> table, string path, CsvSaveOptions? options = null)
        {
            options ??= new CsvSaveOptions();

            using CompressedFileWriter compressedFileWriter = new(path, options.Compression);

            compressedFileWriter.Write(ToStringService.TableToString(table, options));
        }
    }
}