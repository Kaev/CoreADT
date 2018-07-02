using System.Collections.Generic;
using System.IO;

namespace CoreADT.Chunks
{
    class MWID : Chunk
    {

        public List<uint> Offsets { get; set; } = new List<uint>();

        public MWID(byte[] chunkBytes) : base(chunkBytes)
        {
            if (chunkBytes.Length > 0)
                for (int i = 0; i < chunkBytes.Length / 4; i++)
                    Offsets.Add(ReadUInt32());
        }

        public override byte[] GetChunkBytes()
        {
            using (var stream = new MemoryStream())
            {
                using (var writer = new BinaryWriter(stream))
                {
                    foreach (var offset in Offsets)
                        writer.Write(offset);
                }
                return stream.ToArray();
            }
        }
    }
}
