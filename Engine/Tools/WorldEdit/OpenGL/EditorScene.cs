using OpenTK.Mathematics;
using OpenTK.Windowing.GraphicsLibraryFramework;
using OpenTK.Wpf;
using Sharp3D.Core;
using Sharp3D.Graphics;
using System.Windows.Input;
using WorldEdit.OpenGL;

public unsafe class EditorScene
{
    public Renderer Renderer;
    public World World;

    private EditorInputManager _input;
    private GLWpfControl _wpfControl;
    private Camera _camera;

    private VertexArray _vertexArray;
    private VertexBuffer vertexBuffer;
    private IndexBuffer indexBuffer;
    private VertexBufferLayout layout;

    private Shader _shader;

    public EditorScene(GLWpfControl wpfControl)
    {
        _wpfControl = wpfControl;
        Renderer = new Renderer();
        World = new World();

        _camera = new Camera(new Vector3(8, 8, 8), 1920 / 1080f);

        _shader = new Shader(@"C:\Users\The1Wolfcast\source\Sharp3D\Sharp3D\Engine\Core\Engine\Shaders\shader.vert",
                             @"C:\Users\The1Wolfcast\source\Sharp3D\Sharp3D\Engine\Core\Engine\Shaders\shader.frag");

        var brushData = World.GetBrushData();
        float[] vertexArrayData = brushData.Item1;
        uint[] indexArrayData = brushData.Item2;

        _shader.Bind();

        _vertexArray = new VertexArray();
        _vertexArray.Bind();

        vertexBuffer = new VertexBuffer(vertexArrayData);
        indexBuffer = new IndexBuffer(indexArrayData);
        layout = new VertexBufferLayout();

        layout.Add<float>(3); // Position
        layout.Add<float>(2); // Tex coords
        layout.Add<float>(3); // Normals

        _vertexArray.AddBuffer(vertexBuffer, layout);

        Renderer.Load();

        _input = new EditorInputManager(_camera, wpfControl);
        _input.InitializeInputHandling(wpfControl);
    }

    public void Update(float deltaTime)
    {
        _input.ProcessInput(deltaTime);
    }

    public void Render()
    {
        Renderer.Render(_vertexArray, indexBuffer, _shader, _camera);
    }
}
