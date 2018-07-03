using System;
using System.IO;
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
            Position.X = ReadSingle();
            Position.Y = ReadSingle();
            Position.Z = ReadSingle();
            Rotation.X = ReadSingle();
            Rotation.Y = ReadSingle();
            Rotation.Z = ReadSingle();
            LowerBounds.X = ReadSingle();
            LowerBounds.Y = ReadSingle();
            LowerBounds.Z = ReadSingle();
            UpperBounds.X = ReadSingle();
            UpperBounds.Y = ReadSingle();
            UpperBounds.Z = ReadSingle();
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
                    writer.Write(Position.X);
                    writer.Write(Position.Y);
                    writer.Write(Position.Z);
                    writer.Write(Rotation.X);
                    writer.Write(Rotation.Y);
                    writer.Write(Rotation.Z);
                    writer.Write(LowerBounds.X);
                    writer.Write(LowerBounds.Y);
                    writer.Write(LowerBounds.Z);
                    writer.Write(UpperBounds.X);
                    writer.Write(UpperBounds.Y);
                    writer.Write(UpperBounds.Z);
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
                    writer.Write(new char[] { 'D', 'I', 'W', 'M' });
                    writer.Write(ChunkSize);
                }
                return stream.ToArray();
            }
        }
    }
}
