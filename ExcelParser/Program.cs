using System.Data;
using System.IO;
using System;
using System.Text;
using ExcelDataReader;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Reflection;

class Program
{
    static void Main(string[] args)
    {
        // 엑셀 파일 경로
        string excelFilePath = "EXCEL";
        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

        string[] excelFiles = Directory.GetFiles(excelFilePath, "*.xlsx");

        foreach (string excelFile in excelFiles)
        {
            ProcessExcelFile(excelFile);
        }
    }

    static void ProcessExcelFile(string path)
    {
        // 엑셀 파일에서 데이터 읽기
        using (var stream = File.Open(path, FileMode.Open, FileAccess.Read))
        {
            using (var reader = ExcelReaderFactory.CreateReader(stream))
            {
                // 첫 번째 시트로 제한할 수도 있습니다.
                DataSet dataSet = reader.AsDataSet();

                for (int i = 0; i < dataSet.Tables.Count; i++)
                {
                    // 첫 번째 시트 선택
                    DataTable dataTable = dataSet.Tables[i];

                    // C# 클래스 생성
                    string classCode = GenerateClass(dataTable);

                    // JSON 데이터 생성
                    string jsonData = ConvertDataTableToJson(dataTable);

                    // 결과 출력
                    Console.WriteLine("C# Class:\n" + classCode);
                    Console.WriteLine("\nJSON Data:\n" + jsonData);

                    WriteFile(dataTable.TableName + ".cs", classCode);
                    WriteFile(dataTable.TableName + ".json", jsonData);
                }
            }
        }
    }

    // C# 클래스 생성 함수
    static string GenerateClass(DataTable dataTable)
    {
        string classCode = $"public class {dataTable.TableName} \n {{ ";

        for (int i = 0; i < dataTable.Columns.Count; i++)
        {
            string typeName = dataTable.Rows[0][i].ToString();
            string propertyName = dataTable.Rows[1][i].ToString();

            classCode += $"    public {typeName} {propertyName} {{ get; set; }}\n";
        }

        classCode += "}\n";

        return classCode;
    }

    // DataTable을 JSON으로 변환하는 함수
    static string ConvertDataTableToJson(DataTable dataTable)
    {
        var jsonArray = new JArray();

        // 첫 번째 줄과 두 번째 줄은 필드 이름이나 타입이 아니므로 제외하고 세 번째 줄부터 데이터를 읽습니다.
        for (int i = 2; i < dataTable.Rows.Count; i++)
        {
            var jsonObject = new JObject();
            for (int j = 0; j < dataTable.Columns.Count; j++)
            {
                string columnName = dataTable.Rows[1][j].ToString();
                string cellValue = dataTable.Rows[i][j].ToString();
                jsonObject.Add(columnName, cellValue);
            }
            jsonArray.Add(jsonObject);
        }

        string jsonData = JsonConvert.SerializeObject(jsonArray, Formatting.Indented);
        return jsonData;
    }


    static void WriteFile(string fileName, string content)
    {

        // 파일 경로 생성

        string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "RESULT");
        string filePath = Path.Combine(folderPath, fileName);

        Directory.CreateDirectory(folderPath);
        // 파일 쓰기
        File.WriteAllText(filePath, content);

        Console.WriteLine($"File '{fileName}' has been written successfully.");
    }
}

