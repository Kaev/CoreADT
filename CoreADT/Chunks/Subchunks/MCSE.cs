using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using CoreADT.Helper;

namespace CoreADT.Chunks.Subchunks
{
    public class MCSE : Chunk
    {

        public override uint ChunkSize { get; set; }

        public uint[] EntryId { get; set; }
        public Vector3<float>[] Position { get; set; }
        public Vector3<float>[] Size { get; set; }

        public MCSE(byte[] chunkBytes, MCNK parentChunk) : base(chunkBytes)
        {
            EntryId = new uint[parentChunk.NumberSoundEmitters];
            Position = new Vector3<float>[parentChunk.NumberSoundEmitters];
            Size = new Vector3<float>[parentChunk.NumberSoundEmitters];
        }

        public override byte[] GetChunkBytes()
        {
            using (var stream = new MemoryStream())
            {
                using (var writer = new BinaryWriter(stream))
                {
                    for (int i = 0; i < EntryId.Length; i++)
                    {
                        writer.Write(EntryId[i]);
                        writer.WriteVector3Float(Position[i]);
                        writer.WriteVector3Float(Size[i]);
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
                    writer.Write(new char[] { 'E', 'S', 'C', 'M' });
                    writer.Write(ChunkSize);
                }
                return stream.ToArray();
            }
        }
    }
}
