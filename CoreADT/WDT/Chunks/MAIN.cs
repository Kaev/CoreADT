using System.IO;
using CoreADT.Helper;

namespace CoreADT.WDT.Chunks
{
    public class MAIN : Chunk
    {

        public override uint ChunkSize { get; set; }

        private uint _Flags { get; set; }

        public bool HasAdt
        {
            get => _Flags.GetBit(0);
            set => BitHelper.SetBit(_Flags, 0, value);
        }
        public bool Loaded
        {
            get => _Flags.GetBit(1);
            set => BitHelper.SetBit(_Flags, 1, value);
        }
        public uint AsyncId { get; set; }

        public MAIN(byte[] chunkBytes) : base(chunkBytes)
        {
            _Flags = ReadUInt32();
            AsyncId = ReadUInt32();
            Close();
        }

        public override byte[] GetChunkBytes()
        {
            using (var stream = new MemoryStream())
            {
                using (var writer = new BinaryWriter(stream))
                {
                    writer.Write(_Flags);
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
                    writer.Write(new char[] { 'N', 'I', 'A', 'M' });
                    writer.Write(ChunkSize);
                }
                return stream.ToArray();
            }
        }
    }
}
