using System.IO;
using CoreADT.ADT.Flags;

namespace CoreADT.ADT.MCLYData
{
    public class MCLYLayer
    {

        public static uint Size => sizeof(uint) * 4;

        public uint TextureId { get; set; }
        public MCLYFlags Flags { get; set; }
        public uint OffsetInMCAL { get; set; }
        public uint EffectId { get; set; }

        public MCLYLayer() { }

        public MCLYLayer(BinaryReader reader)
        {
            TextureId = reader.ReadUInt32();
            Flags = (MCLYFlags)reader.ReadUInt32();
            OffsetInMCAL = reader.ReadUInt32();
            EffectId = reader.ReadUInt32();
        }

        public void Write(BinaryWriter writer)
        {
            writer.Write(TextureId);
            writer.Write((uint)Flags);
            writer.Write(OffsetInMCAL);
            writer.Write(EffectId);
        }
    }
}
