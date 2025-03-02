using OpenTK.Graphics.OpenGL4;

namespace Sharp3D.Graphics
{
    public class VertexBuffer
    {
        private int _handle;

        public VertexBuffer(float[] data)
        {
            _handle = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, _handle);
            GL.BufferData(BufferTarget.ArrayBuffer, data.Length * sizeof(float), data, BufferUsageHint.StaticDraw);
        }
        ~VertexBuffer()
        {
            GL.DeleteBuffer(_handle);
        }

        public void Bind() => GL.BindBuffer(BufferTarget.ArrayBuffer, _handle);

        public void Unbind() =>  GL.BindBuffer(BufferTarget.ArrayBuffer, 0);

    }
}
