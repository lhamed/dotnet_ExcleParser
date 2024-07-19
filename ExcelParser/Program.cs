// 안녕하세요. 클라이언트 개발자 정윤재입니다. 
// 이 프로젝트는 간단한 포트폴리오이자, 코딩스타일을 보여드리기 위한 것입니다. 
// 잘 부탁드립니다.

// 주석은 /* */ 보다는 // 를 선호합니다. 

public static class STRINGS
{
    // 일명 매직 넘버, 스트링들은 따로 선언해두기를 좋아합니다. 
    // 런타임 동안 바뀌지 않을 상수는 대문자와 언더바를 활용합니다. 
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
        ConverterManager converterManager = new ConverterManager
        ([
            new JsonConverter(), 
            new CsharpClassConverter(), 
        ]);

        var excelParser = new ExcelParser(converterManager);
        string[] targetPathes = Directory.GetFiles
        (
            STRINGS.EXCEL_FOLDER_PATH, 
            STRINGS.XLSX_SEARCH_PATTERN
        );

        excelParser.Parse(targetPathes);
    }
}


