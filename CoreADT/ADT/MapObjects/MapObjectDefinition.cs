using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using CoreADT.ADT.Flags;
using CoreADT.Helper;

namespace CoreADT.ADT.MapObjects
{
    public class MapObjectDefinition
    {

        public static uint Size => sizeof(uint) * 2 + sizeof(float) * 12 + sizeof(UInt16) * 4;

        public uint MWIDEntry { get; set; }
        public uint UniqueId { get; set; }
        public Vector3<float> Position { get; set; }
        public Vector3<float> Rotation { get; set; }
        /// <summary>
        /// Position + Bounding Box
        /// </summary>
        public Vector3<float> LowerBounds { get; set; }
        /// <summary>
        /// Position + Bounding Box
        /// </summary>
        public Vector3<float> UpperBounds { get; set; }
        public MODFFlags Flags { get; set; }
        public UInt16 DoodadSet { get; set; }
        /// <summary>
        /// Used for renaming
        /// </summary>
        public UInt16 NameSet { get; set; }
        public UInt16 Padding { get; set; }

        public MapObjectDefinition() { }

        public MapObjectDefinition(BinaryReader reader)
        {
            MWIDEntry = reader.ReadUInt32();
            UniqueId = reader.ReadUInt32();
            Position = reader.ReadVector3Float();
            Rotation = reader.ReadVector3Float();
            LowerBounds = reader.ReadVector3Float();
            UpperBounds = reader.ReadVector3Float();
            Flags = (MODFFlags)reader.ReadUInt16();
            DoodadSet = reader.ReadUInt16();
            NameSet = reader.ReadUInt16();
            Padding = reader.ReadUInt16();
        }

        public void Write(BinaryWriter writer)
        {
            writer.Write(MWIDEntry);
            writer.Write(UniqueId);
            writer.WriteVector3Float(Position);
            writer.WriteVector3Float(Rotation);
            writer.WriteVector3Float(LowerBounds);
            writer.WriteVector3Float(UpperBounds);
            writer.Write((UInt16)Flags);
            writer.Write(DoodadSet);
            writer.Write(NameSet);
            writer.Write(Padding);
        }
    }
}
