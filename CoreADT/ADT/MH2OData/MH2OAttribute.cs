using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CoreADT.ADT.MH2OData
{
    public class MH2OAttribute
    {

        public static uint Size => sizeof(byte) * 16;

        public byte[] Fishable { get; set; } = new byte[8];
        public byte[] Deep { get; set; } = new byte[8];

        public MH2OAttribute() { }

        public MH2OAttribute(BinaryReader reader)
        {
            Fishable = reader.ReadBytes(8);
            Deep = reader.ReadBytes(8);
        }

        public void Write(BinaryWriter writer)
        {
            writer.Write(Fishable);
            writer.Write(Deep);
        }

    }
}
