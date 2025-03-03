using OpenTK.Mathematics;
using Sharp3D.Data;

namespace Sharp3D.Core
{
    public class World
    {
        public List<Brush> Brushes;
        public World()
        {
            Brushes = new List<Brush>();
            CreateBrushes();
        }

        private void CreateBrushes()
        {
            Brushes.Add(new Brush(new Vector3(0,0,0), new Quaternion(0,0,0), new Vector3(1,10,10)));
        }

        public (float[], uint[]) GetBrushData()
        {
            List<float> allVertices = new List<float>();
            List<uint> allIndices = new List<uint>();

            uint vertexOffset = 0;

            foreach (var brush in Brushes)
            {
                foreach (var vertex in brush.Vertices)
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

                foreach (var index in brush.Indices)
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
