using Sharp3D.Core;
using OpenTK.Windowing.Desktop;

namespace Sharp3D.GUI
{
    public class UIManager
    {
        public static UIManager Instance;

        private ImGuiController _controller;

        private List<GuiWindow> _windows;
        private List<GuiWindow> _windowsToAdd;
        private List<GuiWindow> _windowsToRemove;

        public UIManager(int width, int height)
        {
            if (Instance != null) throw new Exception("UIManager instance already exists!");
            Instance = this;

            _controller = new ImGuiController(width, height);

            _windows = new List<GuiWindow>();
            _windowsToAdd = new List<GuiWindow>();
            _windowsToRemove = new List<GuiWindow>();
        }

        public void Update(GameWindow window, float deltaTime)
        {
            _controller.Update(window, deltaTime);

            foreach (GuiWindow windowElement in _windows)
            {
                windowElement.Render();
            }

            if (_windowsToAdd.Count > 0)
            {
                _windows.AddRange(_windowsToAdd);
                _windowsToAdd.Clear();
            }

            if (_windowsToRemove.Count > 0)
            {
                foreach (GuiWindow windowElement in _windowsToRemove)
                {
                    _windows.Remove(windowElement);
                }
                _windowsToAdd.Clear();
            }
        }

        public void Render()
        {
            _controller.Render();
        }

        public void Resize(int width, int height)
        {
            _controller.WindowResized(width, height);
        }

        public void AddWindow(GuiWindow window)
        {
            _windowsToAdd.Add(window);
        }
    }

}
