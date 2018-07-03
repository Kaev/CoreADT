using System;
using System.IO;
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
            Position.X = ReadSingle();
            Position.Y = ReadSingle();
            Position.Z = ReadSingle();
            Rotation.X = ReadSingle();
            Rotation.Y = ReadSingle();
            Rotation.Z = ReadSingle();
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
                    writer.Write(Position.X);
                    writer.Write(Position.Y);
                    writer.Write(Position.Z);
                    writer.Write(Rotation.X);
                    writer.Write(Rotation.Y);
                    writer.Write(Rotation.Z);
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
