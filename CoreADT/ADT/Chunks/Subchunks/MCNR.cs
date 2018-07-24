using System.IO;
using CoreADT.ADT.MCNRData;

namespace CoreADT.ADT.Chunks.Subchunks
{
    public class MCNR : Chunk
    {

        public override uint ChunkSize { get; } = MCNREntry.Size * 145 + sizeof(byte) * 13;

        public MCNREntry[] Entries = new MCNREntry[145];
        public byte[] Padding { get; set; } = new byte[13];


        public MCNR(byte[] chunkBytes) : base(chunkBytes)
        {
            for (int i = 0; i < 145; i++)
                Entries[i] = new MCNREntry(this);
            Padding = ReadBytes(13);
            Close();
        }

        public override byte[] GetChunkBytes()
        {
            using (var stream = new MemoryStream())
            {
                using (var writer = new BinaryWriter(stream))
                {
                    for (int i = 0; i < 145; i++)
                        Entries[i].Write(writer);
                    writer.Write(Padding);
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
                    writer.Write(new char[] { 'R', 'N', 'C', 'M' });
                    writer.Write(ChunkSize);
                }
                return stream.ToArray();
            }
        }
    }
}
