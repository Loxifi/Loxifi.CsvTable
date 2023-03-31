using System;
using System.Collections.Generic;
using System.Text;

namespace Loxifi.CsvTable.Exceptions
{
    public class NewlineInCsvException : Exception
    {
        public string RowData { get; }
        public NewlineInCsvException(string row) : base("Newline discovered in CSV")
        {
            this.RowData = row;
        }
    }
}
