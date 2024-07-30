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
        System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance); // 사용하는 모듈이 의존성 있음

        var extracter = new ExcelFileExtracter
        (
            new JsonConvertStrategy(),
            new CsharpClassConvertStrategy()
        ); // 스탬프 결합도 

        string[] targetPathes = Directory.GetFiles // 외부 결합도
        (
            STRINGS.EXCEL_FOLDER_PATH,
            STRINGS.XLSX_SEARCH_PATTERN
        );

        FileInfo[] fileInfos = extracter.Extract(targetPathes); // 스탬프 결합도 

        foreach (var fileInfo in fileInfos)
        {
            fileInfo.Write();
        }
    }
}


