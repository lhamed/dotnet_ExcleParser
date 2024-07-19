
using System.Data;

public interface IDataTableConverter
{
    public string ConvertToString(DataTable table);

    public string ExportFileName(DataTable table);
}