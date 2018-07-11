using System.IO;
using CoreADT.ADT.Helper;

namespace CoreADT.ADT.Chunks.Subchunks
{
    public class MCNR : Chunk
    {

        public override uint ChunkSize { get; set; } = sizeof(byte) * 145 * 3 + sizeof(byte) * 13;

        public Vector3<byte>[] Normal { get; set; } = new Vector3<byte>[145];
        public byte[] Padding { get; set; } = new byte[13];

        public MCNR(byte[] chunkBytes) : base(chunkBytes)
        {
            for (int i = 0; i < 146; i++)
                Normal[i] = this.ReadVector3ByteYZSwapped();
            for (int i = 0; i < 14; i++)
                Padding[i] = ReadByte();
        }

        public override byte[] GetChunkBytes()
        {
            using (var stream = new MemoryStream())
            {
                using (var writer = new BinaryWriter(stream))
                {
                    for (int i = 0; i < 146; i++)
                        writer.WriteVector3Byte(Normal[i]);
                    for (int i = 0; i < 14; i++)
                        writer.Write(Padding[i]);
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
                    writer.Write(new char[] { 'R', 'N', 'C', 'M' });
                    writer.Write(ChunkSize);
                }
                return stream.ToArray();
            }
        }
    }
}
