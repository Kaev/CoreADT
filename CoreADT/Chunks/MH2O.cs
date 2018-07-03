using System;
using System.IO;

namespace CoreADT.Chunks
{
    public class MH2O : Chunk
    {
        public override uint ChunkSize { get; set; }

        #region MH2OHeaderData
        public uint OffsetInstances { get; set; }
        public uint LayerCount { get; set; }
        public uint OffsetAttributes { get; set; }
        #endregion

        #region MH2OInstances
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
        #endregion

        #region MH2OInstanceVertexData
        public float[,] HeightMap { get; set; } = new float[8, 8];
        public byte[,] DepthMap { get; set; } = new byte[8, 8];
        #endregion

        #region MH2OAttributes
        public byte[] Fishable { get; set; } = new byte[8];
        public byte[] Deep { get; set; } = new byte[8];
        #endregion

        public byte[] RenderBitmapBytes { get; set; }

        public MH2O(byte[] chunkBytes, long positionAfterLastMH2OHeaderData) : base(chunkBytes)
        {
            BaseStream.Position = positionAfterLastMH2OHeaderData;
            // MH2OHeaderData
            OffsetInstances = ReadUInt32();
            LayerCount = ReadUInt32();
            OffsetAttributes = ReadUInt32();

            if (LayerCount > 0)
            {
                // MH2OInstances
                BaseStream.Position = OffsetInstances;
                LiquidTypeId = ReadUInt16();
                LiquidVertexFormat = ReadUInt16();
                MinHeightLevel = ReadSingle();
                MaxHeightLevel = ReadSingle();
                OffsetX = ReadByte();
                OffsetY = ReadByte();
                Width = ReadByte();
                Height = ReadByte();
                OffsetExistsBitmap = ReadUInt32();
                OffsetVertexData = ReadUInt32();

                // MH2OInstanceVertexData
                if (OffsetVertexData != 0 && LiquidVertexFormat != 2)
                {
                    BaseStream.Position = OffsetVertexData;
                    for (byte y = OffsetY; y < Height + OffsetY; y++)
                        for (byte x = OffsetX; x < Width + OffsetX; x++)
                            HeightMap[y, x] = ReadSingle();

                    for (byte y = OffsetY; y < Height + OffsetY; y++)
                        for (byte x = OffsetX; x < Width + OffsetX; x++)
                            DepthMap[y, x] = ReadByte();
                }

                // MH2OAttributes
                if (OffsetAttributes != 0)
                {
                    BaseStream.Position = OffsetAttributes;
                    Fishable = ReadBytes(8);
                    Deep = ReadBytes(8);
                }

                RenderBitmapBytes = ReadBytes((Width * Height + 7) / 8);
            }
            Close();
        }

        public override byte[] GetChunkBytes()
        {
            throw new NotSupportedException();
        }

        public byte[] GetMH2OHeaderDataBytes()
        {
            using (var stream = new MemoryStream())
            {
                using (var writer = new BinaryWriter(stream))
                {
                    writer.Write(OffsetInstances);
                    writer.Write(LayerCount);
                    writer.Write(OffsetAttributes);
                }
                return stream.ToArray();
            }
        }

        public byte[] GetMH2ORenderMaskBytes()
        {
            using (var stream = new MemoryStream())
            {
                using (var writer = new BinaryWriter(stream))
                {
                    writer.Write(Fishable);
                    writer.Write(Deep);
                }
                return stream.ToArray();
            }
        }

        public byte[] GetMH2OHeightMapDataBytes()
        {
            using (var stream = new MemoryStream())
            {
                using (var writer = new BinaryWriter(stream))
                {
                    for (byte y = OffsetY; y < Height + OffsetY; y++)
                        for (byte x = OffsetX; x < Width + OffsetX; x++)
                            writer.Write(HeightMap[y, x]);

                    for (byte y = OffsetY; y < Height + OffsetY; y++)
                        for (byte x = OffsetX; x < Width + OffsetX; x++)
                            writer.Write(DepthMap[y, x]);
                }
                return stream.ToArray();
            }
        }

        public byte[] GetMH2OInstanceBytes()
        {
            using (var stream = new MemoryStream())
            {
                using (var writer = new BinaryWriter(stream))
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
                return stream.ToArray();
            }
        }

        public override byte[] GetChunkHeaderBytes()
        {
            using (var stream = new MemoryStream())
            {
                using (var writer = new BinaryWriter(stream))
                {
                    writer.Write(new char[] { 'O', '2', 'H', 'M' });
                    writer.Write(ChunkSize);
                }
                return stream.ToArray();
            }
        }
    }
}
