using System;
using System.Collections.Generic;
using System.IO;
using CoreADT.ADT.Flags;
using CoreADT.ADT.MapObjects;
using CoreADT.Helper;

namespace CoreADT.ADT.Chunks
{
    public class MODF : Chunk
    {

        public override uint ChunkSize => MapObjectDefinition.Size * (uint)MapObjectDefinitions.Count;

        public List<MapObjectDefinition> MapObjectDefinitions { get; set; } = new List<MapObjectDefinition>();

        public MODF(byte[] chunkBytes) : base(chunkBytes)
        {
            for (int i = 0; i < chunkBytes.Length / 64; i++)
                MapObjectDefinitions.Add(new MapObjectDefinition(this));
            Close();
        }

        public override byte[] GetChunkBytes()
        {
            using (var stream = new MemoryStream())
            {
                using (var writer = new BinaryWriter(stream))
                {
                    foreach (var definition in MapObjectDefinitions)
                        definition.Write(writer);
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
                    writer.Write(new char[] { 'F', 'D', 'O', 'M' });
                    writer.Write(ChunkSize);
                }
                return stream.ToArray();
            }
        }
    }
}
