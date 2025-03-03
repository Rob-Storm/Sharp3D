using OpenTK.Mathematics;
using OpenTK.Windowing.GraphicsLibraryFramework;

public class Input
{
    //singleton stuffs
    private static Input _instance;
    public static Input Instance => _instance ??= new Input();

    private HashSet<Keys> _currentKeys = new();
    private HashSet<Keys> _previousKeys = new();
    private HashSet<MouseButton> _currentMouseButtons = new();
    private HashSet<MouseButton> _previousMouseButtons = new();

    public Vector2 MousePosition { get; private set; }
    public Vector2 MouseDelta { get; private set; }

    private bool _stateUpdated = false;

    private Input() { }

    public void Update(KeyboardState keyboard, MouseState mouse)
    {
        if (_stateUpdated) return;
        _stateUpdated = true;

        _previousKeys = new HashSet<Keys>(_currentKeys);
        _previousMouseButtons = new HashSet<MouseButton>(_currentMouseButtons);

        _currentKeys.Clear();
        foreach (Keys key in Enum.GetValues(typeof(Keys)))
        {
            if (keyboard.IsKeyDown(key))
                _currentKeys.Add(key);
        }

        _currentMouseButtons.Clear();
        foreach (MouseButton button in Enum.GetValues(typeof(MouseButton)))
        {
            if (mouse.IsButtonDown(button))
                _currentMouseButtons.Add(button);
        }

        Vector2 newMousePosition = new(mouse.X, mouse.Y);
        MouseDelta = newMousePosition - MousePosition;
        MousePosition = newMousePosition;
    }

    public void ResetState() => _stateUpdated = false;

    public static bool IsKeyDown(Keys key) => Instance._currentKeys.Contains(key);
    public static bool IsKeyPressed(Keys key) => Instance._currentKeys.Contains(key) && !Instance._previousKeys.Contains(key);
    public static bool IsKeyReleased(Keys key) => !Instance._currentKeys.Contains(key) && Instance._previousKeys.Contains(key);

    public static bool IsMouseButtonDown(MouseButton button) => Instance._currentMouseButtons.Contains(button);
    public static bool IsMouseButtonPressed(MouseButton button) => Instance._currentMouseButtons.Contains(button) && !Instance._previousMouseButtons.Contains(button);
    public static bool IsMouseButtonReleased(MouseButton button) => !Instance._currentMouseButtons.Contains(button) && Instance._previousMouseButtons.Contains(button);
}
