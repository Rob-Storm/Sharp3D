using OpenTK.Graphics.OpenGL4;

namespace Sharp3D.Graphics
{
    public class IndexBuffer
    {
        private int _handle;
        private int _count;

        public IndexBuffer(uint[] data)
        {
            _count = data.Length;

            _handle = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, _handle);
            GL.BufferData(BufferTarget.ElementArrayBuffer, _count * sizeof(uint), data, BufferUsageHint.StaticDraw);

        }
        ~IndexBuffer()
        {
            GL.DeleteBuffer(_handle);
        }

        public int GetCount() => _count;

        public void Bind() => GL.BindBuffer(BufferTarget.ElementArrayBuffer, _handle);

        public void Unbind() =>  GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0);

    }
}
