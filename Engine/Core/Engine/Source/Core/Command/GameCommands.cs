using Sharp3D.Graphics;
using Sharp3D.GUI;
using OpenTK.Graphics.OpenGL4;
using System.Reflection;

namespace Sharp3D.Core.Command
{
    public static class GameCommands
    {
        public static DevConsole DevConsole;

        [Command("Help", "Prints every command, their description, and arguments, if any")]
        public static void Help()
        {
            Debug.Log("------------ Commands: ------------", LogLevel.Command);

            foreach (var command in Type.GetType(typeof(GameCommands).ToString()).GetMethods().Where(m => m.GetCustomAttribute<CommandAttribute>() != null))
            {
                string description = command.GetCustomAttribute<CommandAttribute>().Description;

                string args = string.Empty;

                if (command.GetParameters().Length == 0)
                {
                    args = "void";
                }
                else
                {
                    foreach (var arg in command.GetParameters())
                    {
                        args += $"{arg.ParameterType.Name} {arg.Name}";

                        if (command.GetParameters().Length > 1)
                            args += $", ";

                    }
                }

                Debug.Log($"{command.Name} <{args}> {description}", LogLevel.Command);
            }

            Debug.Log("-----------------------------------", LogLevel.Command);
        }

        [Command("Clear", "Clears the console history")]
        public static void Clear()
        {
            DevConsole.ClearHistory();
        }

        [Command("Say", "Prints a message to the console")]
        public static void Say(string message)
        {
            Debug.Log(message);
        }

        [Command("Quit", "Exits the game")]
        public static void Quit()
        {
            Engine.Instance.Close();
        }

        [Command("TextureMode", "Sets the filtering mode of all textures")]
        public static void TextureMode(TextureMode mode)
        {
            Texture.SetTextureMode(mode);
        }

        [Command("GLDebug", "Prints OpenGL debug info")]
        public static void GLDebug()
        {
            if (GL.GetError() == ErrorCode.NoError)
                Debug.Log("No OpenGL errors", LogLevel.Command);
            else Debug.Log(GL.GetError().ToString(), LogLevel.Command);
        }

        [Command("Wireframe", "Toggles wireframe mode")]
        public static void Wireframe()
        {
            Engine.Instance.Renderer.ToggleWireframeMode();
        }
    }
}
