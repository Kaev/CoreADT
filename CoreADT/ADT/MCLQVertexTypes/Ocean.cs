using System.IO;

namespace CoreADT.ADT.MCLQVertexTypes
{
    public class Ocean : VertexType
    {
        public byte Depth { get; set; }
        public byte Foam { get; set; }
        public byte Wet { get; set; }
        public byte Filler { get; set; }

        public override void Read(BinaryReader reader)
        {
            Depth = reader.ReadByte();
            Foam = reader.ReadByte();
            Wet = reader.ReadByte();
            Filler = reader.ReadByte();
        }

        public override void Write(BinaryWriter writer)
        {
            writer.Write(Depth);
            writer.Write(Foam);
            writer.Write(Wet);
            writer.Write(Filler);
        }
    }
}
