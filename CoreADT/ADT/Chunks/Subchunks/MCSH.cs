using System.IO;

namespace CoreADT.Chunks.Subchunks
{
    public class MCSH : Chunk
    {

        public override uint ChunkSize { get; set; } = sizeof(byte) * 64 * 8;

        /// <summary>
        /// You need to read every _bit_ of these bytes for the entire ShadowMap
        /// </summary>
        public byte[,] ShadowMap { get; set; } = new byte[64, 8];

        public MCSH(byte[] chunkBytes) : base(chunkBytes)
        {
            for (int i = 0; i < 64; i++)
                for (int j = 0; j < 8; j++)
                    ShadowMap[i, j] = ReadByte();
            Close();
        }

        public override byte[] GetChunkBytes()
        {
            using (var stream = new MemoryStream())
            {
                using (var writer = new BinaryWriter(stream))
                {
                    for (int i = 0; i < 64; i++)
                        for (int j = 0; j < 8; j++)
                            writer.Write(ShadowMap[i, j]);
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
                    writer.Write(new char[] { 'H', 'S', 'C', 'M' });
                    writer.Write(ChunkSize);
                }
                return stream.ToArray();
            }
        }
    }
}
