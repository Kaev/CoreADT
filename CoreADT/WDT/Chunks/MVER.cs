using System.IO;

namespace CoreADT.WDT.Chunks
{
    public class MVER : Chunk
    {

        public override uint ChunkSize { get; set; } = sizeof(uint);

        public uint Version { get; set; }

        public MVER(byte[] chunkBytes) : base(chunkBytes)
        {
            Version = ReadUInt32();
            Close();
        }

        public override byte[] GetChunkBytes()
        {
            using (var stream = new MemoryStream())
            {
                using (var writer = new BinaryWriter(stream))
                {
                    writer.Write(Version);
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
                    writer.Write(new char[] { 'R', 'E', 'V', 'M' });
                    writer.Write(ChunkSize);
                }
                return stream.ToArray();
            }
        }
    }
}
