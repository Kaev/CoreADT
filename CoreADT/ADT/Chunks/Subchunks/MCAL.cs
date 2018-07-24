using System;
using System.IO;
using CoreADT.ADT.MCALData;

namespace CoreADT.ADT.Chunks.Subchunks
{
    public class MCAL : Chunk
    {

        public override uint ChunkSize { get; }

        public MCALAlphaMap[] AlphaMaps { get; set; }

        public MCAL(byte[] chunkBytes, MCNK parentChunk, WDT.WDT wdt) : base(chunkBytes)
        {
            AlphaMaps = new MCALAlphaMap[parentChunk.MCLY.Layers.Length];
            for (int i = 0; i < parentChunk.MCLY.Layers.Length; i++)
                AlphaMaps[i] = new MCALAlphaMap(this, parentChunk, wdt, i);
            Close();
        }


        public override byte[] GetChunkBytes()
        {
            throw new NotImplementedException();
        }

        public byte[] GetChunkBytes(MCNK parentChunk, WDT.WDT wdt)
        {
            using (var stream = new MemoryStream())
            {
                using (var writer = new BinaryWriter(stream))
                {
                    for (int i = 0; i < AlphaMaps.Length; i++)
                        AlphaMaps[i].Write(writer, parentChunk, wdt, i);
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
                    writer.Write(new char[] { 'L', 'A', 'C', 'M' });
                    writer.Write(ChunkSize);
                }
                return stream.ToArray();
            }
        }
    }
}
