namespace API.Support.FileProvider
{
    /// <summary>
    /// Provide File read and write functions
    /// </summary>
    public class FileProviderService
    {
        public virtual async Task<string> Read(string filePath)
        {
            if (File.Exists(filePath))
            {
                // Reads file line by line
                using StreamReader reader = new StreamReader(filePath);
                var result = await reader.ReadToEndAsync();

                return result;
            }
            else
            {
                throw new Exception("file does not exist");
            }
        }

        public virtual async Task Write(string filePath, string content)
        {
            await File.WriteAllTextAsync(filePath, content);
        }
    }
}