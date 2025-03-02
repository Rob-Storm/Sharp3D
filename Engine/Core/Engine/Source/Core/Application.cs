using OpenTK.Windowing.Common.Input;
using Sharp3D.Core.Command;
using Sharp3D.Util;
using StbImageSharp;

namespace Sharp3D.Core
{
    public class Application
    {
        public void Run()
        {
            CommandRegistry.RegisterCommands(System.Reflection.Assembly.GetExecutingAssembly());
            Config.ParseAllConfigs();

            AppDomain.CurrentDomain.UnhandledException += (sender, args) =>
            {
                Exception ex = (Exception)args.ExceptionObject;
                Debug.Log($"Unhandled Exception: {ex.Message}\n{ex.StackTrace}", LogLevel.Error);
            };

            int windowWidth, windowHeight;
            string title;
            WindowIcon icon;

            var imagePath = Config.GetKeyValueFormatted("Engine", "Window", "Window_Icon_Path");

            using (Stream stream = File.OpenRead(imagePath))
            {
                ImageResult image = ImageResult.FromStream(stream, ColorComponents.RedGreenBlueAlpha);

                icon = new WindowIcon(new Image(image.Width, image.Height, image.Data));
            }

            windowWidth = int.Parse(Config.GetKeyValueFormatted("Engine", "Window", "Window_Width"));
            windowHeight = int.Parse(Config.GetKeyValueFormatted("Engine", "Window", "Window_Height"));
            title = Config.GetKeyValueFormatted("Engine", "Window", "Window_Title");

            Engine game = new Engine(windowWidth, windowHeight, title, icon);

            game.Run();

        }
    }

}
