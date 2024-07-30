using System.Data;
using Microsoft.VisualBasic;

public class ExcelFileExtracter
{
    ExcelFileConverter fileInfoExtracter;
     IConvertStrategy[] convertStrategies;
    public ExcelFileExtracter(params IConvertStrategy[] convertStrategies)
    {
        this.convertStrategies = convertStrategies;
        this.fileInfoExtracter = new ExcelFileConverter();
    }

    public FileInfo[] Extract(string[] pathes)
    {
        List<FileInfo> result = new List<FileInfo>();

        foreach(var strategy in convertStrategies)
        {
            FileInfo[] fileInfos = fileInfoExtracter.Convert(strategy, pathes);
            result.AddRange(fileInfos);
        }

        return result.ToArray();
    }
}

public class ExcelFileConverter
{
    DataTableExporter dataTableExporter;

    public ExcelFileConverter()
    {
        this.dataTableExporter = new DataTableExporter();
    }

    public FileInfo[] Convert(IConvertStrategy strategy, string[] pathes)
    {
        List<DataTable> exportedTables = new List<DataTable>();
        foreach (var path in pathes)
        {
            DataTable[] tables = dataTableExporter.Export(path);
            foreach (var table in tables)
            {
                exportedTables.Add(table);
            }
        }

        List<FileInfo> exportedFileInfos = new List<FileInfo>();
        foreach (var table in exportedTables)
        {
            string fileContent = strategy.Convert(table);
            string fileName = strategy.GetFileName(table);

            FileInfo fileInfo = new FileInfo(fileName,fileContent);
            exportedFileInfos.Add(fileInfo);
        }

        return exportedFileInfos.ToArray();
    }
}
