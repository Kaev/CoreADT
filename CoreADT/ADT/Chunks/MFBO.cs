using System;
using System.IO;

namespace CoreADT.ADT.Chunks
{
    public class MFBO : Chunk
    {

        public override uint ChunkSize { get; } = sizeof(uint) * 18;

        public Int16[,] Maximum { get; set; } = new Int16[2, 2];
        public Int16[,] Minimum { get; set; } = new Int16[2, 2];

        public MFBO(byte[] chunkBytes) : base(chunkBytes)
        {
            for(int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    Maximum[i, j] = ReadInt16();

            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    Minimum[i, j] = ReadInt16();
            Close();
        }

        public override byte[] GetChunkBytes()
        {
            using (var stream = new MemoryStream())
            {
                using (var writer = new BinaryWriter(stream))
                {
                    for (int i = 0; i < 3; i++)
                        for (int j = 0; j < 3; j++)
                            writer.Write(Maximum[i, j]);

                    for (int i = 0; i < 3; i++)
                        for (int j = 0; j < 3; j++)
                            writer.Write(Minimum[i, j]);
                }
                return stream.ToArray();
            }
        }

        public override byte[] GetChunkHeaderBytes()
        {
            using (var stream = new MemoryStream())
            {
                using (var writer = new BinaryWriter(stream))
                {
                    writer.Write(new char[] { 'O', 'B', 'F', 'M' });
                    writer.Write(ChunkSize);
                }
                return stream.ToArray();
            }
        }
    }
}
