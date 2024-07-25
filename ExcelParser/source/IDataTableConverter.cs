
using System.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

// 전략 패턴을 이용해 확장자 별로 다른 처리를 구현했다. 
public interface ConvertStrategy
{
    public string ConvertToString(DataTable table);

    public string ExportFileName(DataTable table);
}

public class CsharpClassConvertStrategy : ConvertStrategy
{
    public string ConvertToString(DataTable table)
    {

        string classCode = $"public class {table.TableName} \n {{ ";

        for (int i = 0; i < table.Columns.Count; i++)
        {
            string typeName = table.Rows[0][i].ToString();
            string propertyName = table.Rows[1][i].ToString();

            classCode += $"    public {typeName} {propertyName} ;\n";
        }

        classCode += "}\n";

        return classCode;
    }

    public string ExportFileName(DataTable table)
    {
        return table.TableName + STRINGS.CSHARP_FILE_SUFFIX;
    }
}

public class JsonConvertStrategy : ConvertStrategy
{
    public string ConvertToString(DataTable table)
    {
            
        var jsonArray = new JArray();

        // 0번째 열 => 타입 , 1번째 열 => 이름 이므로, 2 부터 시작한다. 
        for (int i = 2; i < table.Rows.Count; i++)
        {
            var jsonObject = new JObject();
            for (int j = 0; j < table.Columns.Count; j++)
            {
                string columnName = table.Rows[1][j].ToString();
                string cellValue = table.Rows[i][j].ToString();
                jsonObject.Add(columnName, cellValue);
            }
            jsonArray.Add(jsonObject);
        }

        string jsonData = JsonConvert.SerializeObject(jsonArray, Formatting.Indented);
        return jsonData;
    }

    public string ExportFileName(DataTable table)
    {
        return table.TableName + STRINGS.JSON_FILE_SUFFIX;
    }
}