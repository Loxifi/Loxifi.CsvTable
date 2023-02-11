using Loxifi.CsvTable.CsvAdapters;
using Loxifi.CsvTable.Services;
using System.Data;

namespace Loxifi.CsvTable.Extensions
{
    /// <summary>
    ///
    /// </summary>
    public static class DataTableExtensions
    {
        /// <summary>
        /// Saves the data table to a CSV file
        /// </summary>
        /// <param name="table">The data table to save</param>
        /// <param name="path">The path to save the data table to</param>
        /// <param name="options">Optional options to use when saving</param>
        public static void Save(this DataTable table, string path, CsvSaveOptions? options = null)
        {
            options ??= new CsvSaveOptions();

            using CompressedFileWriter compressedFileWriter = new(path, options.Compression);

            compressedFileWriter.Write(ToStringService.TableToString(new DataTableAdapter(table), options));
        }
    }
}