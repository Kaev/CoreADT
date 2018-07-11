using System.IO;

namespace CoreADT.ADT.Chunks.Subchunks
{
    public class MCRF : Chunk
    {

        public override uint ChunkSize { get; set; }

        public uint[] DoodadReferences { get; set; }
        public uint[] ObjectReferences { get; set; }

        public MCRF(byte[] chunkBytes, MCNK parentChunk) : base(chunkBytes)
        {
            DoodadReferences = new uint[parentChunk.NumberDoodadRefs];
            ObjectReferences = new uint[parentChunk.NumberMapObjectRefs];

            for (int i = 0; i < DoodadReferences.Length; i++)
                DoodadReferences[i] = ReadUInt32();
            for (int i = 0; i < ObjectReferences.Length; i++)
                ObjectReferences[i] = ReadUInt32();
            Close();
        }

        public override byte[] GetChunkBytes()
        {
            using (var stream = new MemoryStream())
            {
                using (var writer = new BinaryWriter(stream))
                {
                    for (int i = 0; i < DoodadReferences.Length; i++)
                        writer.Write(DoodadReferences[i]);
                    for (int i = 0; i < ObjectReferences.Length; i++)
                        writer.Write(ObjectReferences[i]);
                }
                return stream.ToArray();
            }
        }

        public override byte[] GetChunkHeaderBytes()
        {
            ChunkSize = sizeof(uint) * (uint)DoodadReferences.Length + sizeof(uint) * (uint)ObjectReferences.Length;
            using (var stream = new MemoryStream())
            {
                using (var writer = new BinaryWriter(stream))
                {
                    writer.Write(new char[] { 'F', 'R', 'C', 'M' });
                    writer.Write(ChunkSize);
                }
                return stream.ToArray();
            }
        }
    }
}
