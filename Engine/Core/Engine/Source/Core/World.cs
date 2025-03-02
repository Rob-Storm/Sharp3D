using Sharp3D.Data;
using OpenTK.Mathematics;

namespace Sharp3D.Core
{
    public class World
    {
        public const int SIZE = 1;
        public Chunk[,] Chunks;

        public World()
        {
            Chunks = new Chunk[SIZE, SIZE];

            CreateChunks();
        }

        private void CreateChunks()
        {
            for (int w = 0; w < SIZE; w++)
            {
                for (int l = 0; l < SIZE; l++)
                {
                    Chunks[w, l] = new Chunk(new Vector3(w * Chunk.WIDTH, 0, l * Chunk.LENGTH));
                    Debug.Log($"Created chunk {w + l}", LogLevel.Info);
                }
            }
        }

        public (float[], uint[]) GetChunkData()
        {
            List<float> verts = new List<float>();
            List<uint> inds = new List<uint>();

            foreach(Chunk chunk in Chunks)
            {
                var blockData = chunk.GetBlockData().ToTuple();

                verts.AddRange(blockData.Item1);
                inds.AddRange(blockData.Item2);

                Debug.Log($"Chunk {chunk.Offset / Chunk.WIDTH},{chunk.Offset / Chunk.LENGTH} has {blockData.Item1.Count()} verices and {blockData.Item2.Count()} indices");
            }


            return (verts.ToArray(), inds.ToArray());
        }
    }
}
