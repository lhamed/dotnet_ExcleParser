using System.Data;

public class ExcelParser
{
    ConvertContext convertContext;
    public ExcelParser(ConvertContext convertContext)
    {
        this.convertContext = convertContext;
    }

    public void Parse(string[] pathes)
    {
        DataTable[] dataTables = ExportDataTableFromExcel(pathes);
        FileInfo[] exportedFileInfos = ConvertToFileInfoFromDataTable(dataTables);

        foreach (var fileInfo in exportedFileInfos)
        {
            fileInfo.Write();
        }
    }

    DataTable[] ExportDataTableFromExcel(string[] pathes)
    {
        List<DataTable> exportedTables = new List<DataTable>();
        var dataTableExporter = new DataTableExporter();
        foreach (var path in pathes)
        {
            DataTable[] tables = dataTableExporter.ExportFrom(path);
            foreach (var table in tables)
            {
                exportedTables.Add(table);
            }
        }
        return exportedTables.ToArray();
    }

    FileInfo[] ConvertToFileInfoFromDataTable(DataTable[] tables)
    {
        List<FileInfo> exportedFileInfos = new List<FileInfo>();

            
        foreach (var table in tables)
        {
            var fileInfos = convertContext.ConvertToFileInfosFrom(table);
            foreach (var fileInfo in fileInfos)
            {
                exportedFileInfos.Add(fileInfo);
            }
        }

        return exportedFileInfos.ToArray();
    }


}
