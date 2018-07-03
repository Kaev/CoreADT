using System.IO;

namespace CoreADT.Chunks
{
    class MCIN : Chunk
    {

        public override uint ChunkSize { get; set; } = sizeof(uint) * 4 * 256;

        public uint OffsetMCNK { get; set; }
        public uint Size { get; set; }
        /// <summary>
        /// Always 0, only set in the client
        /// </summary>
        public uint Flags { get; set; } = 0;
        /// <summary>
        /// Always 0, only set in the client
        /// </summary>
        public uint AsyncId { get; set; } = 0;

        public MCIN(byte[] chunkBytes) : base(chunkBytes)
        {
            OffsetMCNK = ReadUInt32();
            Size = ReadUInt32();
            Flags = ReadUInt32();
            AsyncId = ReadUInt32();
            Close();
        }

        public override byte[] GetChunkBytes()
        {
            using (var stream = new MemoryStream())
            {
                using (var writer = new BinaryWriter(stream))
                {
                    writer.Write(OffsetMCNK);
                    writer.Write(Size);
                    writer.Write(Flags);
                    writer.Write(AsyncId);
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
                    writer.Write(new char[] { 'N', 'I', 'C', 'M' });
                    writer.Write(ChunkSize);
                }
                return stream.ToArray();
            }
        }
    }
}
