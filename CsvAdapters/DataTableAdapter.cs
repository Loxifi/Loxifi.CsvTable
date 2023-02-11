using Loxifi.CsvTable.Interfaces;
using System.Data;

namespace Loxifi.CsvTable.CsvAdapters
{
    internal class DataTableAdapter : ICsvAdapter
    {
        private readonly DataTable _table;

        public DataTableAdapter(DataTable table)
        {
            _table = table;
        }

        public IEnumerable<string> ColumnNames => _table.Columns.Cast<DataColumn>().Select(c => c.ColumnName);

        public IEnumerable<IEnumerable<object?>> Data => _table.Rows.Cast<DataRow>().Select(r => r.ItemArray);
    }
}