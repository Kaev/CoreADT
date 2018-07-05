using System.IO;
using CoreADT.WDT.Flags;

namespace CoreADT.WDT.Chunks
{
    public class MPHD : Chunk
    {
        public override uint ChunkSize { get; set; } = sizeof(uint) * 8;

        public MPHDFlags Flags { get; set; }
        public uint Something { get; set; }
        public uint[] Unused { get; set; } = new uint[6];

        public MPHD (byte[] chunkBytes) : base(chunkBytes)
        {
            Flags = (MPHDFlags)ReadUInt32();
            Something = ReadUInt32();
            for (int i = 0; i < 7; i++)
                Unused[i] = ReadUInt32();
        }

        public override byte[] GetChunkBytes()
        {
            using (var stream = new MemoryStream())
            {
                using (var writer = new BinaryWriter(stream))
                {
                    writer.Write((uint)Flags);
                    writer.Write(Something);
                    for (int i = 0; i < 7; i++)
                        writer.Write(Unused[i]);
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
                    writer.Write(new char[] { 'D', 'H', 'P', 'M' });
                    writer.Write(ChunkSize);
                }
                return stream.ToArray();
            }
        }
    }
}
