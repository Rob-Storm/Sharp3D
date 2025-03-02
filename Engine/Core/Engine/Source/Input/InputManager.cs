using Sharp3D.Core;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.GraphicsLibraryFramework;
using Sharp3D.GUI;
using OpenTK.Windowing.Desktop;
using ImGuiNET;

namespace Sharp3D.Game
{
    public class InputManager
    {
        private Camera _camera;
        private bool _cursorLocked = true;
        private bool _firstMove = true;
        private Vector2 _lastPos;
        private NativeWindow _window;
        private UIManager _uiManager;

        public InputMode InputMode { get; protected set; } = InputMode.Game;
        private Action<TextInputEventArgs> _textInputHandler;
        public InputManager(Camera camera, NativeWindow window, UIManager uiManager)
        {
            _camera = camera;
            _window = window;
            _uiManager = uiManager;

            _textInputHandler = HandleTextInput;
        }

        public void ProcessInput(FrameEventArgs args, KeyboardState keyboardState, MouseState mouseState, ref bool cursorLocked, ref CursorState cursorState)
        {
            const float cameraSpeed = 2.0f;
            const float sensitivity = 0.2f;

            if(InputMode == InputMode.Game)
            {
                if (keyboardState.IsKeyDown(Keys.W))
                    _camera.Position += _camera.Front * cameraSpeed * (float)args.Time;
                if (keyboardState.IsKeyDown(Keys.S))
                    _camera.Position -= _camera.Front * cameraSpeed * (float)args.Time;
                if (keyboardState.IsKeyDown(Keys.A))
                    _camera.Position -= _camera.Right * cameraSpeed * (float)args.Time;
                if (keyboardState.IsKeyDown(Keys.D))
                    _camera.Position += _camera.Right * cameraSpeed * (float)args.Time;
                if (keyboardState.IsKeyDown(Keys.Space))
                    _camera.Position += Vector3.UnitY * cameraSpeed * (float)args.Time;
                if (keyboardState.IsKeyDown(Keys.LeftControl))
                    _camera.Position -= Vector3.UnitY * cameraSpeed * (float)args.Time;

                if(keyboardState.IsKeyPressed(Keys.GraveAccent))
                {
                    _uiManager.AddWindow(new DevConsole(new System.Numerics.Vector2(800, 600)));

                    SetInputModeUI();
                }

                if (cursorLocked)
                {
                    if (_firstMove)
                    {
                        _lastPos = new Vector2(mouseState.X, mouseState.Y);
                        _firstMove = false;
                    }
                    else
                    {
                        var deltaX = mouseState.X - _lastPos.X;
                        var deltaY = mouseState.Y - _lastPos.Y;
                        _lastPos = new Vector2(mouseState.X, mouseState.Y);

                        _camera.Yaw += deltaX * sensitivity;
                        _camera.Pitch -= deltaY * sensitivity;
                    }
                }
            }
        }

        public void SetInputModeGame()
        {
            InputMode = InputMode.Game;

            _window.TextInput -= _textInputHandler;
            _window.CursorState = CursorState.Grabbed;

            _firstMove = true;

            _lastPos = new Vector2(_window.MousePosition.X, _window.MousePosition.Y);
        }

        public void SetInputModeUI()
        {

            InputMode = InputMode.UI;

            _window.TextInput += _textInputHandler;
            _window.CursorState = CursorState.Normal;

        }

        private void HandleTextInput(TextInputEventArgs e)
        {
            ImGui.GetIO().AddInputCharacter(e.AsString[0]);
        }
    }

    public enum InputMode
    {
        Game,
        UI,
        GameUI
    };

}
