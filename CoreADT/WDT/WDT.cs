using System;
using System.IO;
using System.Linq;
using CoreADT.WDT.Chunks;
using MODF = CoreADT.WDT.Chunks.MODF;
using MVER = CoreADT.WDT.Chunks.MVER;
using MWMO = CoreADT.WDT.Chunks.MWMO;

namespace CoreADT.WDT
{
    public class WDT
    {

        public MPHD MPHD { get; set; }
        public MAIN MAIN { get; set; }
        public MWMO MWMO { get; set; }
        public MODF MODF { get; set; }
        public MVER MVER { get; set; }

        public void Load(string wdtFileName)
        {
            using (var reader = new BinaryReader(File.OpenRead(wdtFileName)))
            {
                while (reader.BaseStream.Position < reader.BaseStream.Length)
                {
                    var chunkName = new string(reader.ReadChars(4).Reverse().ToArray());
                    var chunkSize = reader.ReadInt32();
                    var chunkType = Type.GetType(chunkName);
                    if (chunkType != null)
                        GetType().GetProperty(chunkName)?.SetValue(this, Activator.CreateInstance(chunkType, reader.ReadBytes(chunkSize)));
                }
            }
        }

        public void Write()
        {
            using (var writer = new BinaryWriter(File.OpenWrite("test.wdt")))
            {
                // MVER
                writer.Write(MVER.GetChunkHeaderBytes());
                writer.Write(MVER.GetChunkBytes());

                // MPHD
                writer.Write(MPHD.GetChunkHeaderBytes());
                writer.Write(MPHD.GetChunkBytes());

                // MAIN
                writer.Write(MAIN.GetChunkHeaderBytes());
                writer.Write(MAIN.GetChunkBytes());

                // MWMO
                writer.Write(MWMO.GetChunkHeaderBytes());
                writer.Write(MWMO.GetChunkBytes());

                // MODF
                writer.Write(MODF.GetChunkHeaderBytes());
                writer.Write(MODF.GetChunkBytes());
            }
        }

    }
}
