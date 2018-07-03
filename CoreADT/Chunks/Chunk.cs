using System.IO;

namespace CoreADT.Chunks
{
    public abstract class Chunk : BinaryReader
    {

        public abstract uint ChunkSize { get; set; }

        public Chunk(byte[] chunkBytes) : base(new MemoryStream(chunkBytes))
        {
        }

        public abstract byte[] GetChunkBytes();

        public abstract byte[] GetChunkHeaderBytes();
    }
}
