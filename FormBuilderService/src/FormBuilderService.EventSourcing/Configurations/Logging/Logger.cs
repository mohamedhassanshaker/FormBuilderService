using FormBuilderService.EventSourcing.Configurations.Handlers;
using FormBuilderService.EventSourcing.Events.Abstraction;
using System.Text.Json;

namespace FormBuilderService.EventSourcing.Configurations.Logging
{
    public class Logger
    {
        public static bool LogDataToJsonFile(DomainEventMessage domainEventMessage)
        {
            try
            {
                if (!FileHandler.DirectoryExists($"{FileHandler.GetCurrentDirectory()}\\{FolderNames.LogFolder}\\{domainEventMessage.EventType}"))
                    FileHandler.CreateDirectory($"{FileHandler.GetCurrentDirectory()}\\{FolderNames.LogFolder}\\{domainEventMessage.EventType}");

                string filePath = FileHandler.GetFilePath(domainEventMessage.EventId.ToString(), domainEventMessage.EventType);

                JsonSerializerOptions options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };
                string json = JsonSerializer.Serialize(domainEventMessage, options);

                FileHandler.WriteFile(filePath, json);

                return true;
            }

            catch (Exception)
            {
                return false;
            }
        }
    }
}