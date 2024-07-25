using System.Text.Encodings; 

public static class STRINGS
{
    public const string EXCEL_FOLDER_PATH = "EXCEL";
    public const string XLSX_SEARCH_PATTERN = "*.xlsx";
    public const string RESULT_FOLDER_PATH = "RESULT";

    public const string CSHARP_FILE_SUFFIX = ".cs";
    public const string JSON_FILE_SUFFIX = ".json";
}

class Program
{
    static void Main()
    {
        System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

        ConvertContext convertContext = new ConvertContext
        (
            new JsonConvertStrategy(), 
            new CsharpClassConvertStrategy()
        );

        var excelParser = new ExcelParser(convertContext);
        string[] targetPathes = Directory.GetFiles
        (
            STRINGS.EXCEL_FOLDER_PATH, 
            STRINGS.XLSX_SEARCH_PATTERN
        );

        excelParser.Parse(targetPathes);
    }
}


