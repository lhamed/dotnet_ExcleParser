using System.Data;
using ExcelDataReader;



public class DataTableExporter
{
    DataTable[] dataTables;

    public DataTable[] ExportFrom(string path)
    {
        if (path == null)
        {
            throw new Exception($"Parameter {nameof(path)} is null.");
        }

        using (var stream = File.Open(path, FileMode.Open, FileAccess.Read))
        {
            using (var reader = ExcelReaderFactory.CreateReader(stream))
            {
                DataSet dataSet = reader.AsDataSet();

                dataTables = new DataTable[dataSet.Tables.Count];
            }
        }
        return dataTables;
    }
}