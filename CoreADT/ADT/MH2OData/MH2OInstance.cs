using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CoreADT.ADT.MH2OData
{
    public class MH2OInstance
    {

        public static uint Size => sizeof(UInt16) * 2 + sizeof(float) * 2 + sizeof(byte) * 4 + sizeof(uint) * 2;

        public UInt16 LiquidTypeId { get; set; }
        public UInt16 LiquidVertexFormat { get; set; }
        public float MinHeightLevel { get; set; }
        public float MaxHeightLevel { get; set; }
        public byte OffsetX { get; set; }
        public byte OffsetY { get; set; }
        public byte Width { get; set; }
        public byte Height { get; set; }
        public uint OffsetExistsBitmap { get; set; }
        public uint OffsetVertexData { get; set; }

        public MH2OInstance() { }

        public MH2OInstance(BinaryReader reader)
        {
            LiquidTypeId = reader.ReadUInt16();
            LiquidVertexFormat = reader.ReadUInt16();
            MinHeightLevel = reader.ReadSingle();
            MaxHeightLevel = reader.ReadSingle();
            OffsetX = reader.ReadByte();
            OffsetY = reader.ReadByte();
            Width = reader.ReadByte();
            Height = reader.ReadByte();
            OffsetExistsBitmap = reader.ReadUInt32();
            OffsetVertexData = reader.ReadUInt32();
        }

        public void Write(BinaryWriter writer)
        {
            writer.Write(LiquidTypeId);
            writer.Write(LiquidVertexFormat);
            writer.Write(MinHeightLevel);
            writer.Write(MaxHeightLevel);
            writer.Write(OffsetX);
            writer.Write(OffsetY);
            writer.Write(Width);
            writer.Write(Height);
            writer.Write(OffsetExistsBitmap);
            writer.Write(OffsetVertexData);
        }
    }
}
