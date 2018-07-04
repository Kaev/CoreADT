using System;
using System.IO;
using CoreADT.Flags;
using CoreADT.Helper;

namespace CoreADT.Chunks
{
    public class MDDF : Chunk
    {
        public override uint ChunkSize { get; set; }

        public uint MMIDEntry { get; set; }
        public uint UniqueId { get; set; }
        public Vector3<float> Position { get; set; }
        public Vector3<float> Rotation { get; set; }
        /// <summary>
        /// 1024 = 1.0f
        /// </summary>
        public UInt16 Scale { get; set; }
        public MDDFFlags Flags { get; set; }

        public MDDF(byte[] chunkBytes) : base(chunkBytes)
        {
            MMIDEntry = ReadUInt32();
            UniqueId = ReadUInt32();
            Position = this.ReadVector3Float();
            Rotation = this.ReadVector3Float();
            Scale = ReadUInt16();
            Flags = (MDDFFlags)ReadUInt16();
            Close();
        }

        public override byte[] GetChunkBytes()
        {
            using (var stream = new MemoryStream())
            {
                using (var writer = new BinaryWriter(stream))
                {
                    writer.Write(MMIDEntry);
                    writer.Write(UniqueId);
                    writer.WriteVector3Float(Position);
                    writer.WriteVector3Float(Rotation);
                    writer.Write(Scale);
                    writer.Write((UInt16)Flags);
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
