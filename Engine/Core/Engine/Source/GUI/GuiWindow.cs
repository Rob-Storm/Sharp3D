using ImGuiNET;
using System.Numerics;

namespace Sharp3D.GUI
{
    public abstract class GuiWindow
    {
        public string Title { get; private set; }

        public Vector2 WindowSize { get; private set; }
        private Vector2 _initialPosition;
        public ImGuiWindowFlags WindowFlags { get; private set; }

        private bool _isActive = true;
        public bool IsActive
        {
            get { return _isActive; }
            set { _isActive = value; }
        }

        private bool _prevIsActive = true;

        public EventHandler OnWindowClosed;


        protected GuiWindow(string title, Vector2 size, Vector2 position, ImGuiWindowFlags flags)
        {
            Title = title;
            WindowSize = size;
            _initialPosition = position;
            WindowFlags = flags;
        }

        protected abstract void RenderContent();

        public void Render()
        {
            _prevIsActive = IsActive;

            if (!IsActive) return;

            ImGui.SetNextWindowSize(WindowSize, ImGuiCond.FirstUseEver);
            ImGui.SetNextWindowPos(_initialPosition, ImGuiCond.FirstUseEver);

            if (ImGui.Begin(Title, ref _isActive, WindowFlags)) RenderContent();

            ImGui.End();

            if(_prevIsActive && !IsActive)
            {
                CloseWindow();
            }
        }

        public virtual void CloseWindow()
        {
            OnWindowClosed?.Invoke(this, EventArgs.Empty);

        }

    }
}
