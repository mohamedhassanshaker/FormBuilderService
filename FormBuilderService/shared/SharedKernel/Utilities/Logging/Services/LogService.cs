using SharedKernel.Utilities.Logging.Models;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;

namespace SharedKernel.Utilities.Logging.Services
{
    public static class LogService
    {
        private static string _logName = default!;

        public static void SetLogName(string logName)
        {
            _logName = logName;
        }

        public static void LogError(
            string message,
            [CallerMemberName] string callerName = "",
            [CallerFilePath] string callerPath = "",
            [CallerLineNumber] int? lineNumber = 0,
            params object[] parameters)
        {
            InitializeLog();

            using var eventLog = new EventLog(_logName);

            eventLog.Source = _logName;

            eventLog.WriteEntry(GenerateLogMessage(new LogMessage(message, callerName, callerPath, lineNumber, parameters)), EventLogEntryType.Error);
        }

        public static void LogInformation(
            string message,
            [CallerMemberName] string callerName = "",
            [CallerFilePath] string callerPath = "",
            [CallerLineNumber] int? lineNumber = 0,
            params object[] parameters)
        {
            using var eventLog = new EventLog(_logName);

            eventLog.Source = _logName;

            eventLog.WriteEntry(GenerateLogMessage(new LogMessage(message, callerName, callerPath, lineNumber, parameters)), EventLogEntryType.Information);

        }

        public static void LogError(
            Exception exception,
            [CallerMemberName] string callerName = "",
            [CallerFilePath] string callerPath = "",
            [CallerLineNumber] int? lineNumber = 0)
        {
            InitializeLog();

            using var eventLog = new EventLog(_logName);

            eventLog.Source = _logName;

            eventLog.WriteEntry(GenerateLogMessage(new LogMessage(exception.ToString(), callerName, callerPath, lineNumber)), EventLogEntryType.Error);
        }
        static void InitializeLog()
        {
            try
            {
                if (!EventLog.SourceExists(_logName))
                {
                    EventLog.CreateEventSource(_logName, _logName);
                }
            }

            catch (Exception)
            {
            }
        }

        static string GenerateLogMessage(LogMessage logMessage)
        {
            StringBuilder logBuilder = new StringBuilder();

            logBuilder.AppendLine($"Caller: {logMessage.CallerName}");
            logBuilder.AppendLine($"Caller Path: {logMessage.CallerPath}");
            logBuilder.AppendLine($"Caller Line: {logMessage.LineNumber}");
            logBuilder.AppendLine($"Log Message: {logMessage.Message}");

            return logBuilder.ToString();
        }

        public static void LogError(
            string v,
            int parameters,
            string language,
            string cancellationReferenceNumber,
            string cancellationBaseUrl,
            string returnUrl)
        {
            throw new NotImplementedException();
        }
    }
}