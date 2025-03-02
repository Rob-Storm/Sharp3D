using OpenTK.Mathematics;

namespace Sharp3D.Data
{
    public struct Brush
    {
        public Vector3 Position { get; set; }
        public Vertex[] Vertices { get; private set; }
        public uint[] Indices{ get; private set; }

        public Brush(Vector3 position)
        {
            Position = position;

            Vertices = new Vertex[]
            {
                // front face
                new Vertex(new Vector3(-0.5f, -0.5f, -0.5f) + Position, Face.Back, new Vector2(0.0f, 0.0f)), // 0
                new Vertex(new Vector3(-0.5f,  0.5f, -0.5f) + Position, Face.Back, new Vector2(0.0f, 1.0f)), // 1
                new Vertex(new Vector3( 0.5f,  0.5f, -0.5f) + Position, Face.Back, new Vector2(1.0f, 1.0f)), // 2
                new Vertex(new Vector3( 0.5f, -0.5f, -0.5f) + Position, Face.Back, new Vector2(1.0f, 0.0f)), // 3

                // back face
                new Vertex(new Vector3(-0.5f, -0.5f,  0.5f) + Position, Face.Front, new Vector2(0.0f, 0.0f)), // 4
                new Vertex(new Vector3(-0.5f,  0.5f,  0.5f) + Position, Face.Front, new Vector2(0.0f, 1.0f)), // 5
                new Vertex(new Vector3( 0.5f,  0.5f,  0.5f) + Position, Face.Front, new Vector2(1.0f, 1.0f)), // 6
                new Vertex(new Vector3( 0.5f, -0.5f,  0.5f) + Position, Face.Front, new Vector2(1.0f, 0.0f)), // 7

                // right face 
                new Vertex(new Vector3(-0.5f, -0.5f, -0.5f) + Position, Face.Left, new Vector2(0.0f, 0.0f)), // 8
                new Vertex(new Vector3(-0.5f, -0.5f,  0.5f) + Position, Face.Left, new Vector2(1.0f, 0.0f)), // 9
                new Vertex(new Vector3(-0.5f,  0.5f,  0.5f) + Position, Face.Left, new Vector2(1.0f, 1.0f)), // 10
                new Vertex(new Vector3(-0.5f,  0.5f, -0.5f) + Position, Face.Left, new Vector2(0.0f, 1.0f)), // 11

                // left face
                new Vertex(new Vector3( 0.5f, -0.5f, -0.5f) + Position, Face.Right, new Vector2(0.0f, 0.0f)), // 12
                new Vertex(new Vector3( 0.5f, -0.5f,  0.5f) + Position, Face.Right, new Vector2(1.0f, 0.0f)), // 13
                new Vertex(new Vector3( 0.5f,  0.5f,  0.5f) + Position, Face.Right, new Vector2(1.0f, 1.0f)), // 14
                new Vertex(new Vector3( 0.5f,  0.5f, -0.5f) + Position, Face.Right, new Vector2(0.0f, 1.0f)), // 15

                // top face 
                new Vertex(new Vector3(-0.5f, -0.5f, -0.5f) + Position, Face.Bottom, new Vector2(0.0f, 1.0f)), // 16
                new Vertex(new Vector3( 0.5f, -0.5f, -0.5f) + Position, Face.Bottom, new Vector2(1.0f, 1.0f)), // 17
                new Vertex(new Vector3( 0.5f, -0.5f,  0.5f) + Position, Face.Bottom, new Vector2(1.0f, 0.0f)), // 18
                new Vertex(new Vector3(-0.5f, -0.5f,  0.5f) + Position, Face.Bottom, new Vector2(0.0f, 0.0f)), // 19

                // bottom face 
                new Vertex(new Vector3(-0.5f,  0.5f, -0.5f) + Position, Face.Top, new Vector2(0.0f, 1.0f)), // 20
                new Vertex(new Vector3( 0.5f,  0.5f, -0.5f) + Position, Face.Top, new Vector2(1.0f, 1.0f)), // 21
                new Vertex(new Vector3( 0.5f,  0.5f,  0.5f) + Position, Face.Top, new Vector2(1.0f, 0.0f)), // 22
                new Vertex(new Vector3(-0.5f,  0.5f,  0.5f) + Position, Face.Top, new Vector2(0.0f, 0.0f)), // 23
            };

            Indices = new uint[]
            {
                // Back face
                0, 1, 3,  1, 2, 3,

                // Front face
                4, 7, 5,  5, 7, 6,

                // Left face 
                8, 9, 11,  9, 10, 11,

                // Right face
                12, 15, 13,  13, 15, 14,

                // Bottom face 
                16, 17, 19,  17, 18, 19,

                // Top face
                20, 23, 21,  21, 23, 22,
            };
        }

        public struct Vertex
        {
            public Vector3 Position { get; }
            public Vector3 Normal { get; }
            public Vector2 TexCoord { get; }

            public Vertex(Vector3 position, Vector3 normal, Vector2 texCoord)
            {
                Position = position;
                Normal = normal;
                TexCoord = texCoord;
            }
        }


        public struct Face
        {
            public static Vector3 Front = -Vector3.UnitZ;
            public static Vector3 Back = Vector3.UnitZ;
            public static Vector3 Right = -Vector3.UnitX;
            public static Vector3 Left = Vector3.UnitX;
            public static Vector3 Top = -Vector3.UnitY;
            public static Vector3 Bottom = Vector3.UnitY;
        }
    }
}
