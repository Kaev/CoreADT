using System;
using System.Collections.Generic;
using System.IO;
using CoreADT.ADT.Doodads;
using CoreADT.ADT.Flags;
using CoreADT.Helper;

namespace CoreADT.ADT.Chunks
{
    public class MDDF : Chunk
    {
        public override uint ChunkSize => DoodadDefinition.Size * (uint)DoodadDefinitions.Count;

        public List<DoodadDefinition> DoodadDefinitions { get; set; } = new List<DoodadDefinition>();

        public MDDF(byte[] chunkBytes) : base(chunkBytes)
        {
            for (int i = 0; i < chunkBytes.Length / 36; i++)
                DoodadDefinitions.Add(new DoodadDefinition(this));
            Close();
        }

        public override byte[] GetChunkBytes()
        {
            using (var stream = new MemoryStream())
            {
                using (var writer = new BinaryWriter(stream))
                {
                    foreach (var definition in DoodadDefinitions)
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
                    writer.Write(new char[] { 'F', 'D', 'D', 'M' });
                    writer.Write(ChunkSize);
                }
                return stream.ToArray();
            }
        }
    }
}
