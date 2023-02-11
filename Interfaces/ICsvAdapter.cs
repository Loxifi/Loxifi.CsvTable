namespace Loxifi.CsvTable.Interfaces
{
    internal interface ICsvAdapter
    {
        public IEnumerable<string> ColumnNames { get; }

        public IEnumerable<IEnumerable<object?>> Data { get; }
    }
}