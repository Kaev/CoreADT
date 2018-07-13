using System.IO;
using CoreADT.ADT.MCINData;

namespace CoreADT.ADT.Chunks
{
    public class MCIN : Chunk
    {

        public override uint ChunkSize { get; } = MCINEntry.Size * 256;

        public MCINEntry[] Entries { get; set; } = new MCINEntry[256];

        public MCIN(byte[] chunkBytes) : base(chunkBytes)
        {
            for (int i = 0; i < 256; i++)
                Entries[i] = new MCINEntry(this);
            Close();
        }

        public override byte[] GetChunkBytes()
        {
            using (var stream = new MemoryStream())
            {
                using (var writer = new BinaryWriter(stream))
                {
                    for (int i = 0; i < 256; i++)
                        Entries[i].Write(writer);
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
