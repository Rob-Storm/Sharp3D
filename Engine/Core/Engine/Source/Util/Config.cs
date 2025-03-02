using IniParser;
using IniParser.Model;
using Sharp3D.Core;

namespace Sharp3D.Util
{
    public static class Config
    {
        private static readonly Dictionary<string, string> ConfigFilePaths = new()
        {
            { "Game", @$"{Environment.CurrentDirectory}/Config/GameConfig.ini" },
            { "Engine", @$"{Environment.CurrentDirectory}/Config/EngineConfig.ini" }
        };

        private static readonly Dictionary<string, IniData> ConfigData = new();

        /// <summary>
        /// Parses all specified configuration files.
        /// </summary>
        public static void ParseAllConfigs()
        {
            FileIniDataParser parser = new FileIniDataParser();

            foreach (var (configType, path) in ConfigFilePaths)
            {
                try
                {
                    ConfigData[configType] = parser.ReadFile(path);
                }
                catch (Exception ex)
                {
                    Debug.Log($"Error parsing {configType} config: {ex.Message}", LogLevel.Error);
                }
            }
        }

        /// <summary>
        /// Returns the value of the key in the specified config file without the brackets.
        /// </summary>
        /// <param name="configType">Type of configuration file (e.g., "Game" or "Sharp2D").</param>
        /// <param name="section">Section name in the .ini file. Example: [Window]</param>
        /// <param name="key">Key name in the .ini file. Example: Window_Icon_Path</param>
        /// <returns>The value of the key formatted without brackets, or null if not found.</returns>
        public static string GetKeyValueFormatted(string configType, string section, string key)
        {
            if (!ConfigData.ContainsKey(configType))
            {
                Debug.Log($"Configuration type '{configType}' not loaded.", LogLevel.Error);
                return null;
            }

            if (!ConfigData[configType].Sections.ContainsSection(section) ||
                !ConfigData[configType][section].ContainsKey(key))
            {
                Debug.Log($"Section '{section}' or key '{key}' not found in {configType} config.", LogLevel.Error);
                return null;
            }

            string value = ConfigData[configType][section][key];
            return Remove(value, "[]");
        }

        private static string Remove(string input, string charsToRemove)
        {
            return new string(input.Where(c => !charsToRemove.Contains(c)).ToArray());
        }
    }
}
