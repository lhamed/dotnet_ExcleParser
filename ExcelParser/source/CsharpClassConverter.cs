
using System.Data;

public class CsharpClassConverter : IDataTableConverter
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