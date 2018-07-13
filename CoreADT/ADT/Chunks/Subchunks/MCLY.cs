using System.IO;
using CoreADT.ADT.MCLYData;

namespace CoreADT.ADT.Chunks.Subchunks
{
    public class MCLY : Chunk
    {
        public override uint ChunkSize => MCLYLayer.Size * (uint)GetChunkBytes().Length / 16;

        public MCLYLayer[] Layers { get; set; }

        public MCLY(byte[] chunkBytes) : base(chunkBytes)
        {
            var layerCount = chunkBytes.Length / 16;
            Layers = new MCLYLayer[layerCount];
            for (int i = 0; i < layerCount; i++)
                Layers[i] = new MCLYLayer(this);
        }

        public override byte[] GetChunkBytes()
        {
            using (var stream = new MemoryStream())
            {
                using (var writer = new BinaryWriter(stream))
                {
                    for (int i = 0; i < Layers.Length; i++)
                        Layers[i].Write(writer);
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
                    writer.Write(new char[] { 'Y', 'L', 'C', 'M' });
                    writer.Write(ChunkSize);
                }
                return stream.ToArray();
            }
        }
    }
}
