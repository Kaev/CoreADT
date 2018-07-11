using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CoreADT.ADT.MH2OData
{
    public class MH2OHeader
    {

        public static uint Size => sizeof(uint) * 3;

        public uint OffsetInstances { get; set; }
        public uint LayerCount { get; set; }
        public uint OffsetAttributes { get; set; }

        public MH2OHeader() { }

        public MH2OHeader(BinaryReader reader)
        {
            OffsetInstances = reader.ReadUInt32();
            LayerCount = reader.ReadUInt32();
            OffsetAttributes = reader.ReadUInt32();
        }

        public void Write(BinaryWriter writer)
        {
            writer.Write(OffsetInstances);
            writer.Write(LayerCount);
            writer.Write(OffsetAttributes);
        }
    }
}
