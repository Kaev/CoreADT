using System;
using System.IO;
using CoreADT.ADT.Flags;
using CoreADT.Helper;

namespace CoreADT.ADT.Doodads
{
    public class DoodadDefinition
    {

        public static uint Size = sizeof(uint) * 2 + sizeof(float) * 6 + sizeof(UInt16) * 2;

        public uint MMIDEntry { get; set; }
        public uint UniqueId { get; set; }
        public Vector3<float> Position { get; set; }
        public Vector3<float> Rotation { get; set; }
        /// <summary>
        /// 1024 = 1.0f
        /// </summary>
        public UInt16 Scale { get; set; }
        public MDDFFlags Flags { get; set; }

        public DoodadDefinition()
        { }

        public DoodadDefinition(BinaryReader reader)
        {
            MMIDEntry = reader.ReadUInt32();
            UniqueId = reader.ReadUInt32();
            Position = reader.ReadVector3Float();
            Rotation = reader.ReadVector3Float();
            Scale = reader.ReadUInt16();
            Flags = (MDDFFlags)reader.ReadUInt16();
        }

        public void Write(BinaryWriter writer)
        {
            writer.Write(MMIDEntry);
            writer.Write(UniqueId);
            writer.WriteVector3Float(Position);
            writer.WriteVector3Float(Rotation);
            writer.Write(Scale);
            writer.Write((UInt16)Flags);
        }
    }
}
