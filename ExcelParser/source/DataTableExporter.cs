using System.Data;
using ExcelDataReader;

public class DataTableExporter
{
    public DataTable[] Export(string path)
    {
        List<DataTable> dataTables = new List<DataTable>();

        using (var stream = File.Open(path, FileMode.Open, FileAccess.Read))
        {
            using (var reader = ExcelReaderFactory.CreateReader(stream))
            {
                DataSet dataSet = reader.AsDataSet();

                var d = dataSet.Tables;
                foreach(DataTable a in d)
                {
                    dataTables.Add(a);
                }
            }
        }
        return dataTables.ToArray();
    }
}