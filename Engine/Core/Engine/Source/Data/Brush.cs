using OpenTK.Mathematics;

namespace Sharp3D.Data
{
    public struct Brush
    {
        public Vector3 Position { get; set; }
        public Quaternion Rotation { get; set; }
        public Vector3 Scale { get; set; }
        public Vertex[] Vertices { get; private set; }
        public uint[] Indices{ get; private set; }

        public Brush(Vector3 position, Quaternion rotation, Vector3 scale)
        {
            Position = position;
            Rotation = rotation;
            Scale = scale;

            Matrix4 transform = Matrix4.CreateScale(Scale) *
                                Matrix4.CreateFromQuaternion(Rotation) *
                                Matrix4.CreateTranslation(Position);

            Vector3[] baseVertices = new Vector3[]
            {
                // front face
                new Vector3(-0.5f, -0.5f, -0.5f),
                new Vector3(-0.5f,  0.5f, -0.5f),
                new Vector3( 0.5f,  0.5f, -0.5f),
                new Vector3( 0.5f, -0.5f, -0.5f),
                                                           
                // back face                               
                new Vector3(-0.5f, -0.5f,  0.5f),
                new Vector3(-0.5f,  0.5f,  0.5f),
                new Vector3( 0.5f,  0.5f,  0.5f),
                new Vector3( 0.5f, -0.5f,  0.5f),
                                                           
                // right face                              
                new Vector3(-0.5f, -0.5f, -0.5f),
                new Vector3(-0.5f, -0.5f,  0.5f),
                new Vector3(-0.5f,  0.5f,  0.5f),
                new Vector3(-0.5f,  0.5f, -0.5f),
                                                           
                // left face                               
                new Vector3( 0.5f, -0.5f, -0.5f),
                new Vector3( 0.5f, -0.5f,  0.5f),
                new Vector3( 0.5f,  0.5f,  0.5f),
                new Vector3( 0.5f,  0.5f, -0.5f),
                                                           
                // top face                                
                new Vector3(-0.5f, -0.5f, -0.5f),
                new Vector3( 0.5f, -0.5f, -0.5f),
                new Vector3( 0.5f, -0.5f,  0.5f),
                new Vector3(-0.5f, -0.5f,  0.5f),
                                                           
                // bottom face                             
                new Vector3(-0.5f,  0.5f, -0.5f),
                new Vector3( 0.5f,  0.5f, -0.5f),
                new Vector3( 0.5f,  0.5f,  0.5f),
                new Vector3(-0.5f,  0.5f,  0.5f),
            };

            Vector3[] transformedVertices = new Vector3[baseVertices.Length];
            for (int i = 0; i < baseVertices.Length; i++)
            {
                transformedVertices[i] = Vector3.TransformPosition(baseVertices[i], transform);
            }

            Vertices = new Vertex[]
            {
                new Vertex(transformedVertices[0], Face.Back, new Vector2(0.0f, 0.0f)), // 0
                new Vertex(transformedVertices[1], Face.Back, new Vector2(0.0f, 1.0f)), // 1
                new Vertex(transformedVertices[2], Face.Back, new Vector2(1.0f, 1.0f)), // 2
                new Vertex(transformedVertices[3], Face.Back, new Vector2(1.0f, 0.0f)), // 3

                new Vertex(transformedVertices[4], Face.Front, new Vector2(0.0f, 0.0f)), // 4
                new Vertex(transformedVertices[5], Face.Front, new Vector2(0.0f, 1.0f)), // 5
                new Vertex(transformedVertices[6], Face.Front, new Vector2(1.0f, 1.0f)), // 6
                new Vertex(transformedVertices[7], Face.Front, new Vector2(1.0f, 0.0f)), // 7

                new Vertex(transformedVertices[8], Face.Left, new Vector2(0.0f, 0.0f)), // 8
                new Vertex(transformedVertices[9], Face.Left, new Vector2(0.0f, 1.0f)), // 9
                new Vertex(transformedVertices[10], Face.Left, new Vector2(1.0f, 1.0f)), // 10
                new Vertex(transformedVertices[11], Face.Left, new Vector2(1.0f, 0.0f)), // 11

                new Vertex(transformedVertices[12], Face.Right, new Vector2(0.0f, 0.0f)), // 12
                new Vertex(transformedVertices[13], Face.Right, new Vector2(0.0f, 1.0f)), // 13
                new Vertex(transformedVertices[14], Face.Right, new Vector2(1.0f, 1.0f)), // 14
                new Vertex(transformedVertices[15], Face.Right, new Vector2(1.0f, 0.0f)), // 15

                new Vertex(transformedVertices[16], Face.Bottom, new Vector2(0.0f, 0.0f)), // 16
                new Vertex(transformedVertices[17], Face.Bottom, new Vector2(0.0f, 1.0f)), // 17
                new Vertex(transformedVertices[18], Face.Bottom, new Vector2(1.0f, 1.0f)), // 18
                new Vertex(transformedVertices[19], Face.Bottom, new Vector2(1.0f, 0.0f)), // 19

                new Vertex(transformedVertices[20], Face.Top, new Vector2(0.0f, 0.0f)), // 20
                new Vertex(transformedVertices[21], Face.Top, new Vector2(0.0f, 1.0f)), // 21
                new Vertex(transformedVertices[22], Face.Top, new Vector2(1.0f, 1.0f)), // 22
                new Vertex(transformedVertices[23], Face.Top, new Vector2(1.0f, 0.0f)), // 23
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
