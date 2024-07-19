
using System.Data;

public class ConverterManager  // 게임 개발이 진행되며, 여러 포맷을 다뤄야 할지도 모르니, 인터페이스로 선언해서 확장에 대비했습니다. 
{
    IDataTableConverter[] dataTableConverters;
    public ConverterManager(IDataTableConverter[] converters)
    {
        this.dataTableConverters = converters;
    }

    public FileInfo[] ConvertToFileInfosFrom(DataTable dataTables)
    {
        int converterCount = dataTableConverters.Length; // 2번 이상 사용할 변수는 따로 묶습니다. 

        // var를 적극 활용합니다. 타입이 분명한 네이밍이라면 말이죠. 
        var resultStrings = new FileInfo[converterCount];

        for (int i = 0; i < converterCount; i++)
        {
            // 1줄 짜리 포문도 바디를 분명하게 선언하기를 좋아합니다. 
            var content = dataTableConverters[i].ConvertToString(dataTables);
            var fileName = dataTableConverters[i].ExportFileName(dataTables);
            resultStrings[i] = new FileInfo(fileName, content);
        }

        return resultStrings;
    }
}