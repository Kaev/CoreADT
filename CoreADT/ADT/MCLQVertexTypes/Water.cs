using System.IO;

namespace CoreADT.MCLQVertexTypes
{
    public class Water : VertexType
    {
        public byte Depth { get; set; }
        public byte Flow0Pct { get; set; }
        public byte Flow1Pct { get; set; }
        public byte Filler { get; set; }
        public float Height { get; set; }

        public override void Read(BinaryReader reader)
        {
            Depth = reader.ReadByte();
            Flow0Pct = reader.ReadByte();
            Flow1Pct = reader.ReadByte();
            Filler = reader.ReadByte();
            Height = reader.ReadSingle();
        }

        public override void Write(BinaryWriter writer)
        {
            writer.Write(Depth);
            writer.Write(Flow0Pct);
            writer.Write(Flow1Pct);
            writer.Write(Filler);
            writer.Write(Height);
        }
    }
}
}
