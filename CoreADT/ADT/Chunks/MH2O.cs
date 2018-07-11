using System;
using System.IO;

namespace CoreADT.ADT.Chunks
{
    public class MH2O : Chunk
    {
        public override uint ChunkSize { get; }

        #region MH2OHeaderData

        #endregion

        #region MH2OInstances


        #region MH2OAttributes

        #endregion

        public byte[] RenderBitmapBytes { get; set; }

        public MH2O(byte[] chunkBytes, long positionAfterLastMH2OHeaderData) : base(chunkBytes)
        {
            BaseStream.Position = positionAfterLastMH2OHeaderData;
            // MH2OHeaderData


            if (LayerCount > 0)
            {
                // MH2OInstances
                BaseStream.Position = OffsetInstances;

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
                    // Attribute stuff

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
