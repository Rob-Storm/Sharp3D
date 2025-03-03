using OpenTK.Mathematics;
using OpenTK.Windowing.GraphicsLibraryFramework;
using OpenTK.Wpf;
using Sharp3D.Core;
using System.Windows;
using System.Windows.Input;
using WorldEdit.Util;

namespace WorldEdit.OpenGL
{
    public class EditorInputManager
    {
        public InputMode InputMode { get; protected set; } = InputMode.Game;

        private Camera _camera;
        private Vector2 _lastPos;
        private bool _firstMove;

        private GLWpfControl _wpfControl;
        private HashSet<Key> _pressedKeys = new HashSet<Key>();

        public EditorInputManager(Camera camera, GLWpfControl wpfControl)
        {
            _camera = camera;
            _wpfControl = wpfControl;
        }

        public void InitializeInputHandling(UIElement element)
        {
            element.PreviewKeyDown += (s, e) => _pressedKeys.Add(e.Key);
            element.PreviewKeyUp += (s, e) => _pressedKeys.Remove(e.Key);
        }

        public void ProcessInput(float deltaTime)
        {
            const float cameraSpeed = 5.0f;

            if (InputMode == InputMode.Game)
            {
                // first person positioning
                if (_pressedKeys.Contains(Key.W))
                    _camera.Position += _camera.Front * cameraSpeed * deltaTime;
                if (_pressedKeys.Contains(Key.S))
                    _camera.Position -= _camera.Front * cameraSpeed * deltaTime;
                if (_pressedKeys.Contains(Key.A))
                    _camera.Position -= _camera.Right * cameraSpeed * deltaTime;
                if (_pressedKeys.Contains(Key.D))
                    _camera.Position += _camera.Right * cameraSpeed * deltaTime;

                // absolute Y axis positioning
                if (_pressedKeys.Contains(Key.Space))
                    _camera.Position += Vector3.UnitY * cameraSpeed * deltaTime;
                if (_pressedKeys.Contains(Key.LeftCtrl))
                    _camera.Position -= Vector3.UnitY * cameraSpeed * deltaTime;

                // misc controls
                if (_pressedKeys.Contains(Key.Z))
                {
                    SetInputModeUI();
                    _pressedKeys.Remove(Key.Z);
                }

                // mouse controls
                if (InputMode == InputMode.Game)
                {
                    var position = Mouse.GetPosition(_wpfControl);
                    var currentPos = new Vector2((float)position.X, (float)position.Y);

                    if (_firstMove)
                    {
                        _lastPos = currentPos;
                        _firstMove = false;
                        return;
                    }

                    float deltaX = currentPos.X - _lastPos.X;
                    float deltaY = currentPos.Y - _lastPos.Y;
                    _lastPos = currentPos;

                    float sensitivity = 0.2f;
                    _camera.Yaw += deltaX * sensitivity;
                    _camera.Pitch -= deltaY * sensitivity;
                }
            }
            else if(InputMode == InputMode.UI)
            {
                if (_pressedKeys.Contains(Key.Z) && _wpfControl.IsMouseOver)
                {
                    SetInputModeGame(_wpfControl);
                    _pressedKeys.Remove(Key.Z);
                }
            }
        }

        public void SetInputModeGame()
        {
            InputMode = InputMode.Game;
            _firstMove = true;
        }

        public void SetInputModeGame(FrameworkElement control)
        {
            InputMode = InputMode.Game;
            _firstMove = true;

            //CursorHelper.WrapCursor(control);
        }


        public void SetInputModeUI()
        {
            InputMode = InputMode.UI;

            //CursorHelper.UnlockCursor();
        }
    }

    public enum InputMode
    {
        Game,
        UI,
        GameUI
    };
}