using System.IO;
using CoreADT.Flags;

namespace CoreADT.Chunks
{
    class MTXF : Chunk
    {

        public override uint ChunkSize { get; set; } = sizeof(uint);

        public MTXFFlags Flags { get; set; }

        public MTXF(byte[] chunkBytes) : base(chunkBytes)
        {
            Flags = (MTXFFlags)ReadUInt32();
            Close();
        }

        public override byte[] GetChunkBytes()
        {
            using (var stream = new MemoryStream())
            {
                using (var writer = new BinaryWriter(stream))
                {
                    writer.Write((uint)Flags);
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
                    writer.Write(new char[] { 'F', 'X', 'T', 'M' });
                    writer.Write(ChunkSize);
                }
                return stream.ToArray();
            }
        }
    }
}
