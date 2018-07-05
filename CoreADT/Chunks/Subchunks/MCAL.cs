using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CoreADT.Chunks.Subchunks
{
    public class MCAL : Chunk
    {

        public byte[,] AlphaMap { get; set; }

        public MCAL(byte[] chunkBytes, MCNK parentChunk) : base(chunkBytes)
        {
            /*if (parentChunk.MCLY.Flags.HasFlag(CompressedAlphaMap) &&
            WDT.MPHD.Flags.HasFlag(0x4) || WDT.MPHD.Flags.HasFlag(0x80))
                // Compressed

            else if(WDT.MPHD.Flags.HasFlag(0x4) || WDT.MPHD.Flags.HasFlag(0x80))
                // Uncompressed (4096)
            else
                // Uncompressed (2048)
            */
        }

        public override uint ChunkSize { get; set; }
        public override byte[] GetChunkBytes()
        {
            throw new NotImplementedException();
        }

        public override byte[] GetChunkHeaderBytes()
        {
            using (var stream = new MemoryStream())
            {
                using (var writer = new BinaryWriter(stream))
                {
                    writer.Write(new char[] { 'L', 'A', 'C', 'M' });
                    writer.Write(ChunkSize);
                }
                return stream.ToArray();
            }
        }
    }
}
