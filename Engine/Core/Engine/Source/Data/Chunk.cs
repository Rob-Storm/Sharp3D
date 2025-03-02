using Sharp3D.Core;
using OpenTK.Mathematics;

namespace Sharp3D.Data
{
    public class Chunk
    {
        public const int WIDTH = 16, HEIGHT = 16, LENGTH = 16;

        public Vector3 Offset { get; private set; }

        public Brush[,,] Blocks;
        public Chunk(Vector3 offset)
        {
            Offset = offset;
            Blocks = new Brush[WIDTH, HEIGHT, LENGTH];

            CreateBlocks();
        }

        private void CreateBlocks()
        {
            for(int w = 0; w < WIDTH; w++)
            {
                for (int h = 0; h < HEIGHT; h++)
                {
                    for (int l = 0; l < LENGTH; l++)
                    {
                        Blocks[w, h, l] = new Brush(new Vector3(w + Offset.X, h + Offset.Y, l + Offset.Z));

                        Debug.Log($"Created Block {Blocks[w,h,l].Position}", LogLevel.Info);
                    }
                }
            }
        }

        public (float[], uint[]) GetBlockData()
        {
            List<float> allVertices = new List<float>();
            List<uint> allIndices = new List<uint>();

            uint vertexOffset = 0;

            foreach (var block in Blocks)
            {
                foreach (var vertex in block.Vertices)
                {
                    allVertices.Add(vertex.Position.X);
                    allVertices.Add(vertex.Position.Y);
                    allVertices.Add(vertex.Position.Z);

                    allVertices.Add(vertex.TexCoord.X);
                    allVertices.Add(vertex.TexCoord.Y);

                    allVertices.Add(vertex.Normal.X);
                    allVertices.Add(vertex.Normal.Y);
                    allVertices.Add(vertex.Normal.Z);
                }

                foreach (var index in block.Indices)
                {
                    allIndices.Add(index + vertexOffset);
                }

                vertexOffset += 24;
            }

            float[] vertexArrayData = allVertices.ToArray();
            uint[] indexArrayData = allIndices.ToArray();

            return (vertexArrayData, indexArrayData);
        }
    }
}
