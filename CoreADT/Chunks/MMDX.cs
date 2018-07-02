using System.Collections.Generic;
using System.IO;
using CoreADT.Helper;

namespace CoreADT.Chunks
{
    public class MMDX : Chunk
    {

        public List<string> Filenames { get; set; } = new List<string>();

        public MMDX(byte[] chunkBytes) : base(chunkBytes)
        {
            if (chunkBytes.Length > 0)
                Filenames = StringHelper.NullTerminatedStringsToList(chunkBytes);
        }

        public override byte[] GetChunkBytes()
        {
            using (var stream = new MemoryStream())
            {
                using (var writer = new BinaryWriter(stream))
                {
                    foreach (var file in Filenames)
                        writer.Write(file + '\0');
                }
                return stream.ToArray();
            }
        }
    }
}
