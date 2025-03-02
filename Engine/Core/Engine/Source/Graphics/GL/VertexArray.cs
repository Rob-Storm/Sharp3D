using OpenTK.Graphics.OpenGL4;
using Sharp3D.Core;

namespace Sharp3D.Graphics
{
    public class VertexArray
    {
        public int Handle { get; set; }

        public VertexArray()
        {
            Handle = GL.GenVertexArray();
            Debug.Log("Creating Vertex Array Object", LogLevel.Engine);
        }
        ~VertexArray()
        {
            GL.DeleteVertexArray(Handle);
        }

        public void Bind()
        {
            GL.BindVertexArray(Handle);

        }

        public void Unbind()
        {
            GL.BindVertexArray(0);

        }

        public void AddBuffer(VertexBuffer vertexBuffer, VertexBufferLayout layout )
        {
            Bind();
            vertexBuffer.Bind();
            var elements = layout.GetElements();
            int offset = 0;

            for(int i = 0; i < elements.Count; i++)
            {
                GL.EnableVertexAttribArray(i);
                GL.VertexAttribPointer(i, elements[i].Count, elements[i].Type, elements[i].Normalized, layout.GetStride(), offset);

                offset += elements[i].Count * VertexBufferElement.GetSizeOfType(elements[i].Type);
            }

        }
    }

}
