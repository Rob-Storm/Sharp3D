using OpenTK.Graphics.OpenGL4;

namespace Sharp3D.Graphics
{
    public class VertexBufferLayout
    {
        private List<VertexBufferElement> _elements = new List<VertexBufferElement>();
        private int _stride;

        public VertexBufferLayout()
        {
            
        }

        public void Add<T>(int count)
        {
            switch (Type.GetTypeCode(typeof(T)))
            {
                case TypeCode.Single:
                    _elements.Add(new VertexBufferElement() { Count = count, Type = VertexAttribPointerType.Float, Normalized = false });
                    _stride += sizeof(float) * count;
                    break;
                case TypeCode.Int32:
                    _elements.Add(new VertexBufferElement() { Count = count, Type = VertexAttribPointerType.Int, Normalized = false });
                    _stride += sizeof(int) * count;
                    break;
                case TypeCode.Char:
                    _elements.Add(new VertexBufferElement() { Count = count, Type = VertexAttribPointerType.Byte, Normalized = true });
                    _stride += sizeof(byte) * count;
                    break;
            }
        }

        public List<VertexBufferElement> GetElements() => _elements;
        public int GetStride() => _stride;
    }

    public struct VertexBufferElement
    {
        public int Count { get; set; }
        public VertexAttribPointerType Type { get; set; }
        public bool Normalized { get; set; }

        public static int GetSizeOfType(VertexAttribPointerType type)
        {
            switch (type)
            {
                case VertexAttribPointerType.Float:
                    return sizeof(float);
                case VertexAttribPointerType.Int:
                    return sizeof(int);
                case VertexAttribPointerType.Byte:
                    return sizeof(byte);
                default:
                    return 0;
            }
        }


    };
}
