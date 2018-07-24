using System.IO;
using CoreADT.ADT.MCSEData;

namespace CoreADT.ADT.Chunks.Subchunks
{
    public class MCSE : Chunk
    {

        public override uint ChunkSize => SoundEmitter.Size * (uint)SoundEmitters.Length;

        public SoundEmitter[] SoundEmitters { get; set; }

        public MCSE(byte[] chunkBytes, MCNK parentChunk) : base(chunkBytes)
        {
            for (int i = 0; i < parentChunk.NumberSoundEmitters; i++)
                SoundEmitters[i] = new SoundEmitter(this);
            Close();
        }

        public override byte[] GetChunkBytes()
        {
            using (var stream = new MemoryStream())
            {
                using (var writer = new BinaryWriter(stream))
                {
                    for (int i = 0; i < SoundEmitters.Length; i++)
                        SoundEmitters[i].Write(writer);
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
                    writer.Write(new char[] { 'E', 'S', 'C', 'M' });
                    writer.Write(ChunkSize);
                }
                return stream.ToArray();
            }
        }
    }
}
