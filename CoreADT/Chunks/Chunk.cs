using System.IO;

namespace CoreADT.Chunks
{
    public abstract class Chunk : BinaryReader
    {
        public Chunk(byte[] chunkBytes) : base(new MemoryStream(chunkBytes))
        {
        }

        public abstract byte[] GetChunkBytes();
    }
}
