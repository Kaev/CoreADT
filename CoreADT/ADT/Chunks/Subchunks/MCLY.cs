using System.IO;
using CoreADT.ADT.Flags;

namespace CoreADT.ADT.Chunks.Subchunks
{
    public class MCLY : Chunk
    {

        public override uint ChunkSize { get; set; }

        public uint TextureId { get; set; }
        public MCLYFlags Flags { get; set; }
        public uint OffsetInMCAL { get; set; }
        public uint EffectId { get; set; }


        public MCLY(byte[] chunkBytes) : base(chunkBytes)
        {
            TextureId = ReadUInt32();
            Flags = (MCLYFlags)ReadUInt32();
            OffsetInMCAL = ReadUInt32();
            EffectId = ReadUInt32();
        }

        public override byte[] GetChunkBytes()
        {
            using (var stream = new MemoryStream())
            {
                using (var writer = new BinaryWriter(stream))
                {
                    writer.Write(TextureId);
                    writer.Write((uint)Flags);
                    writer.Write(OffsetInMCAL);
                    writer.Write(EffectId);
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
                    writer.Write(new char[] { 'Y', 'L', 'C', 'M' });
                    writer.Write(ChunkSize);
                }
                return stream.ToArray();
            }
        }
    }
}
