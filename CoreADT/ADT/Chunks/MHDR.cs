using System.IO;
using CoreADT.ADT.Flags;

namespace CoreADT.ADT.Chunks
{
    public class MHDR : Chunk
    {

        public override uint ChunkSize { get; } = sizeof(uint) * 15;

        public MHDRFlags Flags { get; set; }
        public uint OffsetMCIN { get; set; }
        public uint OffsetMTEX { get; set; }
        public uint OffsetMMDX { get; set; }
        public uint OffsetMMID { get; set; }
        public uint OffsetMWMO { get; set; }
        public uint OffsetMWID { get; set; }
        public uint OffsetMDDF { get; set; }
        public uint OffsetMODF { get; set; }
        public uint OffsetMFBO { get; set; }
        public uint OffsetMH2O { get; set; }
        public uint OffsetMTXF { get; set; }
        public uint[] Unknown { get; set; } = new uint[2];

        public MHDR(byte[] chunkBytes) : base(chunkBytes)
        {
            Flags = (MHDRFlags)ReadUInt32();
            OffsetMCIN = ReadUInt32();
            OffsetMTEX = ReadUInt32();
            OffsetMMDX = ReadUInt32();
            OffsetMMID = ReadUInt32();
            OffsetMWMO = ReadUInt32();
            OffsetMWID = ReadUInt32();
            OffsetMDDF = ReadUInt32();
            OffsetMODF = ReadUInt32();
            OffsetMFBO = ReadUInt32();
            OffsetMH2O = ReadUInt32();
            OffsetMTXF = ReadUInt32();
            for (int i = 0; i < 3; i++)
                Unknown[i] = ReadUInt32();
            Close();
        }

        public override byte[] GetChunkBytes()
        {
            using (var stream = new MemoryStream())
            {
                using (var writer = new BinaryWriter(stream))
                {
                    writer.Write((uint)Flags);
                    writer.Write(OffsetMCIN);
                    writer.Write(OffsetMTEX);
                    writer.Write(OffsetMMDX);
                    writer.Write(OffsetMMID);
                    writer.Write(OffsetMWMO);
                    writer.Write(OffsetMWID);
                    writer.Write(OffsetMDDF);
                    writer.Write(OffsetMODF);
                    writer.Write(OffsetMFBO);
                    writer.Write(OffsetMH2O);
                    writer.Write(OffsetMTXF);
                    for(int i = 0; i < 3; i++)
                        writer.Write(Unknown[i]);
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
                    writer.Write(new char[] { 'R', 'D', 'H', 'M' });
                    writer.Write(ChunkSize);
                }
                return stream.ToArray();
            }
        }
    }
}
