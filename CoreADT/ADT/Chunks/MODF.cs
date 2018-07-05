using System;
using System.IO;
using CoreADT.Flags;
using CoreADT.Helper;

namespace CoreADT.Chunks
{
    public class MODF : Chunk
    {

        public override uint ChunkSize { get; set; }

        public uint MWIDEntry { get; set; }
        public uint UniqueId { get; set; }
        public Vector3<float> Position { get; set; }
        public Vector3<float> Rotation { get; set; }
        /// <summary>
        /// Position + Bounding Box
        /// </summary>
        public Vector3<float> LowerBounds { get; set; }
        /// <summary>
        /// Position + Bounding Box
        /// </summary>
        public Vector3<float> UpperBounds { get; set; }
        public MODFFlags Flags { get; set; }
        public UInt16 DoodadSet { get; set; }
        /// <summary>
        /// Used for renaming
        /// </summary>
        public UInt16 NameSet { get; set; }
        public UInt16 Padding { get; set; }

        public MODF(byte[] chunkBytes) : base(chunkBytes)
        {
            MWIDEntry = ReadUInt32();
            UniqueId = ReadUInt32();
            Position = this.ReadVector3Float();
            Rotation = this.ReadVector3Float();
            LowerBounds = this.ReadVector3Float();
            UpperBounds = this.ReadVector3Float();
            Flags = (MODFFlags) ReadUInt16();
            DoodadSet = ReadUInt16();
            NameSet = ReadUInt16();
            Padding = ReadUInt16();
            Close();
        }

        public override byte[] GetChunkBytes()
        {
            using (var stream = new MemoryStream())
            {
                using (var writer = new BinaryWriter(stream))
                {
                    writer.Write(MWIDEntry);
                    writer.Write(UniqueId);
                    writer.WriteVector3Float(Position);
                    writer.WriteVector3Float(Rotation);
                    writer.WriteVector3Float(LowerBounds);
                    writer.WriteVector3Float(UpperBounds);
                    writer.Write((UInt16)Flags);
                    writer.Write(DoodadSet);
                    writer.Write(NameSet);
                    writer.Write(Padding);
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
