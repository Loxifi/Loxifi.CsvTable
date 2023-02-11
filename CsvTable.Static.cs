using Loxifi.Extensions.StringExtensions;
using System.Data;

namespace Loxifi.CsvTable
{
    /// <summary>
    /// A structure representing the data contents of a CSV file
    /// that can be converted to a DataTable or saved back to disk
    /// after modification
    /// </summary>
    public partial class CsvTable
    {
        private const string NO_LINES_MESSAGE = "Can not read empty data to table without column headers to use as a schema";

        private static FileInfo LoadCsv(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentException($"'{nameof(path)}' cannot be null or whitespace.", nameof(path));
            }

            FileInfo targetfile = new(path);

            if (!targetfile.Exists)
            {
                throw new FileNotFoundException(path);
            }

            return targetfile;
        }

        private static CsvTable<TData> ToCsvTable<TData>(IEnumerable<string> FileLines, CsvLoadOptions<TData>? options = null)
        {
            options ??= new CsvLoadOptions<TData>();

            if (FileLines is null)
            {
                throw new ArgumentNullException(nameof(FileLines));
            }

            CsvTable<TData> toReturn = new(string.Empty);

            IEnumerator<string> LinesEnumerator = FileLines.GetEnumerator();

            bool hasNextLine = LinesEnumerator.MoveNext();

            if (options.HasHeaders)
            {
                foreach (string Header in LinesEnumerator.Current.Split(options.LineSplitOptions))
                {
                    toReturn.Columns.Add(new CsvColumn(Header));
                }

                hasNextLine = LinesEnumerator.MoveNext();
            }
            else
            {
                if (!hasNextLine)
                {
                    throw new ArgumentException(NO_LINES_MESSAGE, nameof(FileLines));
                }

                foreach (string _ in LinesEnumerator.Current.Split(options.LineSplitOptions))
                {
                    toReturn.Columns.Add(new CsvColumn(string.Empty));
                }
            }

            while (hasNextLine)
            {
                List<TData> items = new();

                foreach (string Column in LinesEnumerator.Current.Split(options.LineSplitOptions))
                {
                    TData? castItem = default;

                    if (Column is TData td)
                    {
                        castItem = td;
                    }
                    else if (options.CellTransform is not null)
                    {
                        castItem = options.CellTransform.Invoke(Column);
                    }
                    else
                    {
                        throw new InvalidCastException($"String is not implicitely castable to type {typeof(TData)} and no transformation function was provided");
                    }

                    items.Add(castItem);
                }

                CsvRow<TData> thisRow = toReturn.NewRow(items.ToArray());

                toReturn.Rows.Add(thisRow);

                hasNextLine = LinesEnumerator.MoveNext();
            }

            return toReturn;
        }

        /// <summary>
        /// Loads a CsvTable from disk
        /// </summary>
        /// <typeparam name="TData">
        /// The underlying type of the data.
        /// Unless all cells are the same data type, object or string is suggested
        /// </typeparam>
        /// <param name="path">The path to read the data from</param>
        /// <param name="csvTableOptions">Optional options to use when reading the table</param>
        /// <returns></returns>
        public static CsvTable<TData> Load<TData>(string path, CsvLoadOptions<TData>? csvTableOptions = null)
        {
            FileInfo targetfile = LoadCsv(path);

            csvTableOptions ??= new CsvLoadOptions<TData>();

            long fileLength = targetfile.Length;

            IEnumerable<char> toParse = fileLength < csvTableOptions.MaxFileSize ?
                                                     File.ReadAllText(targetfile.FullName) :
                                                     ReadFile(targetfile.FullName);

            CsvTable<TData> table = ToCsvTable<TData>(toParse.Split(csvTableOptions.LineSplitOptions).Select(l => l.Trim('\r')), csvTableOptions);

            table.TableName = Path.GetFileNameWithoutExtension(path);

            return table;
        }

        /// <summary>
        /// Reads an IEnumerable of CSV lines, each containing an IEnumerable of CSV cell values
        /// </summary>
        /// <param name="path">The path of the file to read</param>
        /// <param name="options"></param>
        /// <returns>An IEnumerable of CSV lines, each containing an IEnumerable of CSV cell values</returns>
        public static IEnumerable<IEnumerable<string>> ReadCsvCells(string path, CsvLoadOptions<string>? options = null)
        {
            options ??= new CsvLoadOptions<string>();

            return ReadFile(path).Split(options.LineSplitOptions).Select(l => l.Trim('\r').Split(options.LineSplitOptions));
        }

        private static IEnumerable<char> ReadFile(string filePath)
        {
            using StreamReader sr = new(filePath);
            int i;
            while ((i = sr.Read()) != -1)
            {
                yield return (char)i;
            }
        }
    }
}