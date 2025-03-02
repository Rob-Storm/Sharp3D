using System.Reflection;

namespace Sharp3D.Core.Command
{
    public static class CommandRegistry
    {
        private static Dictionary<string, MethodInfo> commands = new Dictionary<string, MethodInfo>();

        public static void RegisterCommands(Assembly assembly)
        {
            Debug.Log("Registering commands", LogLevel.Engine);

            var types = assembly.GetTypes();

            foreach(var type in types)
            {
                var methods = type.GetMethods();

                foreach(var method in methods)
                {
                    if(method.GetCustomAttribute<CommandAttribute>() != null)
                        commands.Add(method.Name, method);
                }
            }
        }

        public static bool ExecuteCommand(string input)
        {
            var parts = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length == 0) return false;

            string commandName = parts[0];
            if (!commands.TryGetValue(commandName, out var method))
                return false;

            var parameters = method.GetParameters();
            object[] args = new object[parameters.Length];

            for (int i = 0; i < parameters.Length; i++)
            {
                if (i + 1 >= parts.Length)
                {
                    args[i] = Type.Missing;
                    continue;
                }

                try
                {
                    args[i] = Convert.ChangeType(parts[i + 1], parameters[i].ParameterType);
                }
                catch
                {
                    return false;
                }
            }

            method.Invoke(null, args);
            return true;
        }
    }
}
