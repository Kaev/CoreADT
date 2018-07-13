using System.IO;
using CoreADT.ADT.MCCVData;

namespace CoreADT.ADT.Chunks.Subchunks
{
    public class MCCV : Chunk
    {

        public override uint ChunkSize { get; } = MCCVEntry.Size * 145;

        public MCCVEntry[] Entries { get; set; } = new MCCVEntry[145];

        public MCCV(byte[] chunkBytes) : base(chunkBytes)
        {
            for (int i = 0; i < 145; i++)
                Entries[i] = new MCCVEntry(this);
        }

        
        public override byte[] GetChunkBytes()
        {
            using (var stream = new MemoryStream())
            {
                using (var writer = new BinaryWriter(stream))
                {
                    for (int i = 0; i < 145; i++)
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
                    writer.Write(new char[] { 'V', 'C', 'C', 'M' });
                    writer.Write(ChunkSize);
                }
                return stream.ToArray();
            }
        }
    }
}
