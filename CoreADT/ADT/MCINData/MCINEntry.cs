using System.IO;

namespace CoreADT.ADT.MCINData
{
    public class MCINEntry
    {
        public static uint Size => sizeof(uint) * 4;

        public uint OffsetMCNK { get; set; }
        public uint ChunkSize { get; set; }
        /// <summary>
        /// Always 0, only set in the client
        /// </summary>
        public uint Flags { get; set; }
        /// <summary>
        /// Always 0, only set in the client
        /// </summary>
        public uint AsyncId { get; set; }

        public MCINEntry() { }

        public MCINEntry(BinaryReader reader)
        {
            OffsetMCNK = reader.ReadUInt32();
            ChunkSize = reader.ReadUInt32();
            Flags = reader.ReadUInt32();
            AsyncId = reader.ReadUInt32();
        }

        public void Write(BinaryWriter writer)
        {
            writer.Write(OffsetMCNK);
            writer.Write(ChunkSize);
            writer.Write(Flags);
            writer.Write(AsyncId);
        }
    }
}
