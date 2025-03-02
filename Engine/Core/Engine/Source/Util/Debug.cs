using System.Collections.ObjectModel;

namespace Sharp3D.Core
{
    public static class Debug
    {
        public static List<DebugMessage> LogList = new List<DebugMessage>();
        public static void Log(string message = "Hello", LogLevel loglevel = LogLevel.Info)
        {
            string prefix = string.Empty;
            string debugMessage = string.Empty;
            string timestamp = $"{DateTime.Now.Hour}:{DateTime.Now.Minute}:{DateTime.Now.Second}.{DateTime.Now.Millisecond}";
            ConsoleColor color = ConsoleColor.White;

            LogLevel level = loglevel;

            switch (loglevel)
            {
                case LogLevel.Info:
                    prefix = "Info";
                    color = ConsoleColor.Cyan;
                    break;
                case LogLevel.Warning:
                    prefix = "Warning!";
                    color = ConsoleColor.Yellow;
                    break;
                case LogLevel.Error:
                    prefix = "ERROR!";
                    color = ConsoleColor.Red;
                    break;
                case LogLevel.Engine:
                    prefix = "Engine";
                    color = ConsoleColor.Magenta;
                    break;
                case LogLevel.Command:
                    prefix = "Command";
                    color = ConsoleColor.Green;
                    break;
            }

            Console.Write($"[{timestamp}] ");

            Console.ForegroundColor = color;
            Console.Write($"{prefix}: ");
            Console.ForegroundColor = ConsoleColor.White;

            Console.WriteLine(message);

            debugMessage = $"[{timestamp}] {prefix} - {message}";

            LogList.Add(new DebugMessage(debugMessage, level));
        }
    }

    public enum LogLevel
    {
        Info,
        Warning,
        Error,
        Engine,
        Command,
    };

    public struct DebugMessage
    {
        public string MessageText;
        public LogLevel DebugType;
        public DebugMessage(string message, LogLevel debugType)
        {
            MessageText = message;
            DebugType = debugType;
        }
    }
}
