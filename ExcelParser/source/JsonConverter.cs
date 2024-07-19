
using System.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


public class JsonConverter : IDataTableConverter
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