using OpenTK.Graphics.OpenGL4;
using Sharp3D.Core;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;

namespace Sharp3D.Graphics
{
    public class Renderer : IDisposable
    {
        private bool _wireframeEnabled = false;
        public Renderer()
        {

        }

        public void Load()
        {
            GL.ClearColor(0.2f, 0.3f, 0.3f, 1.0f);
            GL.Enable(EnableCap.DepthTest);
            GL.Enable(EnableCap.CullFace);
            GL.CullFace(TriangleFace.Back);
            GL.FrontFace(FrontFaceDirection.Ccw);
            GL.DepthFunc(DepthFunction.Less);

            GL.PolygonMode(TriangleFace.FrontAndBack, PolygonMode.Fill);
        }

        public void Render(VertexArray vertexArray, IndexBuffer indexBuffer, Shader shader, Camera camera)
        {
            Matrix4 model = Matrix4.Identity;

            shader.SetUniformMatrix4("uModel", model);
            shader.SetUniformMatrix4("uView", camera.GetViewMatrix());
            shader.SetUniformMatrix4("uProjection", camera.GetProjectionMatrix());

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            shader.Bind();

            vertexArray.Bind();
            indexBuffer.Bind();

            GL.DrawElements(PrimitiveType.Triangles, indexBuffer.GetCount(), DrawElementsType.UnsignedInt, 0);
        }

        public void OnFramebufferResize(FramebufferResizeEventArgs e)
        {
            GL.Viewport(0, 0, e.Width, e.Height);
        }

        public void Dispose()
        {
            //_shader.Dispose();
        }

        public void ToggleWireframeMode()
        {
            _wireframeEnabled = !_wireframeEnabled;

            PolygonMode mode = _wireframeEnabled ? PolygonMode.Line : PolygonMode.Fill;

            GL.PolygonMode(TriangleFace.FrontAndBack, mode);
        }
    }
}
