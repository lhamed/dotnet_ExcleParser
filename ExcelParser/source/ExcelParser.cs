using System.Data;



public class ExcelParser
{
    ConvertContext converterManager;
    public ExcelParser(ConvertContext convertManager)
    {
        this.converterManager = convertManager;
    }

    public void Parse(string[] pathes)
    {
        DataTable[] dataTables = ExportDataTableFromExcel(pathes);
        FileInfo[] exportedFileInfos = ConvertToFileInfoFromDataTable(dataTables);

        foreach (var fileInfo in exportedFileInfos)
        {
            string fileName = fileInfo.fileName;
            string fileContent = fileInfo.fileContent;

            WriteFile(fileName, fileContent);
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
            var fileInfos = converterManager.ConvertToFileInfosFrom(table);
            foreach (var fileInfo in fileInfos)
            {
                exportedFileInfos.Add(fileInfo);
            }
        }

        return exportedFileInfos.ToArray();
    }

    void WriteFile(string fileName, string content)
    {
        string folderPath = Path.Combine(Directory.GetCurrentDirectory(), STRINGS.RESULT_FOLDER_PATH);
        string filePath = Path.Combine(folderPath, fileName);

        Directory.CreateDirectory(folderPath);
        File.WriteAllText(filePath, content);

        Console.WriteLine($"File '{fileName}' has been written successfully.");
    }
}
