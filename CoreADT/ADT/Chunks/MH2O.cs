using System;
using System.IO;
using CoreADT.ADT.MH2OData;

namespace CoreADT.ADT.Chunks
{
    public class MH2O : Chunk
    {
        public override uint ChunkSize
        {
            get
            {
                uint size = MH2OHeader.Size * 255;
                for (int i = 0; i < 255; i++)
                {
                    if (Headers[i].LayerCount > 0)
                        size += MH2OAttribute.Size;

                    for (int j = 0; j < Headers[i].LayerCount; j++)
                    {
                        size += MH2OInstance.Size;

                        if (Headers[i].Instances[j].OffsetExistsBitmap > 0)
                            size += (uint)Headers[i].Instances[j].RenderBitmapBytes.Length;

                        if (Headers[i].Instances[j].OffsetVertexData > 0)
                            size += MH2OInstanceVertexData.Size;
                    }
                }
                return size;
            }
        }
        public MH2OHeader[] Headers { get; set; } = new MH2OHeader[255];

        public MH2O(byte[] chunkBytes) : base(chunkBytes)
        {
            for (int i = 0; i < 255; i++)
                Headers[i] = new MH2OHeader(this);

            Close();
        }

        public override byte[] GetChunkBytes()
        {
            using (var stream = new MemoryStream())
            {
                using (var writer = new BinaryWriter(stream))
                {
                    // Write header data
                    for (int i = 0; i < 255; i++)
                        Headers[i].Write(writer);

                    for (int i = 0; i < 255; i++)
                    {
                        // We already wrote 0 for the offsets so we don't need to write anything here if LayerCount == 0
                        if (Headers[i].LayerCount > 0)
                        {
                            // Write header attributes data
                            // We can ommit this chunk if it only contains 0. In this case we already have 0 as an offset.
                            if (!Headers[i].Attributes.HasOnlyZeroes)
                            {
                                Headers[i].OffsetAttributes = (uint) BaseStream.Position;
                                Headers[i].Attributes.Write(writer);
                                var positionAfterCurrentAttributes = BaseStream.Position;
                                BaseStream.Position = Headers[i].OffsetAttributesPosition;
                                writer.Write(Headers[i].OffsetAttributes);
                                BaseStream.Position = positionAfterCurrentAttributes;
                            }

                            // Write instance data





                        }
                    }
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
