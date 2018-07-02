using System.IO;

namespace CoreADT.Chunks
{
    public class MVER : Chunk
    {

        public uint Version { get; set; }

        public MVER(byte[] chunkBytes) : base (chunkBytes)
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
    }
}
