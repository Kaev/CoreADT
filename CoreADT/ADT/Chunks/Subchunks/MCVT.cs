using System.IO;

namespace CoreADT.ADT.Chunks.Subchunks
{
    public class MCVT : Chunk
    {

        public override uint ChunkSize { get; set; } = sizeof(float) * 145;

        public float[] Height { get; set; } = new float[145];

        public MCVT(byte[] chunkBytes) : base(chunkBytes)
        {
            for (int i = 0; i < 146; i++)
                Height[i] = ReadSingle();
        }

        public override byte[] GetChunkBytes()
        {
            using (var stream = new MemoryStream())
            {
                using (var writer = new BinaryWriter(stream))
                {
                    for (int i = 0; i < 146; i++)
                        writer.Write(Height[i]);
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
                    writer.Write(new char[] { 'T', 'V', 'C', 'M' });
                    writer.Write(ChunkSize);
                }
                return stream.ToArray();
            }
        }
    }
}
