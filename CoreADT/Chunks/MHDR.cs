using System.Collections.Generic;
using System.IO;
using CoreADT.Helper;

namespace CoreADT.Chunks
{
    public class MHDR : Chunk
    {

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
        public List<uint> Unknown { get; set; }

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
                Unknown.Add(ReadUInt32());
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
                    for(int i = 0; i < Unknown.Count; i++)
                        writer.Write(Unknown[i]);
                }
                return stream.ToArray();
            }
        }
    }
}
