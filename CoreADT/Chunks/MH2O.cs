using System;
using System.Collections.Generic;
using System.Text;

namespace CoreADT.Chunks
{
    public class MH2O : Chunk
    {

        #region MH2OHeader
        public uint OffsetInformation { get; set; }
        public uint LayerCount { get; set; }
        public uint OffsetRenderMask { get; set; }
        #endregion

        #region MH2OInformation
        public UInt16 LiquidTypeId { get; set; }
        public UInt16 Flags { get; set; }
        public float MinHeightLevel { get; set; }
        public float MaxHeightLevel { get; set; }
        public byte OffsetX { get; set; }
        public byte OffsetY { get; set; }
        public byte Width { get; set; }
        public byte Height { get; set; }
        public uint OffsetMask2 { get; set; }
        public uint OffsetHeightmapData { get; set; }
        #endregion

        #region MH2OHeightmapData
        public float[,] Heightmap { get; set; } = new float[8, 8];
        public byte[,] Transparency { get; set; } = new byte[8, 8];
        #endregion

        #region MH2ORenderMask
        public byte[] Mask { get; set; } = new byte[8];
        public byte[] Fatigue { get; set; } = new byte[8];
        #endregion

        #region MH2OHeaderData

        #endregion

        public MH2O(byte[] chunkBytes) : base(chunkBytes)
        {
            var positionBeforeMH2OHeader = BaseStream.Position;

            // MH2OHeader
            OffsetInformation = ReadUInt32();
            LayerCount = ReadUInt32();
            OffsetRenderMask = ReadUInt32();

            var positionAfterMH2OHeader = BaseStream.Position;

            if (LayerCount > 0)
            {
                // MH2OInformation
                BaseStream.Position = positionBeforeMH2OHeader + OffsetInformation;
                LiquidTypeId = ReadUInt16();
                Flags = ReadUInt16();
                MinHeightLevel = ReadSingle();
                MaxHeightLevel = ReadSingle();
                OffsetX = ReadByte();
                OffsetY = ReadByte();
                Width = ReadByte();
                Height = ReadByte();
                OffsetMask2 = ReadUInt32();
                OffsetHeightmapData = ReadUInt32();
            }

        }

        public override byte[] GetChunkBytes()
        {
            
        }
    }
}
