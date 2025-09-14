namespace qlocktwo;

public static class FileUtility
{
    public static void Append(string filePath, string content)
    {
        try
        {
            // Opens file in append-mode
            using var writer = new StreamWriter(filePath, true);
            writer.WriteLine(content);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Exception: {ex.Message}");
        }
    }
}
