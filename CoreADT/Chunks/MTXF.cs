using System.IO;
using CoreADT.Helper;

namespace CoreADT.Chunks
{
    class MTXF : Chunk
    {

        public MTXFFlags Flags { get; set; }

        public MTXF(byte[] chunkBytes) : base(chunkBytes)
        {
            Flags = (MTXFFlags)ReadUInt32();
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
    }
}
