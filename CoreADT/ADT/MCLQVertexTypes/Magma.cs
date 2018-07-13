using System;
using System.IO;

namespace CoreADT.ADT.MCLQVertexTypes
{
    public class Magma : VertexType
    {
        public override uint Size => sizeof(UInt16) * 2 + sizeof(float);

        public UInt16 S { get; set; }
        public UInt16 T { get; set; }
        public float Height { get; set; }

        public override void Read(BinaryReader reader)
        {
            S = reader.ReadUInt16();
            T = reader.ReadUInt16();
            Height = reader.ReadSingle();
        }

        public override void Write(BinaryWriter writer)
        {
            writer.Write(S);
            writer.Write(T);
            writer.Write(Height);
        }
    }
}
