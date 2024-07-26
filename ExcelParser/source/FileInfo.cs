public class FileInfo
{
    public string fileName;
    public string fileContent;
    public FileInfo(string fileName, string fileContent)
    {
        this.fileName = fileName;
        this.fileContent = fileContent;
    }

    public void Write()
    {
        string folderPath = Path.Combine(Directory.GetCurrentDirectory(), STRINGS.RESULT_FOLDER_PATH);
        string filePath = Path.Combine(folderPath, fileName);

        Directory.CreateDirectory(folderPath);
        File.WriteAllText(filePath, fileContent);

        Console.WriteLine($"File '{fileName}' has been written successfully.");
    }
}