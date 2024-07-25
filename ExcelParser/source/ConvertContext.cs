
using System.Data;

public class ConvertContext  // 게임 개발이 진행되며, 여러 포맷을 다뤄야 할지도 모르니, 인터페이스로 선언해서 확장에 대비했습니다. 
{
    ConvertStrategy[] dataTableConverters;
    public ConvertContext(params ConvertStrategy[] converters)
    {
        this.dataTableConverters = converters;
    }

    public FileInfo[] ConvertToFileInfosFrom(DataTable dataTables)
    {
        int converterCount = dataTableConverters.Length; // 2번 이상 사용할 변수는 따로 묶습니다. 

        var resultStrings = new FileInfo[converterCount];
        for (int i = 0; i < converterCount; i++)
        {
            var content = dataTableConverters[i].ConvertToString(dataTables);
            var fileName = dataTableConverters[i].ExportFileName(dataTables);
            resultStrings[i] = new FileInfo(fileName, content);
        }

        return resultStrings;
    }
}