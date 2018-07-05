using System.IO;

namespace CoreADT.Chunks.Subchunks
{
    public class MCCV : Chunk
    {

        public override uint ChunkSize { get; set; } = sizeof(byte) * 4 * 145;

        public byte Red { get; set; }
        public byte Green { get; set; }
        public byte Blue { get; set; }
        public byte Alpha { get; set; }

        public MCCV(byte[] chunkBytes) : base(chunkBytes)
        {
            Red = ReadByte();
            Green = ReadByte();
            Blue = ReadByte();
            Alpha = ReadByte();
        }

        
        public override byte[] GetChunkBytes()
        {
            using (var stream = new MemoryStream())
            {
                using (var writer = new BinaryWriter(stream))
                {
                    writer.Write(Red);
                    writer.Write(Green);
                    writer.Write(Blue);
                    writer.Write(Alpha);
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
