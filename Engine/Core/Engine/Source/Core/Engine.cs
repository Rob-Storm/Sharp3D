using ImGuiNET;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Common.Input;
using OpenTK.Windowing.Desktop;
using Sharp3D.Input;
using Sharp3D.Graphics;
using Sharp3D.GUI;

namespace Sharp3D.Core
{
    public class Engine : GameWindow
    {
        public static Engine Instance { get; private set; }

        public Renderer Renderer;
        public World World;
        public string WindowTitle { get; private set; }

        private UIManager _uiManager;
        public InputManager InputManager;
        private CursorState _cursorState;

        private Camera _camera;
        private System.Diagnostics.Stopwatch _timer = new System.Diagnostics.Stopwatch();
        private bool _cursorLocked = true;

        private VertexArray _vertexArray;
        VertexBuffer vertexBuffer;
        IndexBuffer indexBuffer;
        VertexBufferLayout layout;

        private Shader _shader;
        private Texture _texture;

        public Engine(int width, int height, string title, WindowIcon icon) : base(GameWindowSettings.Default, new NativeWindowSettings() { ClientSize = (width, height), Title = title, Icon = icon })
        {
            if (Instance != null) throw new Exception("Game instance already exists!");
            Instance = this;

            WindowTitle = Title;

            Debug.Log("Initializing engine", LogLevel.Engine);
        }

        protected override void OnLoad()
        {
            base.OnLoad();

            Renderer = new Renderer();
            World = new World();

            var chunkData = World.GetBrushData();

            float[] vertexArrayData = chunkData.Item1;
            uint[] indexArrayData = chunkData.Item2;

            _shader = new Shader("Shaders/shader.vert", "Shaders/shader.frag");
            _texture = Texture.LoadFromFile("Resources/TextureAtlas.png");

            _shader.Bind();
            _texture.Use(TextureUnit.Texture0);
            _shader.SetInt("texture0", 0);

            _vertexArray = new VertexArray();
            _vertexArray.Bind();

            vertexBuffer = new VertexBuffer(vertexArrayData);
            indexBuffer = new IndexBuffer(indexArrayData);
            layout = new VertexBufferLayout();

            layout.Add<float>(3); // Position
            layout.Add<float>(2); // Tex coords
            layout.Add<float>(3); // Normals

            _vertexArray.AddBuffer(vertexBuffer, layout);

            _camera = new Camera(new Vector3(8,8,8), Size.X / (float)Size.Y);

            Renderer.Load();

            _uiManager = new UIManager(ClientSize.X, ClientSize.Y);
            InputManager = new InputManager(_camera, this, _uiManager);

            InputManager.SetInputModeGame();

            _timer.Start();

            Debug.Log("Staring frame update", LogLevel.Engine);
        }

        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            _cursorState = CursorState;

            Title = $"{WindowTitle} - {args.Time * 1000:F2}ms";

            base.OnUpdateFrame(args);

            _uiManager.Update(this, (float)args.Time);
            InputManager.ProcessInput(args, KeyboardState, MouseState, ref _cursorLocked, ref _cursorState);
        }

        protected override void OnRenderFrame(FrameEventArgs args)
        {
            base.OnRenderFrame(args);

            Renderer.Render(_vertexArray, indexBuffer, _shader, _camera);
            _uiManager.Render();

            SwapBuffers();
        }

        protected override void OnFramebufferResize(FramebufferResizeEventArgs e)
        {
            base.OnFramebufferResize(e);

            Renderer.OnFramebufferResize(e);

            _uiManager.Resize(e.Width, e.Height);

            _camera.AspectRatio = e.Width / (float)e.Height;
        }

        protected override void OnUnload()
        {
            base.OnUnload();
            Renderer.Dispose();
        }
    }

}
