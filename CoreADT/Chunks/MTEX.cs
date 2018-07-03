﻿using System.Collections.Generic;
using System.IO;
using CoreADT.Helper;

namespace CoreADT.Chunks
{
    public class MTEX : Chunk
    {

        public override uint ChunkSize
        {
            get
            {
                uint size = 0;
                // + 1 because strings are written with '\0' at the end
                Filenames.ForEach(f => size += (uint)f.Length + 1);
                return size;
            }
            set => ChunkSize = value;
        }

        public List<string> Filenames { get; set; } = new List<string>();

        public MTEX(byte[] chunkBytes) : base(chunkBytes)
        {
            if (chunkBytes.Length > 0)
                Filenames = StringHelper.NullTerminatedStringsToList(chunkBytes);
            Close();
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

        public override byte[] GetChunkHeaderBytes()
        {
            using (var stream = new MemoryStream())
            {
                using (var writer = new BinaryWriter(stream))
                {
                    writer.Write(new char[] { 'X', 'E', 'T', 'M' });
                    writer.Write(ChunkSize);
                }
                return stream.ToArray();
            }
        }
    }
}
