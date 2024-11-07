namespace FormBuilderService.EventSourcing.Configurations.Handlers
{
    public class FileHandler
    {
        public static string GetCurrentDirectory()
        {
            return AppContext.BaseDirectory;
        }
        public static string GetFilePath(string eventId, string eventType)
        {
            return Path.Combine($"{GetCurrentDirectory()}\\{FolderNames.LogFolder}\\{eventType}", $"{eventType}_{eventId}.json");
        }

        public static bool DirectoryExists(string path)
        {
            return Directory.Exists(path);
        }

        public static bool CreateDirectory(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);

                return true;
            }

            return false;

        }

        public static void WriteFile(string filePath, string contents)
        {
            File.WriteAllText(filePath, contents);
        }
    }
}