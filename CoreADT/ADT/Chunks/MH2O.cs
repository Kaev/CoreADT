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

                    // Write instance data
                    for (int i = 0; i < 255; i++)
                    {
                        // We already wrote 0 for the offsets so we don't need to write anything here if LayerCount == 0
                        if (Headers[i].LayerCount > 0)
                        {
                            Headers[i].OffsetInstances = (uint)writer.BaseStream.Position;
                            var positionBeforeInstances = writer.BaseStream.Position;
                            writer.BaseStream.Position = Headers[i].OffsetInstancesPosition;
                            writer.Write(Headers[i].OffsetInstances);
                            writer.BaseStream.Position = positionBeforeInstances;
                            for (int j = 0; j < Headers[i].LayerCount; j++)
                                Headers[i].Instances[j].Write(writer);
                        }
                    }

                    // Write referenced data
                    for (int i = 0; i < 255; i++)
                    {
                        // We already wrote 0 for the offsets so we don't need to write anything here if LayerCount == 0
                        if (Headers[i].LayerCount > 0)
                        {
                            // Write header attributes data
                            // We can omit this chunk if it only contains 0. In this case we already have 0 as an offset.
                            if (!Headers[i].Attributes.HasOnlyZeroes)
                            {
                                Headers[i].OffsetAttributes = (uint)writer.BaseStream.Position;
                                Headers[i].Attributes.Write(writer);
                                var positionAfterCurrentAttributes = writer.BaseStream.Position;
                                writer.BaseStream.Position = Headers[i].OffsetAttributesPosition;
                                writer.Write(Headers[i].OffsetAttributes);
                                writer.BaseStream.Position = positionAfterCurrentAttributes;
                            }

                            for (int j = 0; j < Headers[i].LayerCount; j++)
                            {
                                // Write RenderBitmapBytes if the length of the array is correct
                                if (Headers[i].Instances[j].RenderBitmapBytes.Length == (Headers[i].Instances[j].Width * Headers[i].Instances[j].Height + 7) / 8)
                                {
                                    Headers[i].Instances[j].OffsetExistsBitmap = (uint)writer.BaseStream.Position;
                                    writer.Write(Headers[i].Instances[j].RenderBitmapBytes);
                                    var positionAfterRenderBitmapBytes = writer.BaseStream.Position;
                                    writer.BaseStream.Position = Headers[i].Instances[j].OffsetExistsBitmapPosition;
                                    writer.Write(Headers[i].Instances[j].OffsetExistsBitmap);
                                    writer.BaseStream.Position = positionAfterRenderBitmapBytes;
                                }

                                // Write instance vertex data - TODO: When can we omit this?
                                Headers[i].Instances[j].OffsetVertexDataPosition = (uint) writer.BaseStream.Position;
                                Headers[i].Instances[j].VertexData.Write(writer, Headers[i].Instances[j]);
                                var positionAfterVertexData = writer.BaseStream.Position;
                                writer.BaseStream.Position = Headers[i].Instances[j].OffsetVertexDataPosition;
                                writer.Write(Headers[i].Instances[j].OffsetVertexDataPosition);
                                writer.BaseStream.Position = positionAfterVertexData;
                            }

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
