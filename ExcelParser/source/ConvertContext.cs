
using System.Data;

public class ConvertContext
{
    IConvertStrategy[] dataTableConverters;
    public ConvertContext(params IConvertStrategy[] converters)
    {
        this.dataTableConverters = converters;
    }

    public FileInfo[] Convert(DataTable dataTables)
    {
        var result = new List<FileInfo>();
        foreach (var convertStrategy in dataTableConverters)
        {
            var content = convertStrategy.Convert(dataTables);
            var fileName = convertStrategy.GetFileName(dataTables);
            result.Add( new FileInfo(fileName, content));
        }
        return result.ToArray();
    }
}