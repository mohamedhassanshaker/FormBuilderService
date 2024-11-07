namespace SharedKernel.Utilities.Logging.Models
{
    public class LogMessage
    {
        public string Message { get; }
        public string CallerName { get; }
        public string CallerPath { get; }
        public int? LineNumber { get; }
        public object[] Parameters { get; }

        public LogMessage(string message, string callerName, string callerPath, int? lineNumber, params object[] parameters)
        {
            Message = message;
            CallerName = callerName;
            CallerPath = callerPath;
            LineNumber = lineNumber;
            Parameters = parameters;
        }
    }
}