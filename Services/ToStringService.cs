using Loxifi.CsvTable.Interfaces;
using System.Text;

namespace Loxifi.CsvTable.Services
{
    internal static class ToStringService
    {
        public static string TableToString(ICsvAdapter tableAdapter, CsvSaveOptions? options = null)
        {
            if (tableAdapter is null)
            {
                throw new ArgumentNullException(nameof(tableAdapter));
            }

            StringBuilder fileContent = new();

            bool firstLine = true;

            options ??= new();

            char? prependChar = null;

            if (options.ExcelTab)
            {
                prependChar = '\t';
            }

            if (options.IncludeHeaders)
            {
                _ = fileContent.Append(string.Join(",", tableAdapter.ColumnNames)); //ToString for the header name?

                firstLine = false;
            }

            foreach (IEnumerable<object?> dr in tableAdapter.Data)
            {
                if (!firstLine)
                {
                    _ = fileContent.Append(System.Environment.NewLine);
                }

                _ = fileContent.Append(ToCsvRow(dr, options.QuoteCharacter, prependChar));

                firstLine = false;
            }

            return fileContent.ToString();
        }

        private static string ToCsvRow(IEnumerable<object?> values, char? quoteCharacter = '"', char? prependChar = null)
        {
            if (values is null)
            {
                throw new ArgumentNullException(nameof(values));
            }

            string? qReplace = null;

            if (quoteCharacter.HasValue)
            {
                qReplace = $"{quoteCharacter.Value}{quoteCharacter.Value}";
            }

            StringBuilder sb = new();

            bool firstItem = false;

            foreach (object? value in values)
            {

                if (!firstItem)
                {
                    _ = sb.Append(',');
                    firstItem = false;
                }

                if (quoteCharacter.HasValue)
                {
                    _ = sb.Append(quoteCharacter.Value);
                }

                string oVal = ObjectToString(value);

                if (quoteCharacter.HasValue)
                {
                    oVal = oVal.Replace($"{quoteCharacter.Value}", qReplace);
                }

                if (prependChar.HasValue)
                {
                    _ = sb.Append(prependChar.Value);
                }

                _ = sb.Append(oVal);

                if (quoteCharacter.HasValue)
                {
                    _ = sb.Append(quoteCharacter.Value);
                }
            }

            return sb.ToString();
        }

        private static string ObjectToString(object? o) => o is DateTime dt ? $"{dt:yyyy-MM-dd HH:mm:ss.fff}" : $"{o}";
    }
}