﻿using System;
using System.IO;

namespace CoreADT.ADT.MH2OData
{
    public class MH2OInstance
    {

        internal long OffsetExistsBitmapPosition { get; set; }
        internal long OffsetVertexDataPosition { get; set; }

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
        public byte[] RenderBitmapBytes { get; set; }
        public MH2OInstanceVertexData VertexData { get; set; }

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

            long positionAfterInstance = reader.BaseStream.Position;

            if (OffsetExistsBitmap > 0)
            {
                reader.BaseStream.Position = OffsetExistsBitmap;
                RenderBitmapBytes = reader.ReadBytes((Width * Height + 7) / 8);
            }

            if (OffsetVertexData > 0)
            {
                reader.BaseStream.Position = OffsetVertexData;
                VertexData = new MH2OInstanceVertexData(reader, this);
            }

            reader.BaseStream.Position = positionAfterInstance;
        }

        public void Write(BinaryWriter writer)
        {
            writer.Write(LiquidTypeId);
            // Write 2 vertex data can be ommitted - TODO: When can we omit this?
            /*if (OffsetVertexData == 0 && LiquidTypeId != 2)
                writer.Write(2);
            else*/
            writer.Write(LiquidVertexFormat);
            writer.Write(MinHeightLevel);
            writer.Write(MaxHeightLevel);
            writer.Write(OffsetX);
            writer.Write(OffsetY);
            writer.Write(Width);
            writer.Write(Height);
            // We will write the Offset later in MH2O.Write
            OffsetExistsBitmapPosition = writer.BaseStream.Position;
            writer.Write(0);
            // We will write the Offset later in MH2O.Write
            OffsetVertexDataPosition = writer.BaseStream.Position;
            writer.Write(0);
        }
    }
}
